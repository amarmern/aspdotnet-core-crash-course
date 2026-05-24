--dotnet tool install --global dotnet-ef --version 11.0.0-preview.4.26230.115
--dotnet ef migrations add InitialCreate --output-dir Data\Migrations
--dotnet ef database update

