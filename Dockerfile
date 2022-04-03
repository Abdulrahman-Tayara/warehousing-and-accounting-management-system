FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/Presentation.csproj", "src/Presentation/"]
COPY ["Application/Application.csproj", "src/Application/"]
COPY ["Domain/Domain.csproj", "src/Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["Authentication/Authentication.csproj", "src/Authentication/"]
RUN dotnet restore "src/Presentation/Presentation.csproj" --disable-parallel
COPY . .
WORKDIR "/src/Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -l
# ENTRYPOINT ["dotnet", "Presentation.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Presentation.dll
