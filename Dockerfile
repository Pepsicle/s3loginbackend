#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["REST_API_Prototype_1/REST_API_Prototype_1.csproj", "REST_API_Prototype_1/"]
RUN dotnet restore "REST_API_Prototype_1/REST_API_Prototype_1.csproj"
COPY . .
WORKDIR "/src/REST_API_Prototype_1"
RUN dotnet build "REST_API_Prototype_1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "REST_API_Prototype_1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "REST_API_Prototype_1.dll"]