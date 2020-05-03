FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /ui
RUN mkdir -p /ui/cms/Content
COPY  bin/Debug/netcoreapp3.0/  ./
COPY  cms/Content/ cms/Content/
RUN mkdir wwwroot
COPY  wwwroot/  ./wwwroot/
# RUN apk add --no-cache tzdata
ENV TZ=Asia/Riyadh
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENTRYPOINT ["dotnet", "CMS.dll"]
EXPOSE 80