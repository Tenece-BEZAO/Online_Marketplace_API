# ONLINE MARKETPLACE API 💰 🎁
The online marketplace is a platform where sellers can list their products and buyers can purchase them. This platform provides an API for external developers to integrate the marketplace into their applications.
___
## ONLINE MARKETPLACE API FEATURES:
### User Management 👨:
- Allows seller and buyer to register and login
- Manages user profile
- Authenticates user and authorize access to resources
- Allows user to update profile information

### Product Management 🎁:
- Allows seller to create product listings
- Allows buyer to search and view products
- Allows buyer to add products to cart and checkout
- Allows buyer to rate and review products
- Allows seller to manage product listings

### Order Management 📝:
- Allows buyer to view order history
- Allows buyer to track order status
- Allows seller to view and manage orders
- Allows seller to generate invoices and receipts

### Payment Management 💰:
- Integration of payment gateway provider (we used PayStack) to enable online payments
- Allows buyer to make payment for orders
- Allows seller to receive payment for products sold

### Shipping Management 🚚:
- Allows seller to manage shipping rates and policies
- Allows buyer to select their preferred shipping method
- Provides shipping tracking and status updates

___
## HOW TO RUN THE API
Follow the following steps to successfully run this API.

**Paste your system's server name on the Connection String**
- Open the *Online_Marketplace.API* project
- Open the *appsettings.json* file
- Edit the connection string and paste your system server name in the *server* value of the *sqlConnection* key.
```C#
...
"ConnectionStrings": {
    "sqlConnection": "server=(Paste your system's server name here); database=MarketPlaceDB; Integrated Security=True; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  ...
```
- Save

**Create a system environment variable**
- Open your computer's cmd window as 
an administrator
- Type the following command:
```C#
setx SECRET "OnlineMarketPlaceSecretKey" /M
```
- This is going to create a system environment variable with the name 
SECRET and the value OnlineMarketPlaceSecretKey. By using /M we specify that 
we want a system variable and not local.
4. **Go back to visual studio and run the program**
___
## TECHNOLOGY STACK 👓:
- Programming language: C#
- Web Framework: .NET Core
- Database: SQL Server

___
N/B: This project is open to future modifications.

