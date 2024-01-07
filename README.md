### The API uses
    - .NET 7 (C# 11)
    - PostgreSQL
    - Firebase Storage to store images for categories
    - Swagger API that allows to test endpoints
    - JWT Authentication
    - Hangfire job to add points every day at 4 am
    - Fluent Validation to validate objects that are being passed in controllers
    - Fluent API to validate objects on a db layer

### To start working with the API, open git bash and run command:
    git clone https://github.com/Mike-Opanasiuk/TestTask_Wallet.git

### After that, open project directory and run command:
    dotnet run

### Afterwards, database will be created automatically. There exists a seeder that will add by default:
    - User Roles ("User");
    - Transaction Types ("Payment", "Credit");
    - Transaction Statuses ("Pending", "Approved", "Canceled");
    - Transaction Categories ("Apple", "IKEA", "Target", "Other");

### [Hint] To test API, make some simple requests:

#### 1. Register user

`curl -X 'POST' \
  'https://localhost:5443/api/Account/register' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "name": "Mike",
  "userName": "mmm1ke",
  "password": "Qwerty_1"
}'`

####  2. Save the returned JWT token for the future authentication (or insert it in Swagger popup window that will appear after click on the right top button [Authorize])

####  3. Create a card (don't forget to replace a token with your token. Otherwise, you will get 401 (Unauthorized) status code)

`curl -X 'POST' \
  'https://localhost:5443/api/Cards/create' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImU1YmZjMDYzLTliYzctNGQ4YS1hMGM0LTQxYzU3ZDE0ODFhNCIsInJvbGVzIjoiVXNlciIsIm5iZiI6MTcwNDYzNTAyMCwiZXhwIjoxNzA0Njc4MjIwLCJpYXQiOjE3MDQ2MzUwMjB9.O7mX6Jdqw-Uz-N2XP42X_x-k43h7vKzYnI2WOV8qeqICaRgch9igyuE4Xuaoun3QrzlHAEsiRnq-LJU8rEj0aw' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "balance": 70
}'`

#### 4. Create some transactions for the card we have just added. For example:

`curl -X 'POST' \
  'https://localhost:5443/api/Transactions/create' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImU1YmZjMDYzLTliYzctNGQ4YS1hMGM0LTQxYzU3ZDE0ODFhNCIsInJvbGVzIjoiVXNlciIsIm5iZiI6MTcwNDYzNTAyMCwiZXhwIjoxNzA0Njc4MjIwLCJpYXQiOjE3MDQ2MzUwMjB9.O7mX6Jdqw-Uz-N2XP42X_x-k43h7vKzYnI2WOV8qeqICaRgch9igyuE4Xuaoun3QrzlHAEsiRnq-LJU8rEj0aw' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "sum": 30,
  "description": "some expense",
  "typeId": "E4C50F39-577B-4D68-AD2A-78B27B3B0563",
  "categoryId": "F4827760-69A0-48C6-8DDC-46301A7099A9",
  "statusId": "6C45F007-AE6B-4684-9AD4-59CB8B8BB38B",
  "cardId": "245a8a14-883b-4e48-8785-266613a6c261"
}'`

#### 5. And now you can view your card information:

`curl -X 'GET' \
  'https://localhost:5443/api/Cards?Id=245a8a14-883b-4e48-8785-266613a6c261' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImU1YmZjMDYzLTliYzctNGQ4YS1hMGM0LTQxYzU3ZDE0ODFhNCIsInJvbGVzIjoiVXNlciIsIm5iZiI6MTcwNDYzNTAyMCwiZXhwIjoxNzA0Njc4MjIwLCJpYXQiOjE3MDQ2MzUwMjB9.O7mX6Jdqw-Uz-N2XP42X_x-k43h7vKzYnI2WOV8qeqICaRgch9igyuE4Xuaoun3QrzlHAEsiRnq-LJU8rEj0aw'`

####  6. Or just view full information of a particular transaction:

`curl -X 'GET' \
  'https://localhost:5443/api/Transactions/3a4a89a7-4f92-4296-8f1b-d990e258370f' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImU1YmZjMDYzLTliYzctNGQ4YS1hMGM0LTQxYzU3ZDE0ODFhNCIsInJvbGVzIjoiVXNlciIsIm5iZiI6MTcwNDYzNTAyMCwiZXhwIjoxNzA0Njc4MjIwLCJpYXQiOjE3MDQ2MzUwMjB9.O7mX6Jdqw-Uz-N2XP42X_x-k43h7vKzYnI2WOV8qeqICaRgch9igyuE4Xuaoun3QrzlHAEsiRnq-LJU8rEj0aw'`
