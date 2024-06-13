FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/AppFileUploader.API/AppFileUploader.API.csproj", "src/Services/AppFileUploader.API/"]
COPY ["src/Services/AppFileUploader.Infrastructure/AppFileUploader.Infrastructure.csproj", "src/Services/AppFileUploader.Infrastructure/"]
COPY ["src/Services/AppFileUploader.Application/AppFileUploader.Application.csproj", "src/Services/AppFileUploader.Application/"]
COPY ["src/Services/AppFileUploader.Domain/AppFileUploader.Domain.csproj", "src/Services/AppFileUploader.Domain/"]
RUN dotnet restore "src/Services/AppFileUploader.API/AppFileUploader.API.csproj"
COPY . .
WORKDIR "src/Services/AppFileUploader.API"
RUN dotnet build "AppFileUploader.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppFileUploader.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppFileUploader.API.dll"]
