# Web-API
ASP.NET Web API 2 using Entity Framework 6 and Generic Repository Pattern.
I have used Auotfac for Dependency Injection and Swagger for API Documentation. I have added integration test also.
Added Jenkins integration to this repo.
Build Test 2

## Getting Started

1. Make sure you've installed recent versions of asp.net framework, sql server and visual studio in your machine.

3. Navigate to the solution’s root directory.

2. create a DB with the name 'SamApp' in your machine and execute the initialScript.sql in order to create the initial version of the DB.

3. open the project GDWebApiLayer and modify the connection string 'SampleAppConnection' in the Web.config file using your local settings.

4. Set GDWebApiLayer as StartUp project.

5. build and execute GDWebApiLayer project using chrome. Hopefully, this task will be executed correctly and you won’t encounter any errors. If errors do occur, make sure that steps 2, 3 and 4 have been completed successfully.

6. the system will open your browser on http://localhost:49815/ without content. To show the application please install the Angular project https://github.com/hmaury1/GameOfDrones.Angular


## Data Model

![Optional Text](../master/Diagram.png)

## Unit Test

To run the test cases you need to have NUnit Test Adapter for Visual Studio.
