FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /App

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src
# Copy everything
COPY ./AcmeCorpAPI ./AcmeCorpAPI
WORKDIR /src/AcmeCorpAPI
# Restore as distinct layers
RUN dotnet restore 
# Build and publish a release
RUN dotnet publish -c Debug -o /App/publish
#migrations
RUN dotnet tool install --global dotnet-ef  
WORKDIR /App/publish
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update --project /src/AcmeCorpAPI/AcmeCorpAPI.csproj
#COPY /root/.local/share/acmecorp.db /App/publish 

# Build runtime image
FROM base as final
WORKDIR /App
COPY --from=build-env /App/publish .
COPY --from=build-env /root/.local/share/acmecorp.db .
ENTRYPOINT ["./AcmeCorpAPI"]