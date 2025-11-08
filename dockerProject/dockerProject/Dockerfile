# Etapa base: runtime ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 21 # Exponha a porta FTP padrão se necessário

# Etapa de build: compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o ficheiro de projeto (ajustado para tua estrutura)
COPY ["dockerProject.csproj", "./"]
RUN dotnet restore "./dockerProject.csproj"

# Copia o resto do código
COPY . .
RUN dotnet build "./dockerProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish: gerar build final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./dockerProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: runtime para produção
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "dockerProject.dll"]
