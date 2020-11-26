FROM mono:6.8

RUN mkdir -p /root/project

ENV NUNIT_VERSION 3.10.0

RUN \
    apt-get update && \
    apt-get install -y && \
    apt-get install -y wget && \
    nuget install NUnit.Runners -o /tmp/nunit -version $NUNIT_VERSION && \
    ln -s /tmp/nunit/NUnit.ConsoleRunner.$NUNIT_VERSION/tools /nunit && \
    apt-get clean

COPY . /root/project

WORKDIR /root/project

#if we want to reduce docker image size we can put the nuget and msbuild command into an entrypoint script,
#then everything will be processed in the container and will not be included in the distributed image directly
RUN nuget restore BoardGame.sln && msbuild .

CMD ["mono", "/nunit/nunit3-console.exe", "Test/bin/Debug/Test.dll"]