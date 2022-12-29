FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /TengriNewsParser
COPY ["./Posts.API", "Posts.API"]
RUN dotnet restore "Posts.API/Posts.API.csproj" 
COPY . .
WORKDIR "/TengriNewsParser/Posts.API"
RUN dotnet build "Posts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Posts.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posts.API.dll"]