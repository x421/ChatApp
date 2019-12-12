FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY base.db /
COPY Chat/*.csproj ./Chat/
RUN dotnet restore

# copy everything else and build app
COPY Chat/. ./Chat/
WORKDIR /app/Chat
RUN dotnet publish -c Release -o out --disable-parallel


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/Chat/out ./
ENTRYPOINT ["dotnet", "Chat.dll"]
