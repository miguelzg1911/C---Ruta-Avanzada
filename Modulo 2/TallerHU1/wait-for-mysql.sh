#!/bin/sh
set -e

host="$1"
shift
cmd="$@"

echo "Esperando a que MySQL ($host) esté listo..."
until mysqladmin ping -h"$host" --silent; do
  sleep 2
done

echo "MySQL está listo, ejecutando la aplicación .NET 🚀"
exec $cmd
