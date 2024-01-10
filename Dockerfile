FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5133

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["kwiz_api_game.csproj", "./"]
RUN dotnet restore "kwiz_api_game.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "kwiz_api_game.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "kwiz_api_game.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "kwiz_api_game.dll"]
