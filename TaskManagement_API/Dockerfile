#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskManagement_API/TaskManagement_API.csproj", "TaskManagement_API/"]
RUN dotnet restore "./TaskManagement_API/./TaskManagement_API.csproj"
COPY . .
WORKDIR "/src/TaskManagement_API"
RUN dotnet build "./TaskManagement_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskManagement_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV MongoDb="mongodb+srv://mistaweng1:eI5wNjHCE6JUu71Z@taskmanagementdb.upnhx.mongodb.net/?retryWrites=true&w=majority&appName=TaskManagementDB"
ENV RedisConnection:Host="redis-11783.c251.east-us-mz.azure.redns.redis-cloud.com:11783"
ENV RedisConnection:Password="pEHowDbhoyQwCQ94DVQzFQIcD8P7t7WX"

EXPOSE 27017 11783 6379

ENTRYPOINT ["dotnet", "TaskManagement_API.dll"]