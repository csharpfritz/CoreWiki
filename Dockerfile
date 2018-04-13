FROM cakebuild/cake:2.1-sdk AS builder

RUN apt-get update -qq \
    && curl -sL https://deb.nodesource.com/setup_9.x | bash - \
    && apt-get install -y nodejs

ADD .  /src

RUN Cake /src/build.cake --Target=Publish

FROM microsoft/aspnetcore:2.0.6

COPY --from=builder /src/output /app

CMD ["dotnet","/app/CoreWiki.dll"]