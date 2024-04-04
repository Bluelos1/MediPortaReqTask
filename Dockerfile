FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY MediportaTask/MediportaTask.csproj ./MediportaTask/
RUN dotnet restore "./MediportaTask/MediportaTask.csproj"

COPY MediportaTask/. ./MediportaTask/
RUN dotnet publish "./MediportaTask/MediportaTask.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/MediportaTask/out .
ENTRYPOINT ["dotnet", "MediportaTask.dll"]
