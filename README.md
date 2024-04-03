# PropTrac Backend


## API

**Details:** API is deployed on Azure: https://proptracapi.azurewebsites.net

> [!IMPORTANT]
> Please view the table below for specific endpoints/ requests. 


<h2 align="center">PropTrac API Endpoints</h2>

| Description | HTTP Method | Endpoint[^1] | Parameter Type (Body or URL) | Parameter Requirements |
| ------------- | :-------------: | ------------- | ------------- |
| Create an Account (Manager or Tenant)  | `POST`  | /User/AddUser  | Body | int ID, string Username, string Password, string Email, bool IsManager, string FirstName, string LastName |
| Login (Manager or Tenant) | `POST`  | /User/Login  | Body | string UsernameOrEmail, string Password |

[^1]: All fields within { } are required to make an API call


## Database

**Details:** Database is Azure SQL. (Basic Free Plan)

<h2 align="center">Entity Relationship Diagram</h2>

![Entity relationship diagram](ERD_V2_3.25.2024.png)
<p align="right">Figure 1: ERD illustrating database schema updated March 25, 2024</p>