import http from 'k6/http';
import { check, sleep } from 'k6';
export const options = {
    vus: 1,
    duration: '30s',
    insecureSkipTLSVerify: true,
    noConnectionReuse: false
};

export default function () {
    const url = 'https://localhost:7257/auth/register';
    const payload = JSON.stringify({
        "firstName": "Giovanni",
        "lastName": "demo",
        "username": "giovanni@test.demo",
        "password": "S0meValidpa$$wd"
    });
    const params = {
      headers: {
        'Content-Type': 'application/json',
      },
    };
    const res = http.post(url, payload, params);
    check(res, {
        'is status 400': (r)=> r.status == 409,
    });
    sleep(1);
}