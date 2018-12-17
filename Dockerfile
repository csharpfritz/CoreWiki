FROM cakebuild/cake:v0.31.0-2.1-sdk AS builder

RUN apt-get update -qq \
    && curl -sL https://deb.nodesource.com/setup_9.x | bash - \
    && apt-get install -y nodejs

ADD .  /src

RUN Cake /src/build.cake --Target=Publish

FROM microsoft/dotnet:2.1.3-aspnetcore-runtime

WORKDIR app

COPY --from=builder /src/output .

CMD ["dotnet","CoreWiki.dll"]

