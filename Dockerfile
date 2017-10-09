FROM microsoft/dotnet:2.0.0-runtime-jessie
ENV ASPNETCORE_ENVIRONMENT="Container"
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet catalog-microservice.dll