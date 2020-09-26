# dotnet publish -c Release -o publish

# docker build -t andreraica/scooterrental_scooterservice .
FROM mcr.microsoft.com/dotnet/core/sdk:3.1
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:44351  
EXPOSE 44351

COPY ./publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "ScooterRental.Presentation.API.dll"]

# >>> Running <<<
# docker run -p 44351:44351 andreraica/scooterrental_scooterservice -d

# >>> Publishing <<<
# docker login
# docker push andreraica/scooterrental_scooterservice