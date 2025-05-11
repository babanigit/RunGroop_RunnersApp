# Use the official .NET SDK (build stage)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything (including solution file, props, configs) BEFORE restoring
COPY . . 

# Restore dependencies
WORKDIR /app/RunGroopWebApp
RUN dotnet restore

# Build and publish the app
RUN dotnet publish -c Release -o /app/out --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out . 

EXPOSE 5223

# Run the application
# CMD ["dotnet", "out/RunGroopWebApp.dll"]

#for railway
CMD ["dotnet", "RunGroopWebApp.dll"]
