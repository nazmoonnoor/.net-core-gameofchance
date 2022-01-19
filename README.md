# Game of Chance - A RESTful API solution built with .Net Core, Sqlite-db OR SQLServer-db

## Tools
- Using docker and docker-compose, so that the project can be run in any machine with little effort.
- Secure the API with AspNetCore.Identity and JWT
- Postman to execute the endpoints
- NUnit to unit tests

## Requirements
- Asp.Net Core 5
- Git
- Docker (optional)

Notes
Note 1: This repository includes the postman collection for the finished API
Note 2: Application will run with docker-compose up -d --build command as it creates docker containers for both the .net core app & sqlserver-db.
Note 3: Docker compose should work as expected. But incase it has issue, run the project without docker. 

## Git clone
Clone the repo and install the dependencies.

git clone https://github.com/nazmoonnoor/domain-adviser-api.git
cd GameOfChance


## Project Structure
Project is build using Clean architecture having in mind. Benefit is to build a scalable, testable and maintainable application. 
The objective is to have the Separation of concerns. 
To achieve it, the Application have layers.
