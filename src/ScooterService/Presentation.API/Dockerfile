# dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:44351  
EXPOSE 44351

COPY ./publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "ScooterRental.Presentation.WebAPI.dll"]

# docker build -t andreraica/ScooterRentalcalculator_webapi .

# >>> Publishing <<<
# docker login
# docker push andreraica/ScooterRentalcalculator_webapi