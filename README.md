# CafeDuCoin

---

## Prerequisites

```
Download & Install Postgresql (https://www.postgresql.org/download/)
Create a postgres user with these credentials (You can use another profile but you'll have to change the appsettings.json in CafeDuCoinAPI project
Username : postgres
Password : root

Run this command on your postgres instance
#CREATE DATABASE cafeducoin_db;
```

---

## How to run - Server Side

```
Go to /CafeDuCoinAPI/bin/Release/net8.0/
Run CafeDuCoinAPI.exe
Check Swagger at http://localhost:5000/swagger/index.html

PS : You can run the API directly from Visual Studio but you'll have to change the baseURL in /cafeducoin/src/services/axios.js to match with the new port
```

Execute a POST request (via SwaggerUI) on endpoint /games/mock in order to create some random games in the database
You can make sure it worked by calling GET /games endpoint

Only /loans/manage/{gameName} endpoint needs an authentication

In order to call /loans/ endpoint, you must register via /users/register endpoint then login via /users/login, you'll obtain a JWT token that can be used as follow to call /loans :
Bearer {JWT_TOKEN}

---

## How to run - Client Side

```
Go to /cafeducoin
Open the folder with Visual Studio Code
Run these commands :
npm install
npm run serve
Open http://localhost:8080/ on your local browser
```

### Login Page
You'll be asked to connect :
  - If you have created an accound with the /users/register endpoint, you can log in directly.
  - If not, click on "register" and process to a new registration. If registration is complete, you'll be redirected on the loggin page. (Password needs at least 8 length, 1 number, 1 uppercase, 1 lowercase and 1 special chars)

### Games List Page
After the connection you'll be redirected to Game List page, each game is stored in the database. Only the image is randomized.
If a game is available, the background will be "white".
If the game is already borrowed, the background will be "grey".
The status (available / unavailable) is displayed on the game card too.
You can check the loans history & games details by clicking the "View Details" button.

### Games Details Page
From this page you'll be able to see the loans history of a specific game, with borrower name, loan date and return date.
If a game is available you'll see a button allowing you to borrow it. This action will reflects in the loan history array & on the games list page.
If you borrowed a game, you'll be able to return it by clicking on "Return" button on this same page.
If a game was borrowed by someone else and not returned yet, you can't see the "Return" button. You'll only see the name of the last borrower.
