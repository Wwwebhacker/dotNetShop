# Use the SDK image to build the app (this includes more tools and is suitable for development tasks)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# # Install dotnet-ef tool
# RUN dotnet tool install --global dotnet-ef

# # Set the PATH so that the dotnet tool can be found
# ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["Items.csproj", "."]
RUN dotnet restore "Items.csproj"
COPY . .
RUN dotnet build "Items.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Items.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime image as a parent image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 5000 for the backend service
EXPOSE 80

ENTRYPOINT ["dotnet", "Items.dll"]
