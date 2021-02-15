# clean-arch-crud

This project is a microservice using clean architecture
<br /><br />

## How to Configure

<br />

1. First run the script that is in the Scripts folder at the root of the project to create the database (Use SQL Server)
<br /><br />

2. Edit <b>appsettings.json</b> file in <b>CleanArchCRUD.Api</b>, change ```"CleanArchCRUD"``` into ```"ConnectionStrings"``` with your database connection string, and change ```"Issuer"``` and ```"Audience"``` with the url of your application
<br /><br />

3. Execute Application
<br /><br />

## How to Use

<br />

1. If you run the script to create the database, there is a registered user, you need to use a database user to authenticate to the api and generate a Token, send a post reuest to <b>https://localhost:44367/api/auth</b> url with a json as in the example below
<br /><br />

```json
    {
        "email": "admin@teste.com",
        "password": "admin"
    }
```
<br />

2. Copy the generated token and add it in the header of your requests to the api, add as <b>Bearer Token</b>
<br /><br />

3. After configuring the token it will be possible to make requests for all routes
<br /><br />

## Routes

1. No token
    * [<span style="color:green;">Post</span>] <b>/api​/Auth</b> Example Request:

    ```json
    {
        "email": "admin@teste.com",
        "password": "admin"
    }
    ```

    <br />
    * [<span style="color:blue;">Patch</span>] <b>/api/Auth</b> Example Request:

    ```json
    {
        "email": "teste@gmail.com",
        "oldpassword": "teste",
        "newpassword": "teste"
    }
    ```

    <br />

2. Token Required
   * [<span style="color:#92a8d1;">Get</span>] <b>/api/User</b>
    <br />

    * [<span style="color:#92a8d1;">Get</span>] <b>/api/User/{id}</b> Example Request: <b>/api/User/1</b>
    <br />

    * [<span style="color:green;">Post</span>] <b>/api/User</b> Example Request:

    ```json
    {
        "name": "Teste",
        "email": "teste@gmail.com",
        "password": "teste"
    }
    ```    
    <br />

    * [<span style="color:orange;">Put</span>] <b>/api/User</b> Example Request:

    ```json
    {
        "userId": 1,
        "name": "Teste",
        "email": "teste@gmail.com",
        "password": "teste"
    }
    ```    
    <br />

    * [<span style="color:red;">Delete</span>] <b>/api/User/{id}</b> Example Request: <b>/api/User/1</b>