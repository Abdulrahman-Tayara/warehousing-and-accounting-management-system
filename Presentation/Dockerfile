FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["wms/wms.csproj", "wms/"]
RUN dotnet restore "wms/wms.csproj"
COPY . .
WORKDIR "/src/wms"
RUN dotnet build "wms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wms.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wms.dll"]
