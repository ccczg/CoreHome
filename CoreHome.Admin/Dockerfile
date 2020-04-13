#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoreHome.Admin/CoreHome.Admin.csproj", "CoreHome.Admin/"]
COPY ["CoreHome.Infrastructure/CoreHome.Infrastructure.csproj", "CoreHome.Infrastructure/"]
COPY ["CoreHome.Data/CoreHome.Data.csproj", "CoreHome.Data/"]
RUN dotnet restore "CoreHome.Admin/CoreHome.Admin.csproj"
COPY . .
WORKDIR "/src/CoreHome.Admin"
RUN dotnet build "CoreHome.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreHome.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreHome.Admin.dll"]