#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT="Okd"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KingMeetup.api/KingMeetup.API.csproj", "KingMeetup.api/"]
COPY ["KingMeetup.Contract/KingMeetup.Contract.csproj", "KingMeetup.Contract/"]
COPY ["KingMeetup.Infrastructure/KingMeetup.Infrastructure.csproj", "KingMeetup.Infrastructure/"]
COPY ["KingMeetup.IOC/KingMeetup.IOC.csproj", "KingMeetup.IOC/"]
COPY ["KingMeetup.Messaging/KingMeetup.Messaging.csproj", "KingMeetup.Messaging/"]
COPY ["KingMeetup.Model/KingMeetup.Model.csproj", "KingMeetup.Model/"]
COPY ["KingMeetup.Repository/KingMeetup.Repository.csproj", "KingMeetup.Repository/"]
COPY ["KingMeetup.Service/KingMeetup.Service.csproj", "KingMeetup.Service/"]


RUN dotnet restore "KingMeetup.api/KingMeetup.API.csproj"
COPY . .
WORKDIR "/src/KingMeetup.api"
RUN dotnet build "KingMeetup.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KingMeetup.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KingMeetup.API.dll"]