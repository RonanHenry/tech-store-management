# Tech Store Management (DL Project)
This project is built using the .NET Framework with Windows Presentation Foundation (WPF) as the client and an ASP.NET Web API.

## Preparation
A MySQL Server must be running (WAMP on Windows).

By default, the app assumes that the MySQL user is root and he has no password. If you want to change it, open the file  TechStoreLibrary/Enums/ConnectionResource.cs and modify the string values of the connections (the first is used by the Web API and the second is used directly by the app as a backup when the Web API is not available).
Depending if the Web API is running or not, the app will switch between the two possible sources.

Note: you can also change the names of the databases.

After making these changes, you'll need to republish (to rebuild) the app for the changes to be effective.

## Installation
Start the installation by launching the published installer (setup.exe).
The app should launch right after the installation. If not, you can find it in Start/All programs/Ronan Henry/Tech Store Management.exe.

The first launch can take a while as the database and some initial data are being generated.
The app shows you which source it is using at the bottom right corner.

To launch the Web API, open the solution in Visual Studio and make TachStoreWeb the startup project (which should already be done) and start it.
Once the Web API successfully runs, there's no need to restart the app as it keeps checking if the Web API is available each time you change of tab (vertical menu on the left).

The source should be updated to use the API and you'll be invited to wait again as the "remote" database is being created with initial random generated data through the ASP.NET Web service.
