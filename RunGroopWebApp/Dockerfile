# Use the official .NET SDK (build stage)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy and restore project dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source files and build
COPY . . 
RUN dotnet publish -c Release -o /app/out --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published files from the build stage
COPY --from=build /app/out . 

# Expose necessary ports
EXPOSE 5223

# Run the application
CMD ["dotnet", "out/RunGroopWebApp.dll"]

#for railway
# CMD ["dotnet", "RunGroopWebApp.dll"]

# CMD ["sh", "-c", "dotnet ef database update && dotnet out/api.dll"]

