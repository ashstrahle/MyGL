#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["app/MyGL/MyGL.csproj", "app/MyGL/"]
RUN dotnet restore "app/MyGL/MyGL.csproj"
COPY . .
WORKDIR "/src/MyGL"
RUN dotnet clean
RUN dotnet build "MyGL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyGL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGL.dll"]