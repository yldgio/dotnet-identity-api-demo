FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY src/ ./
RUN ls -la
COPY Docs/OpenApi/openapi.yml ./Identity.Api/openapi.yml
RUN sed -i 's|..\\..\\Docs\\OpenApi\\||g' ./Identity.Api/Identity.Api.csproj
RUN dotnet restore ./Identity.Api/
RUN dotnet build ./Identity.Api/ -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ./Identity.Api/ -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]