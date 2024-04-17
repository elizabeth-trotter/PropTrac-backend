# PropTrac Backend

**Quick Links:**

[![Static Badge](https://img.shields.io/badge/frontend%20repo-navy?style=for-the-badge&logo=github)](https://github.com/calebsylvia/PropTrac) &ensp; [![Website](https://img.shields.io/website?url=https%3A%2F%2Fproptrac-app.vercel.app%2F&up_message=in%20development&up_color=blue&down_color=8B0000&style=for-the-badge&logo=vercel&label=frontend)](https://proptrac-app.vercel.app/) &ensp; [![Static Badge](https://img.shields.io/badge/API_Docs-black?style=for-the-badge&logo=postman)](https://documenter.getpostman.com/view/31041768/2sA3Bn5Bzt) &ensp; [![Static Badge](https://img.shields.io/badge/Database%20schema-grey?style=for-the-badge&logo=eraser-io)](https://app.eraser.io/workspace/pbzCocdYcajMIkRmPd15?origin=share)


## API 

Prop Trac's `API` was built in `C#` following the `Model-View-Controller` Architecture (MVC), and was deployed using `Azure App Services`.

- Endpoints were developed for the **internal team**
- Base URL: https://proptracapi.azurewebsites.net

> [!NOTE]
> Please view the documentation developed with `Postman` for specific endpoints/ requests.

- For API Docs, click [Here](https://documenter.getpostman.com/view/31041768/2sA3Bn5Bzt) :point_left:
&ensp;

<h4 align="center">API Documentation Preview</h4>

![Postman API Internal Documentation Preview](Images/PostmanAPIDocs.png)

<p align="right">Figure 1: Preview of published API Documentation for use with internal team, developed with Postman</p>


## Database 

Prop Trac's database is hosted in `Azure SQL Cloud` alongside the C# MVC API. The database schematic design was created with `Eraser.io`.

<h4 align="center">Entity Relationship Diagram</h4>

![Entity relationship diagram](Images/ERD_V2.4_4.15.2024.png)

<p align="right">Figure 2: Preview of ERD illustrating database schema version 2.4</p>
