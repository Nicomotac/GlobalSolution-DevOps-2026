# ============================================================
# Container da Aplicacao .NET (imagem personalizada)
# ============================================================

# ---------- Estagio de build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restaura as dependencias primeiro (melhor uso de cache)
COPY PM_API/PM_API.csproj PM_API/
RUN dotnet restore PM_API/PM_API.csproj

# Copia o restante e publica em modo Release
COPY PM_API/ PM_API/
RUN dotnet publish PM_API/PM_API.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- Estagio de runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

# Diretorio de trabalho (exigido pelo enunciado)
WORKDIR /app

# Cria um usuario nao privilegiado (exigido pelo enunciado)
RUN groupadd -r pmgroup && useradd -r -g pmgroup pmuser

# Copia os artefatos publicados
COPY --from=build /app/publish .

# Garante que o usuario nao-root seja dono dos arquivos
RUN chown -R pmuser:pmgroup /app

# Passa a executar como usuario nao privilegiado
USER pmuser

# Variavel de ambiente: a aplicacao escuta na porta 8080
ENV ASPNETCORE_URLS=http://+:8080

# Porta exposta (exigido pelo enunciado)
EXPOSE 8080

ENTRYPOINT ["dotnet", "PM_API.dll"]
