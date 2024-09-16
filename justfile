set windows-powershell := true

default:
  just --list

compose-services:
  docker compose up --build -d

install-ef-tools:
  dotnet tool install --global dotnet-ef

add-migration migrationName:
  dotnet ef migrations add '{{migrationName}}' -o ./Persistence/Migrations --startup-project ./src/TektonChallenge.Api/ --project ./src/TektonChallenge.Infrastructure/

remove-migration:
  dotnet ef migrations remove --force --startup-project ./src/TektonChallenge.Api/ --project  ./src/TektonChallenge.Infrastructure/

update-database:
  dotnet ef database update --startup-project ./src/TektonChallenge.Api/ --project ./src/TektonChallenge.Infrastructure/


 