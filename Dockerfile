# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar el archivo del proyecto
COPY API_Buenas-practicas.csproj ./

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código fuente
COPY . ./

# Publicar la aplicación
RUN dotnet publish API_Buenas-practicas.csproj -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-env
WORKDIR /app
COPY --from=build-env /app/out .

# Definir en qué puerto escuchará la app
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "API_Buenas-practicas.dll"]