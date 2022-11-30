FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["Shifty.App/Shifty.App.csproj", "Shifty.App/"]
COPY ["Shifty.Api/Shifty.Api.csproj", "Shifty.Api/"]
RUN dotnet restore "Shifty.App/Shifty.App.csproj"

COPY . ./
RUN dotnet publish "Shifty.App/Shifty.App.csproj" -c Release -o output

FROM nginx:alpine AS prod
WORKDIR /var/www/web
COPY --from=build-env /app/output/wwwroot .
COPY "Shifty.App/nginx.conf" /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443
