# Identity Api App

[toc]

## Identity.Contracts

- **RegisterRequest**

  ```json
  {
      "firstName": "",
      "lastName": "",
      "username": "",
      "password": ""
  }
  ```

- **LoginRequest**

  ```json
  {
      "username": "",
      "password": ""
  }
  ```

- **AuthResponse**

  ```json
  {
      "id": "",
      "firstName": "",
      "lastName": "",
      "username": "",
      "token": ""
  }
  ```

## Identity.Api

- AuthController
  - Login
  - Register

## Secrets in config

`dotnet user-secrets set "JwtSettings:Secret" "any-super-secret-key" --project .\Identity.Api\`
