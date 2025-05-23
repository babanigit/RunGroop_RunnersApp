# 🏃‍♂️ RunGroop_RunnersApp

RunGroop is an online platform for runners. This platform helps you find and join clubs, schedule events, and meet other athletes in your area. Built with the latest .NET 9 MVC framework and PostgreSQL database.

---

## 🚀 Tech Stack

- **Frontend:** Razor Pages (.NET MVC Views)
- **Backend:** ASP.NET Core MVC (v9.0)
- **Database:** PostgreSQL (via Npgsql EF Core Provider)
- **ORM:** Entity Framework Core 9
- **Authentication:** ASP.NET Identity
- **Media Handling:** Cloudinary
- **Dependency Injection:** Built-in .NET 9 DI container

---

## 🌟 Features

- 🔍 Explore and join running clubs
- 👥 View other runners’ profiles
- 📅 Manage and schedule events
- 🗺️ Location-based discovery of clubs and athletes
- 📸 Upload profile and club images via Cloudinary
- 🔒 User authentication & authorization
- 🛠️ Admin panel for managing content

---

## 📸 Screenshots

### 🔹 All Users

![All Users](ss/ss1.png)

### 🔹 User Profile

![Profile of User](ss/ss2.png)

### 🔹 Explore Runners & Clubs

![Explore Runners and Clubs](ss/ss3.png)

---

## 📦 Dependencies

Key NuGet packages used:

- CloudinaryDotNet
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore (incl. Tools, Design)
- Npgsql.EntityFrameworkCore.PostgreSQL

## 🛠️ Installation & Setup

### 🔧 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Cloudinary Account](https://cloudinary.com/)
<!-- - Optional: Docker & Docker Compose -->

---

### 📍 Local Setup

1. **Clone the repository**

   ```bash
   git clone https://github.com/babanigit/RunGroop_RunnersApp.git
   cd RunGroop_RunnersApp
   ```

2. **Set up your PostgreSQL database** and update your `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=RunGroopDB;Username=yourusername;Password=yourpassword"
   }
   ```

3. **Configure Cloudinary credentials** in `appsettings.json`:

   ```json
   "CloudinarySettings": {
     "CloudName": "your_cloud_name",
     "ApiKey": "your_api_key",
     "ApiSecret": "your_api_secret"
   }
   ```

4. **Apply Migrations & Run App**
   ```bash
   dotnet ef database update
   dotnet run
   ```

---

### 🐳 Docker Setup (Optional)

1. **Ensure Docker & Docker Compose are installed.**

2. **Run the following command:**

   ```bash
   docker-compose up --build
   ```

3. **Visit the app**
   ```text
   http://localhost:5000/
   ```

---

## 🤝 Contributing

Contributions, issues and feature requests are welcome!
Feel free to check the [issues page](https://github.com/babanigit/RunGroop_RunnersApp/issues).

---

## 👤 Author

**Aniket Panchal**
🌐 [GitHub](https://github.com/babanigit)
✉️ aniket.vilas.panchal@gmail.com

```

```
