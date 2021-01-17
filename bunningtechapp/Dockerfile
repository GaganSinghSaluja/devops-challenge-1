FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish bunningtech-app -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=build /app/out .
ADD bunningtech-app/data data
ADD bunningtech-app/processed processed
ADD wait-for-it.sh wait-for-it.sh
RUN chmod +x wait-for-it.sh
RUN apt-get update && \
      apt-get -y install sudo

RUN useradd -m docker && echo "docker:docker" | chpasswd && adduser docker sudo

USER docker
CMD /bin/bash

USER root
RUN apt-get update && DEBIAN_FRONTEND=noninteractive apt-get install -y  netcat 
  


