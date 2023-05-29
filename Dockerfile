FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

ARG DB_HOST
ARG DB_PORT
ARG DB_NAME
ARG DB_USERNAME
ARG DB_PASSWORD
ARG JWT_SECRET

ENV DB_HOST=$DB_HOST
ENV DB_PORT=$DB_PORT
ENV DB_NAME=$DB_NAME
ENV DB_USERNAME=$DB_USERNAME
ENV DB_PASSWORD=$DB_PASSWORD
ENV JWT_SECRET=$JWT_SECRET

WORKDIR /src
COPY src .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR ./publish
COPY --from=build-env /publish .
EXPOSE 80
CMD dotnet Prokompetence.Web.PublicApi.dll ConnectionStrings:PostgresProkompetence="Host=${DB_HOST};Port=${DB_PORT};Database=${DB_NAME};Username=${DB_USERNAME};Password=${DB_PASSWORD};Ssl Mode=Require;" Authentication:Key="${JWT_SECRET}"