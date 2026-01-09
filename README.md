🍽️ Restaurant Mini Dashboard – ASP.NET Core Web API
📌 Project Overview

This project is a Restaurant Management Mini Dashboard built using ASP.NET Core Web API.
It provides a secure, role-based backend system to manage menus, orders, and users.

The API is designed to be consumed by web or mobile applications and follows clean RESTful principles.
___________________________________________________________________________________________________________________________________________________________________________________
✨ Key Features

JWT-based Authentication & Authorization

Role-based access control (Admin / Staff)

Menu Management

Order Management

Secure API endpoints

Clean architecture using DTOs
___________________________________________________________________________________________________________________________________________________________________________________

🔐 Authentication & Authorization
Authentication

Implemented JWT Authentication

Secure login with access token

Refresh token support for session renewal

Roles

Admin

Full access to menus, orders, and users

Staff

Restricted access (orders only)

Authorization is enforced using role-based policies to control access to endpoints.
___________________________________________________________________________________________________________________________________________________________________________________

🛠️ Technologies Used
Backend

ASP.NET Core Web API

Entity Framework Core

LINQ

JWT Authentication

DTO Pattern

Database

Microsoft SQL Server
___________________________________________________________________________________________________________________________________________________________________________________

📂 API Endpoints
🔑 Authentication Endpoints
Method	Endpoint	Description
POST	/api/auth/register	Register new user
POST	/api/auth/login	User login (returns JWT & Refresh Token)
POST	/api/auth/refresh-token	Generate new access token
_______________________________________________________________________________________
👤 User Management (Admin Only)
Method	Endpoint	Description
GET	/api/users	Get all users
GET	/api/users/{id}	Get user by ID
DELETE	/api/users/{id}	Delete user
_______________________________________________________________________________________
📋 Menu Management (Admin Only)
Method	Endpoint	Description
GET	/api/menus	Get all menu items
GET	/api/menus/{id}	Get menu item by ID
POST	/api/menus	Create new menu item
PUT	/api/menus/{id}	Update menu item
DELETE	/api/menus/{id}	Delete menu item
_______________________________________________________________________________________
🧾 Order Management (Admin & Staff)
Method	Endpoint	Description
GET	/api/orders	Get all orders
GET	/api/orders/{id}	Get order details
POST	/api/orders	Create new order
POST	/api/orders/{id}/items	Add item to order
DELETE	/api/orders/{id}/items/{itemId}	Remove item from order
PUT	/api/orders/{id}/status	Update order status (Pending / Completed)
___________________________________________________________________________________________________________________________________________________________________________________
🧠 Concepts & Skills Applied

RESTful API Design

JWT Authentication & Refresh Tokens

Role-Based Authorization

DTOs & Validation

Clean Code & Layered Architecture

Secure API Development
___________________________________________________________________________________________________________________________________________________________________________________

📌 Project Type

Backend API

Role-Based Dashboard System

Portfolio / Practice Project
