#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Craftable.Web/Craftable.Web.csproj", "Craftable.Web/"]
RUN dotnet restore "Craftable.Web/Craftable.Web.csproj"
COPY . .
WORKDIR "/src/Craftable.Web"
RUN dotnet build "Craftable.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Craftable.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Craftable.Web.dll"]