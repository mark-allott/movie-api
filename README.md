# MovieApi Proof of concept

## Requirements
1. Data is taken from a Kaggle dataset [9000+ Movies Dataset](https://www.kaggle.com/datasets/disham993/9000-movies-dataset)
1. Design an API that can be used to query the dataset
1. Consumers of the API **must** be able to search by film title / name
1. Consumers of the API **must** be able to limit the search results returned
1. Consumers of the API **must** be able to perform paged searches of results
1. Consumers of the API *should* be able to filter movies by genre

## Additional optional requirements
1. Consumers of the API *may* wish to search by actor
1. Consumers of the API *may* wish to search by more than one actor using and/or combinations

## Testing
1. Before attempting to run the application locally, the database must be configured correctly:
    1. If using LocalDB:
        1. Using the MSSQLLocalDb instance, no changes to the connection string are required
        1. If using a different named instance, the `appsettings.development.json` file will need to be updated with the name of the named instance
    1. If using SQL Server:
        1. Note the name of the server and connection requirements - i.e. if not using the trusted connection option, then a username and password **must** be supplied
        1. Update the `appsettings.development.json` file, replacing `(localDb)\\MSSQLLocalDb` with the appropriate connection information for the server being connected to
        1. If ***not*** using trusted connection, remove the `Trusted_Connection=true`, replacing it with `User ID=[username];Password=[password]`, replacing the username and password with the appropriate values
1. Again, prior to attempting to run the application, the database **must** be created / initialised, or when connecting to it, the console *will* output SQL errors / exceptions!
    1. If using Visual Studio: open the "Package Manager Console" from the View->Other Windows menu option
    1. If using VSCode, open a Terminal session
    1. From the command window (Package Manager Console or Terminal session), ensure the current folder is the root of the project, e.g. `[git clone root location]/movie-api`
    1. From the command-line, run the following: `dotnet ef database update -c MovieContext -p ./MovieApiData/ -s ./movie-api/` - direction of slashes may be different, dependent upon OS, console used etc.
1. Now the database is configured and initialised, you can run the project
    1. Using Visual Studio - use F5, Ctrl+F5, one of the "play" controls on the menu ribbon etc., that should auto-launch the API in a new browser window and navigate to the swagger page
    1. Using VSCode - from a terminal, use this command: `dotnet run --project ./movie-api/movie-api.csproj`. The console output should show the localhost address to navigate to, ctrl+click on the link shown and it should open in a browser - most likely with an error. To fix this, append `/swagger` to the URL shown in the address bar and refresh
1. On first running the application, no data has been setup as yet, so queries should work, but won't yield any results. A sample dataset is provided and can be loaded into the database by using the `/api/Import/LoadKaggleDataset` endpoint. How quickly that will run depends an awful lot on your own configuration. Once completed, the API endpoints should now show data when querying them.