# 1) Runtime (imagem final, leve)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# 2) SDK (para build/publish)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 3) Copiar solução + csproj (respeitando a pasta src)
COPY product-docker.slnx ./
COPY src/API/API.csproj src/API/
COPY src/Application/Application.csproj src/Application/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/

# 4) Restaurar dependências da API (puxa as libs referenciadas)
RUN dotnet restore "src/API/API.csproj"

# 5) Copiar o resto do código para dentro da imagem
COPY . .

# 6) Compilar
RUN dotnet build "src/API/API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 7) Publicar
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/API/API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 8) Imagem final só com runtime e app publicado
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
