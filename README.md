# Game of Chance - A RESTful API solution built with .Net Core, Asp.Net Core Identity and Sqlite-db/SQLServer-db

## Tools
- Using docker, so that the project can be run in any machine with little effort.
- Secure the API with AspNetCore.Identity and JWT
- Postman to execute and test the endpoints
- NUnit to unit tests

## Requirements
- Asp.Net Core 5
- Git
- Docker (optional)

Notes
- Note 1: This repository includes the postman collection for the finished API
- Note 2: Application will run with docker-compose up -d --build command as it creates docker containers for both the .net core app & sqlserver-db.
- Note 3: Docker compose should work as expected. But incase it has issue, run the project without docker. 
- Note 4: NUnit to tests 

## Git clone
Clone the repo and install the dependencies.

git clone https://github.com/nazmoonnoor/GameOfChance.git
cd GameOfChance


## Project Structure
Project is built using Clean architecture having in mind. Benefit is to build a scalable, testable and maintainable application. The objective is to have the Separation of concerns. To achieve it, the Application have layers.

Clean architechture provides a domain centric approach to organizing dependencies. It has the domains be the have everything that depends on it.
Insfructure is responsible for persistance and talking to the database.
Services is a layer to bridge between Infrastucture and APIs. Also handles exceptions and validate inputs.

![GameOfChance Diagram drawio](https://user-images.githubusercontent.com/1157439/150153013-355b2f5a-be00-48bb-9daf-78d5a21a81b5.png)

## Task breakdown
- [x] Project setup with .Net Core 5
- [x] Separate the projects to API, Core, Infrastructure and Shared tools
- [x] Write Tests for Game and how players will Bet on here
- [x] Setting up Asp.Net Core Identity on the Project
- [x] Design db and setting up the ef-migration
- [x] Identify the domains and separate db-infrastucture as per Clean Architecture
- [x] Write Repositories and write related test cases
- [x] Write Controller Api and write test cases for Game Api
- [x] Add validations and handle exceptions
- [x] Run solution on docker
- [ ] Break down Identity and GameApi as separate microservices
- [ ] Write Clients for microservices
- [ ] Use dapr for service invocation and event handling

The solution is not built with Microservice architechture, even though I wish I could manage time to do it as well.

## Run the project

### DB setup
Project is built as an application which is database independent. It is using entity-framework and it is easy to switch among db vendors that is supported by entity-framework and they are relational databases like SQL-Server, Postgres, MySql, SQLite etc.
I have tested with SQLite and SQLite. And primarily it is setup with SQLite. But you can easily test with SQL-Server by changing few configs. They are:
- Add your SQL-Server ConnectionString at appsettings.json
- Open Startup class, goto ConfigureServices method and uncomment `services.ConfigureSqlServer(Configuration)` and commented out `services.ConfigureSQLite(Configuration)`.
- Remove existing Migration classes from GameOfChance.Ingrastructure project and run command `add-migration Init` on Package-Manager console.

When testing with Sqlite db, it will be created at root folder when project is run for the first time and there is no database available initially.

### Postman collection
The solution is provided with a Postman collection which included all the endpoints to test the api. Environment variable collection is also shared.
- `/api/auth/register` to register a user as player
- `/api/auth/login` to login a player, generated token is provided with the reponse. On postman token will be set at environment variable when user loggedin
- `/api/game/` - Post to play the bet
- `/api/game/:id` - Get all the previous bets by the player-id

