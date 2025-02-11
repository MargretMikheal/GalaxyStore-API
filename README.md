# Galaxy API

Galaxy API is a backend solution built using a three-layer architecture (Presentation, Core, and Data) for managing mobile store operations. This API provides endpoints to handle shopping and selling processes—including product management, invoice processing, inventory transfers, and role-based authentication—with a strong focus on maintainability, extendability, and performance.

---

## Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Key Features](#key-features)
- [Barcode Generation](#barcode-generation)
- [System Roles](#system-roles)
- [Default Credentials](#default-credentials)
- [Setup & Installation](#setup--installation)

---

## Project Overview

Galaxy API is designed to serve the backend needs of mobile stores. It supports:
- **Product Management:** Add, update, retrieve, and search products.
- **Invoice Processing:** Create supplier invoices (for buying) and customer invoices (for selling) with automated barcode generation.
- **Inventory Control:** Manage stock and store quantities with soft-delete support.
- **Transfer Operations:** Move items between warehouse stock and store shelves.
- **Role Management & Multilingual Responses:** The API responds in Arabic or Deutsch based on the request header.

All media files (e.g., product images, supplier documents) are stored on the server, and database migrations initialize essential stored procedures (such as for barcode generation).

---

## Architecture

Galaxy API follows a three-layer architecture:

- **Presentation Layer (GalaxyStore.API):**  
  Contains the controllers that expose RESTful endpoints. It does not include any front-end code.
  
- **Core Layer (GalaxyStore.Core):**  
  Contains business logic, service interfaces, DTOs, mapping configurations (using Mapster), and helper classes.
  
- **Data Layer (GalaxyStore.Data):**  
  Contains the database context, repository implementations (Generic Repository), Unit of Work, and migration scripts.

This separation of concerns enhances code maintainability, testability, and scalability.

---

## Key Features

- **Clean Architecture & SOLID Principles:**  
  Clear separation between presentation, business logic, and data access layers.
  
- **Robust API Endpoints:**  
  Endpoints for products, invoices, transfers, and authentication.
  
- **Barcode Generation:**  
  A custom barcode algorithm that generates unique barcodes for every item based on:
  - The first 3 digits: Current year.
  - The next 4 digits: Product serial number (as stored in the product).
  - The last 5 digits: Sequential item number for that product.
  
- **Soft Delete & Global Query Filters:**  
  The API uses soft deletion and global filters to exclude logically deleted records.
  
- **Role-based Access:**  
  The system supports multiple roles (Owner, Salesman, Manager, WareHouse) with the default functioning under the Owner role.
  
- **Multilingual Responses:**  
  Responses are localized in Arabic or Deutsch based on the request header.
  
- **Media Storage:**  
  Media files are stored on the same server under designated folders.
  
- **Stored Procedures for Performance:**  
  The project uses stored procedures for certain operations (e.g., barcode generation) to improve backend performance.

---

## Barcode Generation

The barcode for each item is generated using the following algorithm:
1. **First 3 digits:** Last three digits of the current year.
2. **Next 4 digits:** The product’s serial number (padded to 4 digits).
3. **Last 5 digits:** The sequential order of items for that product (last item code + 1, padded to 5 digits).

**Example:**  
If the current year is 2024, a product has serial "0012", and the new item is the 15th for that product, the barcode might be:  
`024001200015`

---

## System Roles

The system currently supports and may later expand to include the following roles:
- **Admin:** Full control over the system.
- **Sales:** Handles customer sales and invoice processing.
- **Manager:** Oversees daily operations and product management.


---

## Default Credentials

The system comes with a default admin account for initial access:
- **Username:** admin@galaxystore.com
- **Password:** Admin@123

---

## Setup & Installation

### Prerequisites
- **.NET 9 SDK**
- **SQL Server** (or your preferred relational database)
- **Visual Studio 2022** or **Visual Studio Code**

