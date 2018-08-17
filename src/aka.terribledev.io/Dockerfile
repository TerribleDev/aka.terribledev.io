FROM microsoft/dotnet:2.1.400-sdk-alpine3.7

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build", "--configuration", "Release"]

RUN 'ls'
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000/tcp

CMD ["dotnet", "run", "--configuration", "Release"]
