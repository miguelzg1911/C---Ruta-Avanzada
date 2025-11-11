FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar csproj y restaurar
COPY *.csproj ./
RUN dotnet restore

# Copiar todo y publicar
COPY . ./
RUN dotnet publish -c Release -o out

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "webProductos.Api.dll"]
