# K6

[install k6](https://k6.io/docs/getting-started/installation/) or run in Docker

```shell
PS C:\ErrorFlow> dotnet run --project .\Identity.Api\Identity.Api.csproj -c release

PS C:\ErrorFlow> cd .\k6\

#installed globally
PS C:\ErrorFlow\k6> k6 run load-test.js

#Powershell + Docker
PS C:\ErrorFlow\k6> cat load-test.js | docker run --rm -i grafana/k6 run -

#bash + Docker 
$ docker run --rm -i grafana/k6 run - <load-test.js 
```
