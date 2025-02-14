# InvestMaster

InvestMaster is a web application designed to manage investments, allowing users to monitor the market, buy and sell assets, and analyze financial performance. The project utilizes .NET for the backend and React for the frontend.

## Technologies

- **Backend:** .NET (ASP.NET Core)
- **Frontend:** React
- **Database:** PostgreSQL
- **Containerization:** Docker
- **UI Framework:** Tailwind CSS / Material UI

## Features

- **User Authentication:** Login, registration.
- **Market Overview:** Real-time quotes for cryptocurrencies, stocks, and ETFs.
- **Asset Management:** Buy and sell assets.
- **User Dashboard:** Overview of earnings and portfolio analysis.
- **Search and Filtering:** Efficiently search and filter assets.

## Installation and Setup

### Requirements

- .NET 7+
- Node.js 18+
- Docker (optional)

### Backend Setup

1. Navigate to the backend directory:
   ```sh
   cd backend
   
2. Install dependencies:
   ```sh
   dotnet restore

3. Run the application:
   ```sh
   dotnet run

### Frontend Setup

1. Navigate to the frontend directory:
   ```sh
   cd frontend

2. Install dependencies:
   ```sh
   npm install

3. Run the application:
   ```sh
   npm run dev

### Running with Docker

1. Build and run the containers:
   ```sh
   docker-compose up --build

