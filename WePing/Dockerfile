FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./src/WePing/bin/Release/netcoreapp3.1/publish .
ENV ASPNETCORE_URLS https://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000
ENTRYPOINT dotnet WePing.dll

