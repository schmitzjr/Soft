FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY ./publish /publish
WORKDIR /publish
EXPOSE 44555/tcp
ENTRYPOINT ["dotnet", "Softplan.WebApi1.dll"]