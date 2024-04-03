# PropTrac Backend


## API

**Details:** API is deployed on Azure: https://proptracapi.azurewebsites.net

> [!IMPORTANT]
> Please view the table below for specific endpoints/ requests. 


<h4 align="center">PropTrac API Endpoints</h4>

| Description | HTTP Method | Endpoint | Parameter Type (Body or URL) | Parameter Requirements |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| Create an Account *(Manager or Tenant)*  | `POST`  | /User/AddUser  | Body | int ID, string Username, string Password, string Email, bool IsManager, string FirstName, string LastName |
| Login *(Manager or Tenant)* | `POST`  | /User/Login  | Body | string UsernameOrEmail, string Password |

<p align="right">Table 1: API endpoints description updated April 3, 2024</p>


## Database

**Details:** Database is Azure SQL. (Basic Free Plan)

<h4 align="center">Entity Relationship Diagram</h4>

![Entity relationship diagram](ERD_V2_3.25.2024.png)

<p align="right">Figure 1: ERD illustrating database schema updated March 25, 2024</p>
