FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Chat/*.db /base/
COPY Chat/*.csproj ./Chat/
RUN dotnet restore

# copy everything else and build app
COPY Chat/. ./Chat/
COPY Chat/*.db /base/
WORKDIR /app/Chat
RUN dotnet publish -c Release -o out --disable-parallel


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/Chat/out ./
COPY Chat/*.db /base/
ENTRYPOINT ["dotnet", "Chat.dll"]
