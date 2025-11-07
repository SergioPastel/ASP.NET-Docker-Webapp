# Etapa 1: imagem base de runtime (onde a app vai correr)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa 2: imagem de build (para compilar o projeto)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o ficheiro de projeto e restaurar dependências
COPY src.csproj ./
RUN dotnet restore src.csproj

# Copiar o resto do código e compilar em Release
COPY . ./
RUN dotnet publish src.csproj -c Release -o /app/publish

# Etapa 3: imagem final, só com o runtime e os ficheiros publicados
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "src.dll"]
