# EComJwtCrud

EComJwtCrud is a sample **ASP.NET Core Web API** project implementing **JWT authentication**, **user registration**, and **stateless refresh tokens**.  
It follows a **Clean Architecture** approach with layers: API, Application, Domain, and Infrastructure.
Contain Three Entities
1. User
2. Product
3. Category

---

## **Project Structure**

```EComJwtCrud/
│
├─ EComJwtCrud.API # Web API project (controllers, startup)
├─ EComJwtCrud.Application # Services, DTOs, interfaces
├─ EComJwtCrud.Domain # Entities and domain logic
├─ EComJwtCrud.Infrastructure # Repositories, UnitOfWork, data access
└─ EComJwtCrud.sln # Solution file
```


---

## **Prerequisites**

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)  
- SQL Server / LocalDB (or configure your preferred DB)  
- Git (for version control)  
- Docker (optional, for containerized setup)

---

## **Setup (Local)**

1. **Clone the repository**
   ```
    git clone https://github.com/Alimul-Mahfuz/EComJwtCrud
   ```

3. **Edit appsetting.json

```{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=EComJwtCrudDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "EComJwtCrud",
    "Audience": "EComJwtCrud",
    "ExpiryMinutes": "15"
  }
}
```

3. **Restore NuGet Package

   ```
   dotnet restore
   ```
5. **Run Migration
```
   dotnet ef database update --project EComJwtCrud.Infrastructure --startup-project EComJwtCrud.API
```

