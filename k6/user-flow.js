import http from 'k6/http';
import { check, group, sleep } from 'k6';
import {
    randomIntBetween,
    randomString,
    randomItem,
    uuidv4,
    findBetween,
  } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';
export const options = {
  vus: 1000,
  duration: '60s',
};
const SLEEP_DURATION = 0.1;

export default function () {
    let username = `${randomString(10)}@demo.demo`;
    let pwd = "S0meValidpa$$wd";
//    console.log(`VU: ${__VU}  -  ITER: ${__ITER}`);

/*
    console.log(`
      VU ID in instance: ${exec.vu.idInInstance}
        VU ID in test: ${exec.vu.idInTest}`);
        */
    let registration = JSON.stringify({
        "firstName": randomItem(['Gionni', 'Demo']),
        "lastName": `Jon${randomString(1, 'aeiou')}s`,
        "username": username,
        "password": pwd
    });
    let login = JSON.stringify({
        "username": username,
        "password": pwd,
    });
    const params = {
        headers: {
            'Content-Type': 'application/json',
        }
    };
//   console.log(`login: ${login}`);
  group('simple user journey', (_) => {
    // register request
    const register_response = http.post('https://localhost:7257/auth/register', registration, params);
    check(register_response, {
        'is status 200': (r) => r.status === 200,
        'is token present': (r) => r.json().hasOwnProperty('token'),
      });
      
    // Login request
    const login_response = http.post('https://localhost:7239/auth/login', login, params);
    check(login_response, {
      'is status 200': (r) => r.status === 200,
      'is token present': (r) => r.json().hasOwnProperty('token'),
    });
    // params.headers['api-key'] = login_response.json()['api_key'];
    sleep(SLEEP_DURATION);
  });
}