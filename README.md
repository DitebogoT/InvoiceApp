# Invoice Management System

A full-featured invoice management web application built with ASP.NET Core MVC and Entity Framework Core.
NB: This application is still in production. I started it on the 13th of October 25

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-8.0-purple)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple)
![License](https://img.shields.io/badge/license-MIT-green)

## Overview

This Invoice Management System allows businesses to manage their invoicing workflow efficiently. Create invoices, manage customers and products, track payments, and maintain a complete history of transactions.

## Features

- **Dashboard** - Real-time overview of invoices, revenue, and statistics
- **Invoice Management** - Create, edit, view, and delete invoices
- **Customer Management** - Maintain customer database with contact information
- **Product/Service Management** - Manage products and services catalog
- **Automatic Calculations** - Auto-calculate subtotals, taxes, discounts, and totals
- **Invoice Numbering** - Auto-generated sequential invoice numbers
- **Status Tracking** - Track invoice status (Draft, Sent, Paid, Overdue, Cancelled)
- **Responsive Design** - Works seamlessly on desktop, tablet, and mobile devices
- **Data Validation** - Client-side and server-side validation
- **Modern UI** - Clean, professional interface with Bootstrap 5

## Technologies Used

- **Framework**: ASP.NET Core MVC 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server (LocalDB/Express/Full)
- **Frontend**: Bootstrap 5.3, Bootstrap Icons
- **Validation**: jQuery Validation
- **Language**: C# 10

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community, Professional, or Enterprise)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full version)
- [Git](https://git-scm.com/downloads) (optional, for cloning)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/invoice-app.git
cd invoice-app
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Update Database Connection String

Edit `appsettings.json` and update the connection string if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InvoiceAppDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

**Alternative connection strings:**

For SQL Server Express:
```
Server=.\\SQLEXPRESS;Database=InvoiceAppDb;Trusted_Connection=true;MultipleActiveResultSets=true
```

For SQL Server with authentication:
```
Server=localhost;Database=InvoiceAppDb;User Id=sa;Password=YourPassword;MultipleActiveResultSets=true
```

### 4. Run Database Migrations

**Using Package Manager Console (Visual Studio):**
```powershell
Add-Migration InitialCreate
Update-Database
```

**Using .NET CLI:**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the Application

**Using Visual Studio:**
- Press `F5` or click the "Run" button

**Using .NET CLI:**
```bash
dotnet run
```

The application will start at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

## Project Structure

```
InvoiceApp/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── InvoicesController.cs
│   ├── CustomersController.cs
│   └── ProductsController.cs
├── Data/                  # Database Context
│   └── ApplicationDbContext.cs
├── Models/                # Domain Models
│   ├── Customer.cs
│   ├── Invoice.cs
│   ├── InvoiceItem.cs
│   ├── InvoiceStatus.cs
│   └── Product.cs
├── Services/              #Email Service Configuration
|   ├── EmailServices.cs/
|   ├── IEmailServices.cs/
├── ViewModels/            # View Models
│   ├── InvoiceViewModel.cs
|   ├── AccountViewModels.cs
│   └── DashboardViewModel.cs
├── Views/                 # Razor Views
│   ├── Home/
│   ├── Invoices/
│   ├── Customers/
│   ├── Products/
|   ├── Account/
│   └── Shared/
├── wwwroot/              # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
├── appsettings.json      # Configuration
├── Program.cs            # Application Entry Point
└── README.md            # This file
```

## Usage

### Creating an Invoice

1. Navigate to **Invoices** → **Create New Invoice**
2. Select a customer from the dropdown
3. Set invoice date and due date
4. Add invoice items by selecting products
5. Adjust quantities and prices as needed
6. Set tax rate and discount (optional)
7. Add notes (optional)
8. Click **Create Invoice**

### Managing Customers

1. Navigate to **Customers**
2. Click **Add Customer** to create a new customer
3. Fill in customer information
4. Click **Create Customer**

### Managing Products

1. Navigate to **Products**
2. Click **Add Product** to create a new product/service
3. Enter product name, description, and price
4. Click **Create Product**

## Database Schema

### Tables

- **Customers** - Customer information
- **Products** - Product/service catalog
- **Invoices** - Invoice headers
- **InvoiceItems** - Invoice line items

### Relationships

- One Customer → Many Invoices
- One Invoice → Many InvoiceItems
- One Product → Many InvoiceItems

## 🔧 Configuration

### Changing Tax Rate

Edit the default tax rate in `Models/Invoice.cs`:

```csharp
public decimal TaxRate { get; set; } = 15; // Change to your default tax rate
```

### Customizing Invoice Number Format

Edit the `GenerateInvoiceNumber()` method in `Controllers/InvoicesController.cs`:

```csharp
private string GenerateInvoiceNumber()
{
    var lastInvoice = _context.Invoices.OrderByDescending(i => i.Id).FirstOrDefault();
    var nextNumber = (lastInvoice?.Id ?? 0) + 1;
    return $"INV-{DateTime.Now.Year}-{nextNumber:D5}";
}
```

## Future Enhancements

- [ ] PDF invoice generation
- [ ] Email invoice functionality
- [ ] User authentication and authorization
- [ ] Payment tracking (partial/full payments)
- [ ] Recurring invoices
- [ ] Reports and analytics
- [ ] Multi-currency support
- [ ] Export to Excel/CSV
- [ ] Invoice templates
- [ ] Search and advanced filtering
- [ ] API endpoints for mobile apps
- [ ] Document attachments

## 🐛 Troubleshooting

### Database Connection Issues

**Problem**: Cannot connect to database

**Solution**: 
- Ensure SQL Server is running
- Check connection string in `appsettings.json`
- Verify database name doesn't conflict with existing databases

### Migration Issues

**Problem**: Migration fails

**Solution**:
```bash
# Remove the last migration
dotnet ef migrations remove

# Create a new migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update
```

### Port Already in Use

**Problem**: Port 5000/5001 is already in use

**Solution**: Edit `Properties/launchSettings.json` and change the port numbers

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Author

Your Name - [@yourhandle](https://twitter.com/yourhandle)

Project Link: [https://github.com/DitebogoT/invoice-app](https://github.com/DitebogoT/invoice-app)

## Acknowledgments

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core)
- [Bootstrap](https://getbootstrap.com)
- [Bootstrap Icons](https://icons.getbootstrap.com)

## Support

For support, email diditlhakola@.com or open an issue in the repository.

---

⭐ If you found this project helpful, please give it a star!
