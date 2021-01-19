FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY ./publish /publish
WORKDIR /publish
EXPOSE 44333/tcp
ENTRYPOINT ["dotnet", "Softplan.WebApi2.dll"]