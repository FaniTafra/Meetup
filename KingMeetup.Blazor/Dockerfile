#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT="Okd"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KingMeetup.Blazor/KingMeetup.Blazor.csproj", "KingMeetup.Blazor/"]
COPY ["KingMeetup.Messaging/KingMeetup.Messaging.csproj", "KingMeetup.Messaging/"]
COPY ["KingMeetup.Model/KingMeetup.Model.csproj", "KingMeetup.Model/"]
RUN dotnet restore "KingMeetup.Blazor/KingMeetup.Blazor.csproj"
COPY . .
WORKDIR "/src/KingMeetup.Blazor"
RUN dotnet build "KingMeetup.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KingMeetup.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KingMeetup.Blazor.dll"]