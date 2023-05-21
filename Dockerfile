FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /src
COPY src .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR ./publish
COPY --from=build-env /publish .
EXPOSE 3000
CMD dotnet Prokompetence.Web.PublicApi.dll ConnectionStrings:Prokompetence="Host=rc1b-liahvfp827gx0uul.mdb.yandexcloud.net;Port=6432;Database=prokompetence;Username=npredein;Password=72718735;Ssl Mode=VerifyFull;" Authentication:Key="secret2dsagfhasgfyugajhahfs"

FROM nginx:1.21.0-alpine as production
ENV NODE_ENV production
# COPY --from=builder /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]