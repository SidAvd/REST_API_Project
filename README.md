# RESTapi Project
Rest API that manages a Database.

## About the Project
This is a simple project in which a basic API manages information in a Database. The project is created with the ASP.NET Framework.
The DB tables are connected with a many-to-many relationship. The managing of the DB information is realized with the use of
Entity Framework ORM. The RDBMS used is MySQL.

## Functionality 
Whith the API running the user utilizing the endpoints accordingly can Post information that is being saved in the DB, can Get info
from the DB, can update the DB's info and can delete from the DB (CRUD).

The project is a search form for YuGiOh cards (from the known YuGiOh card game). The user can use eight fields that correspond to characteristics of cards (filters). These fields are the name (or part of it), card type, attribute, race, archetype, level, attack and defense. Of course any combination of these characteristics can be used too.
![search form 1](<Yugioh_MVC/Images/Form_1.png>)

After the search button is clicked, the application connects to this API: https://ygoprodeck.com/api-guide/ and fetches all the cards that match the chosen characteristics. The cards are presented with all their info inside a table (each line represents one card) and there is also a url for the image of every card.

## Technical Information
The application fetches information from the API: https://ygoprodeck.com/api-guide/. It is designed in the MVC architectural pattern, which means that the project is separated in Models, Views and the Controller.
The Views are the pages that the user interacts with (basicaly the Search Form View and the Results View). The Models are classes that are used as blueprints for the exchange of information between the Controller
and the Views and between the Controller and the JSON files. And lastly the Controller has all the logic that happens server side and manages the application’s functionality. Emphasis is also placed on the serialization
and deserialization of JSON formatted information. The View files use Razor syntax and the CSS framework Bootstrap.

## Test the App
