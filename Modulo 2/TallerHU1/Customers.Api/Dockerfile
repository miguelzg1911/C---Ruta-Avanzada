# Etapa 1: construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos los archivos de proyecto y restauramos dependencias
COPY *.sln .
COPY Customers.Api/*.csproj Customers.Api/
COPY GestionCustomers.Domain/*.csproj GestionCustomers.Domain/
COPY GestionCustomers.Application/*.csproj GestionCustomers.Application/
COPY GestionCustomers.Infrastructure/*.csproj GestionCustomers.Infrastructure/

RUN dotnet restore

# Copiamos el resto del código y publicamos
COPY . .
RUN dotnet publish Customers.Api/Customers.Api.csproj -c Release -o out

# Etapa 2: imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# instala el cliente mysql
RUN apt-get update && apt-get install -y default-mysql-client

# Copiar el script de espera
COPY wait-for-mysql.sh /app/
RUN chmod +x /app/wait-for-mysql.sh

COPY --from=build /app/out .

EXPOSE 5040
ENTRYPOINT ["dotnet", "Customers.Api.dll"]
