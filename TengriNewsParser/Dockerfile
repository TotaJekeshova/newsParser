FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /TengriNewsParser
COPY ["./TengriNewsParser", "TengriNewsParser"]
RUN dotnet restore "TengriNewsParser/TengriNewsParser.csproj" 
COPY . .
WORKDIR "/NewsParser/TengriNewsParser"
RUN dotnet build "TengriNewsParser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TengriNewsParser.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TengriNewsParser.dll"]