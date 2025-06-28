# ビルド用イメージ
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# csprojをコピーして復元
COPY *.sln .
COPY App.Api/*.csproj ./App.Api/
COPY App.Application/*.csproj ./App.Application/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Infrastructure/*.csproj ./App.Infrastructure/

RUN dotnet restore

# ソースコードをコピーしてビルド
COPY . .
WORKDIR /app/App.Api
RUN dotnet publish -c Release -o out

# 実行用イメージ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/App.Api/out ./

ENTRYPOINT ["dotnet", "App.Api.dll"]
