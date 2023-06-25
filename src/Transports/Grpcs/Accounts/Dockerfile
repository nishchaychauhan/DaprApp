#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
RUN apt-get update && apt-get install -y --no-install-recommends apt-utils gss-ntlmssp libicu-dev locales locales-all
    # Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80
EXPOSE 443
EXPOSE 50001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
RUN apt-get update && apt-get install -y --no-install-recommends apt-utils gss-ntlmssp
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/BuildingBlocks/Healthchecks/Healthchecks.csproj", "src/BuildingBlocks/Healthchecks/"]
COPY ["src/Foundation/ContentTypes/Aarya.Foundation.ContentTypes.csproj", "src/Foundation/ContentTypes/"]
COPY ["src/Foundation/Contracts/Accounts/Microservices.Grpc.Accounts.Contracts.csproj", "src/Foundation/Contracts/Accounts/"]
COPY ["src/Foundation/Contracts/Assets/Aarya.Grpc.Assets.Contracts.csproj", "src/Foundation/Contracts/Assets/"]
COPY ["src/Foundation/Contracts/SMS/Aarya.Grpc.SMS.Contracts.csproj", "src/Foundation/Contracts/SMS/"]
COPY ["src/Foundation/Infrastructure/Aarya.Foundation.Infrastructure.csproj", "src/Foundation/Infrastructure/"]
COPY ["src/Foundation/Server/Aarya.Foundation.Infrastructure.Server.csproj", "src/Foundation/Server/"]
COPY ["src/Foundation/Partners/Aarya.Foundation.Infrastructure.Partners.csproj", "src/Foundation/Partners/"]
COPY ["src/Grpcs/Accounts/Microservices.Grpc.Accounts.csproj", "src/Grpcs/Accounts/"]
COPY ["src/Services/Assets/Aarya.Services.Assets.csproj", "src/Services/Assets/"]
COPY ["src/Services/Accounts/Aarya.Services.Accounts.csproj", "src/Services/Accounts/"]
COPY ["src/Services/AWSS3/Aarya.Services.AWSS3.csproj", "src/Services/AWSS3/"]
COPY ["src/Services/Core/Aarya.Services.Core.csproj", "src/Services/Core/"]
COPY ["src/Web/Services/Stores/Lists/Aarya.Client.Services.Lists.csproj", "src/Web/Services/Stores/Lists/"]
COPY ["src/Services/Elastic/Aarya.Services.Elastic.csproj", "src/Services/Elastic/"]
COPY ["src/Services/ScyllaDb/Aarya.Services.ScyllaDB.csproj", "src/Services/ScyllaDb/"]
COPY ["src/Services/Managers/Aarya.Services.Managers.csproj", "src/Services/Managers/"]
COPY ["src/SharedServices/Entity/Aarya.Services.EntityDefinition.csproj", "src/SharedServices/Entity/"]
COPY ["src/SharedServices/Shared/Aarya.Services.Shared.csproj", "src/SharedServices/Shared/"]
COPY ["NuGet.config", "."]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "src/Grpcs/Accounts/Microservices.Grpc.Accounts.csproj"
COPY . .
WORKDIR "/src/Grpcs/Accounts"
RUN dotnet build "Microservices.Grpc.Accounts.csproj" -c Release --no-restore -o /app/build
RUN dotnet dev-certs https

FROM build AS publish
RUN dotnet publish "Microservices.Grpc.Accounts.csproj" -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Grpc.Accounts.dll"]