set /p migration=What Do You Want To Name This Migration?
TheGoodStuff.Church\dotnet ef migration add %migration% -o Data\Migrations
