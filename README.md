# **DevSpot**

A job posting center for developer roles, implemented using ASP.NET, Entity Framework, SQL Server, and more. *(Work in progress...)*

## **Table of Contents**

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## **About the Project**

DevSpot is designed to streamline the process of posting and managing job listings for developer roles. This platform aims to connect employers with potential developer candidates efficiently.

## **Technologies Used**

- **ASP.NET**
- **Entity Framework**
- **SQL Server**
- **C#**
- **HTML**
- **CSS**
- **JavaScript**

## **Getting Started**

### **Prerequisites**

Ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### **Installation**

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/sydmasina/DevSpot.git
   cd DevSpot
   ```

2. **Restore Dependencies:**

   ```bash
   dotnet restore
   ```

### **Database Setup**

1. **Configure the Connection String:**

   Update the `appsettings.json` file in the `DevSpot` project with your SQL Server details:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=DevSpotDB;Trusted_Connection=True;"
   }
   ```

2. **Apply Migrations:**

   ```bash
   dotnet ef database update
   ```

### **Running the Application**

- **Using the .NET CLI:**

  ```bash
  dotnet run --project DevSpot
  ```

- **Using Visual Studio:**

  - Open `DevSpot.sln`.
  - Set `DevSpot` as the startup project.
  - Press `F5` to run the application.

## **Project Structure**

```
DevSpot/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── appsettings.json
├── Program.cs
└── Startup.cs
```

## **Contributing**

Contributions are welcome! Please follow these steps:

1. **Fork the Repository**
2. **Create a Feature Branch:**

   ```bash
   git checkout -b feature/YourFeatureName
   ```

3. **Commit Your Changes:**

   ```bash
   git commit -m 'Add Your Feature'
   ```

4. **Push to the Branch:**

   ```bash
   git push origin feature/YourFeatureName
   ```

5. **Open a Pull Request**

## **License**

This project is licensed under the MIT License.
