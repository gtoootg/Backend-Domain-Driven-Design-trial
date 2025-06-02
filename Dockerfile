# ビルド用イメージ
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# csprojをコピーして復元
COPY *.sln .
COPY YourApp.API/*.csproj ./YourApp.API/
COPY YourApp.Application/*.csproj ./YourApp.Application/
COPY YourApp.Domain/*.csproj ./YourApp.Domain/
COPY YourApp.Infrastructure/*.csproj ./YourApp.Infrastructure/

RUN dotnet restore

# ソースコードをコピーしてビルド
COPY . .
WORKDIR /app/YourApp.API
RUN dotnet publish -c Release -o out

# 実行用イメージ
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/YourApp.API/out ./

ENTRYPOINT ["dotnet", "YourApp.API.dll"]
