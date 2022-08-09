import http from 'k6/http';
import { check, sleep } from 'k6';
export const options = {
    vus: 10,
    duration: '1m',
    insecureSkipTLSVerify: true
};

export default function () {
    const url = 'https://localhost:7239/auth/register';
    const payload = JSON.stringify({
        "firstName": "Giovanni",
        "lastName": "demo",
        "username": "giovanni@test.demo",
        "password": "somepwd"
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
    // sleep(1);
}