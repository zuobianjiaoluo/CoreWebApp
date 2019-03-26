FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-stretch AS build
WORKDIR /src
COPY ["WebApp_HelloWorld/WebApp_HelloWorld.csproj", "WebApp_HelloWorld/"]
RUN dotnet restore "WebApp_HelloWorld/WebApp_HelloWorld.csproj"
COPY . .
WORKDIR "/src/WebApp_HelloWorld"
RUN dotnet build "WebApp_HelloWorld.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApp_HelloWorld.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApp_HelloWorld.dll"]