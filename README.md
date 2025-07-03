dotnet new sln -n react-service

Create src Folder

mkdir src
cd src
Run following command within src folder

dotnet new web -n React.Api
dotnet new classlib -n React.DAL
dotnet new classlib -n React.Domain


dotnet sln react-service.sln add .\src\React.Api\React.Api.csproj
dotnet sln react-service.sln add .\src\React.DAL\React.DAL.csproj
dotnet sln react-service.sln add .\src\React.Domain\React.Domain.csproj
