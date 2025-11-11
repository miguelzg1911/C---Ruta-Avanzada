# Usamos la imagen oficial de .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos csproj y restauramos dependencias
COPY *.sln .
COPY webProductos.Api/*.csproj ./webProductos.Api/
COPY webProductos.Application/*.csproj ./webProductos.Application/
COPY webProductos.Domain/*.csproj ./webProductos.Domain/
COPY webProductos.Infrastructure/*.csproj ./webProductos.Infrastructure/

RUN dotnet restore

# Copiamos todo y construimos
COPY . .
WORKDIR /app/webProductos.Api
RUN dotnet publish -c Release -o out

# Imagen final para correr
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/webProductos.Api/out .
EXPOSE 5079
ENTRYPOINT ["dotnet", "webProductos.Api.dll"]
