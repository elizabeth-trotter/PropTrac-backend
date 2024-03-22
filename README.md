### PropTrac Objective:
**Create an API & Database for our front end.**

### Requirements:

- Pages
    - Login
        - create manager account
        - login manager
        - login tenant
        
    - MANAGER:
        - AdminDash
            - display property review overview graph
            - display maintenance requests
            - display active tenants, open listing, properties
            - display..
            - (*no add, edit, or delete on dash)

        - Properties
            - add property

        - Property Details
            - edit property
            - delete property

        - Account
            - edit account
            - add file
            - remove file

        - Payments

        - Tenants
            - add tenant
            - edit tenant
            - remove tenant

        - Service
            - add maintenance request
            - edit maintenance request
            - remove maintenance request

    - TENANTS:
        - TenantDash
            - get file
            - add maintenance request


- Controllers / folder
    - UserController / file
        - Create user / endpoint | C
        - Login user / endpoint  | R
        - Update user / endpoint | U
        - Delete user / endpoint | D
        - Get file by user.id?

    - TenantUserController / file
        - Create tenantUser / endpoint | C
        - Login tenantUser / endpoint  | R
        - Update tenantUser / endpoint | U
        - Delete tenantUser / endpoint | D
        - Get tenantUser by firstName
        - Get tenantUser by lastName
        - Get tenantUser by email
        - Get tenantUser by phone
        - Get tenantUser by location
        - Get tenantUser by leaseType
        - Get tenantUser by leaseStart
        - Get tenantUser by leaseEnd
        - Get file by tenantUser?

    - PropertyController / file
        - Create Property / endpoint | C
        - Get Property / endpoint    | R
        - Update Property / endpoint | U
        - Delete Property / endpoint | D
        - Get Property by propId? (search functionality not on Figma)
        - Get Property by address? (key not in ERD)
        - Get Property by tenants? (search functionality not on Figma)

    - ServiceController / file (not on Figma or ERD)
        - Create service / endpoint | C
        - Get service / endpoint    | R
        - Update service / endpoint | U
        - Delete service / endpoint | D

    - PaymentsController?
        - Get payments from Stripe?


- Services / folder
    - Context / folder
        - DataContext / file

    - PasswordService / file
        - Hash Password
        - Verify HashPassword

        - UserController / file
        - Create user / endpoint | C
        - Login user / endpoint  | R
        - Update user / endpoint | U
        - Delete user / endpoint | D
        - Get file by user.id?

    - TenantUserController / file
        - Create tenantUser / endpoint | C
        - Login tenantUser / endpoint  | R
        - Update tenantUser / endpoint | U
        - Delete tenantUser / endpoint | D
        - Get tenantUser by firstName
        - Get tenantUser by lastName
        - Get tenantUser by email
        - Get tenantUser by phone
        - Get tenantUser by location
        - Get tenantUser by leaseType
        - Get tenantUser by leaseStart
        - Get tenantUser by leaseEnd
        - Get file by tenantUser?

    - PropertyController / file
        - Create Property / endpoint | C
        - Get Property / endpoint    | R
        - Update Property / endpoint | U
        - Delete Property / endpoint | D
        - Get Property by propId? (search functionality not on Figma)
        - Get Property by address? (key not in ERD)
        - Get Property by tenants? (search functionality not on Figma)

    - ServiceController / file (not on Figma or ERD)
        - Create service / endpoint | C
        - Get service / endpoint    | R
        - Update service / endpoint | U
        - Delete service / endpoint | D

    - PaymentsController?
        - Get payments from Stripe?

    
### Need to finish models (need to change ERD to match)
- Models / folder
    - UserModel / file
        - int ID
        - string Username
        - string Salt
        - string Hash

    - BlogItemModel / file (model for each blog item)
        - int ID
        - int UserID
        - string PublishedName
        - string Date
        - string Title
        - string Description
        - string Image
        - string Tags
        - string Categories
        - bool IsPublished
        - bool IsDeleted (soft delete, still in database. can be recovered.)

        - DTOs / folder (data transfer object)
            - LoginDTO
                - string Username
                - string Password
            - CreateAccountDTO / file
                - int ID = 0
                - string Username
                - string Password
            - PasswordDTO / file
                - string Salt
                - string Hash

