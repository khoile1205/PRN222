# Use .NET SDK to build the solution
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore the dependencies
COPY CaffeinManagement.sln ./
COPY BussinessLayer/*.csproj BussinessLayer/
COPY DataLayer/*.csproj DataLayer/
COPY PresentationLayer/*.csproj PresentationLayer/
COPY Shared/*.csproj Shared/

# Restore dependencies for the solution
RUN dotnet restore CaffeinManagement.sln

# Copy the entire source code
COPY . .

# Set the working directory to the PresentationLayer before building
WORKDIR /app/PresentationLayer

# Build the solution
RUN dotnet build -c Release

# Publish the solution

RUN dotnet publish -c Release -o out
# Use ASP.NET runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/PresentationLayer/out .

# Copy appsettings files
COPY PresentationLayer/appsettings.json ./
COPY PresentationLayer/appsettings.Development.json ./

# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose port
EXPOSE 8080

# Run the PresentationLayer app
ENTRYPOINT ["dotnet", "PresentationLayer.dll"]