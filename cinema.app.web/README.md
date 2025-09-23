# 🎬 Cinema Booking App

A prototype cinema ticket booking system with a React + TypeScript frontend and a backend API built using ASP.NET 8 Web API.
The application provides an interactive seat map where users can:

🎟 View available seats in real time.
🟩 Select and book seats, with instant visual feedback.
🔎 Search bookings using a unique booking reference ID.
🛡 Prevent double booking by disabling already reserved seats.
📝 Display confirmation messages with booking details.

Key Features

Modern Frontend: Built with React, TypeScript, Axios, and Bootstrap for a responsive UI.
Robust Backend: ASP.NET 8 Web API with EF Core for data persistence and MediatR for clean CQRS patterns.
Validation: Server-side and client-side checks to ensure proper booking requests.

Testing:

Unit testing with xUnit for repositories and handlers.
Extensible Architecture: Clean separation of concerns (Minimal API, Repositories, Models), making it easy to extend with more new features.

## ✨ Features

- 🎟 Seat selection grid with rows (A–H) and seats (1–10).  
- ✅ Seat states:  
  - Green → selected  
  - Dark → booked  
  - Red → highlighted (search view mode)  
- 🔎 Search for bookings by booking reference.  
- 🖥 Frontend: React + TypeScript + Bootstrap + Axios.  
- ⚙️ Backend: .NET 8 Web API.  
- 🧪 Testing: xUnit (backend).  


## 📂 Project Structure

Cinema.App/
│── tests/
│   ├── cinema.app.web.cypress.test/
│   └── cinema.app.web.tests/
│
│── web/
│   └── cinema.app.web/
│       ├── ClientApp/
│       │   ├── app/
│       │   │   ├── api/
│       │   │   ├── components/
│       │   │   ├── routes/
│       │   │   └── utils/
│       │   ├── app.css
│       │   ├── root.tsx
│       │   └── routes.ts
│       │
│       ├── Features/
│       │   └── Booking/
│       │       ├── DTOs/
│       │       ├── Handlers/
│       │       ├── Profiles/
│       │       └── Repositories/
│       │
│       ├── Infrastructure/
│       │   ├── Configuration/
│       │   ├── Contracts/
│       │   |── Entities/
│       │	└── CinemaContext.cs
│       |
│       ├── Program.cs
│       ├── appsettings.json
│       └── README.md


## 🚀 How to Setup

### Prerequisites

- **.NET SDK 8.0 or later**  
  Check version:  
  ```bash
  dotnet --version
  ```

- **Node.js 18+ and npm**  
  Check version:  
  ```bash
  node -v
  npm -v
  ```

### Setup code

First copy the zip file to the desired directory. Then unzip it to access the project files.


### Build the Project

Build the backend API:

```
navigate to Cinema.App 
dotnet build
```

OR 

```
navigate to Cinema.App 
open Cinema.App.sln using VS
build the project
```


Build the frontend:

```
navigate to Cinema.App -> cinema.app.web.client -> ClentApp
npm install
npm run build
```

### Run the Application

Start the backend API:

```bash
navigate to Cinema.App 
dotnet run --project cinema.app.web
```

Start the frontend :

```bash
navigate to Cinema.App -> cinema.app.web.client -> ClentApp
npm run dev
```

The app will be available at:  
👉 **Frontend**: `http://localhost:5173`  
👉 **Backend API**: `https://localhost:5001/api`


## ⚙️ How it Works

### Getting Started

- On launch, the user is presented with a **seat map grid** (rows `A–H`, seats `1–10`).  
- Seat states:  
  - **Available** → white border  
  - **Selected** → green  
  - **Booked** → dark grey  
  - **Search Highlight** → red  

### Booking Seats

1. Select one or more available seats.  
2. Click **Confirm Booking**.  
3. A booking reference is generated (e.g., `GIC-0001`).  
4. Selected seats turn **booked**. (X)

### Searching Bookings

- Enter a booking reference in the **search box**.  
- Matching seats are highlighted in **red**.  
- All seats are disabled to prevent changes in **view mode**.  

---

## 📊 Output

- After confirming a booking:  
  - A unique **Booking Reference ID** is displayed.  
  - Selected seats are updated in the grid.  
- After searching for a booking:  
  - The booking reference and seat map are shown in **read-only mode**.  

---

## 💡 Usage

- This web application can be used to manage seat reservations for a cinema.  
- The backend is cleanly structured using **CQRS + MediatR**, **Repository Pattern**, and **EF Core** with InMemory Database.  
- The frontend is modular with **React + Bootstrap**

---

## 🧪 Testing

### Unit Testing (xUnit)

- The backend follows a Domain-Driven Design (DDD) approach with a Vertical Slice architecture, keeping each feature self-contained (e.g., Booking with its own DTOs, Handlers, Repositories, and Validators). Some of the test cases are developed using a Test-Driven Development (TDD) style, where tests are written before implementation to guide the design.
- Tests cover:  
  - Booking repository  
  - Query/command handlers  
  - Validators  

Run tests:

```bash
dotnet test
```

