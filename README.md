# PropTrac Backend :house:

**Quick Links + Status:**

[![Static Badge](https://img.shields.io/badge/Frontend%20Repo-A2D7E2?style=flat&logo=github&logoColor=black)](https://github.com/calebsylvia/PropTrac) &ensp;[![Website](https://img.shields.io/website?url=https%3A%2F%2Fproptrac-app.vercel.app%2F&up_message=in%20development&up_color=8DD394&down_color=red&style=flat&logo=vercel&label=Frontend%20App)](https://proptrac-app.vercel.app/) &ensp;[![Static Badge](https://img.shields.io/badge/API_Docs-public-lightgrey?style=flat&logo=postman&logoColor=black&labelColor=EEE2D1)](https://documenter.getpostman.com/view/31041768/2sA3Bn5Bzt) &ensp;[![Static Badge](https://img.shields.io/badge/Database%20Schema-public-lightgray?style=flat&logo=eraser-io&labelColor=DE7676)](https://app.eraser.io/workspace/pbzCocdYcajMIkRmPd15?origin=share) &ensp;![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/et120/PropTrac-backend/main_proptracapi.yml?style=flat&logo=azure-devops&label=Build%20%26%20Deploy%20API&labelColor=grey&color=8DD394)

## Overview
PropTrac represents an integrated solution designed to optimize the operational efficiency of Property Management. The team is currently working on this full stack application, and completion is projected for June 1, 2024.

- This repository houses the backend logic.
- For the **Frontend component**, please refer to [PropTrac's Repository](https://github.com/calebsylvia/PropTrac) and to observe the current state of progress, visit [PropTrac App (in development)](https://proptrac-app.vercel.app/). Login using the test accounts below.

## Test Accounts
This project is currently under development, and test accounts have been created for both Property Managers and Tenants to aid in beta testing. 

#### Seed data has been added to the database for the following test accounts:

|  | User Type | Username | Email | Password |
| --- | --- | --- | --- | --- |
| 1. | Manager | john_doe | john@example.com | JohnPass12! |
| 2. | Tenant | alice_johnson | alice@example.com | AlicePass34! |

###### ^ Test account data is for example purposes only and does not contain any real user information


## API 

PropTrac's `API` is built in `C#` with the `ASP.Net Core` framework, follows `Model-View-Controller` (MVC) architecture, and is being deployed with Microsoft's PaaS `Azure App Service`.

- Endpoints were developed for the **internal team**.
- Base URL: https://proptracapi.azurewebsites.net

> [!NOTE]
> Please reference the documentation developed in `Postman` for specific endpoints/ requests.

- To view the **Internal API Docs**, click [Here](https://documenter.getpostman.com/view/31041768/2sA3Bn5Bzt) :point_left:
&ensp;


## Database 

PropTrac's database is hosted in `Azure SQL Cloud` alongside the C# MVC API. The schematic diagram of the database was created with `Eraser.io`.

- To view the **Database Schema**, click [Here](https://app.eraser.io/workspace/pbzCocdYcajMIkRmPd15?origin=share) :point_left:
&ensp;


## PropTrac Preview
![PropTrac Login Screen](https://github.com/et120/PropTrac-backend/assets/148283439/62e049f9-9d8b-432e-8743-1f586a8176f9)

<p align="right">Figure 1: Preview of the login screen after hitting end of week 3 milestone</p>

