# RESTapi Project
Rest API that manages a Database.

## About the Project
This is a simple project in which a basic API manages information in a Database (Rest-HTTP). The project is created with the
ASP.NET Framework. The DB tables are connected with a many-to-many relationship. The managing of the DB information is realized
with the use of Entity Framework ORM. The RDBMS used is MySQL.

## Functionality 
Whith the API running the user utilizing the endpoints accordingly (with Postman or Swagger for example) can Post information
(JSON formatted) that is being saved in the DB, can Get info from the DB, can update the DB's info and can delete from the DB
(CRUD).

![search form 1](<Yugioh_MVC/Images/Form_1.png>)

## Technical Information
The project contains all the logic of an API that manages a DB. This means that it uses Models (classes) that corespond to the tables
of the DB, it uses the Entity Framework object-relational mapping to relate the DB's tables with the models and realize the many-to-many
relationship that these tables have. It also uses Controllers to relate the API's URIs (endpoints) to certain actions (and what info it 
is be fetched from the DB). DTO logic is also used for safety practicing.

## Test the App
Download the project. Create the DB schema. Open Visual Studio and then open the Package Manager Console. Run Add-Migration "Init migration"
and after its done run Update-Database. After the DB's tables are created run the project and test all the endpoints of the API with
Postamn or Swagger.
