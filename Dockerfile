#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["s3loginbackend/s3loginbackend.csproj", "s3loginbackend/"]
RUN dotnet restore "s3loginbackend/s3loginbackend.csproj"
COPY . .
WORKDIR "/src/s3loginbackend"
RUN dotnet build "s3loginbackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "s3loginbackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "s3loginbackend.dll"]