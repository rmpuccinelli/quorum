# Quorum Legislative Analysis

A web application for analyzing legislative voting patterns using CSV data files.

> Time spent on implementation: 10 hours

## Prerequisites

- .NET 9.0 SDK
- Node.js (v18 or later)
- Angular CLI (`npm install -g @angular/cli`)

### Installing Prerequisites

#### Installing Node.js
1. Visit [Node.js official website](https://nodejs.org/)
2. Download and install the LTS version (v18 or later)
3. Verify installation:
```bash
node --version
npm --version
```

#### Installing .NET 9.0 SDK
1. Visit [.NET 9.0 download page](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Download and install the SDK for your operating system
3. Verify installation:
```bash
dotnet --version
```

#### Installing Angular CLI
1. After installing Node.js, open a terminal/command prompt
2. Install Angular CLI globally:
```bash
npm install -g @angular/cli
```
3. Verify installation:
```bash
ng version
```

## Project Structure

```
src/
├── Quorum.Api/         # Backend API
├── Quorum.Client/      # Angular frontend
├── Quorum.Application/ # Business logic
├── Quorum.Model/       # Domain models
├── Quorum.Infrastructure/ # Data access
└── Quorum.Tests/       # Test projects
```

## Setup & Running

### Backend (API)

1. Navigate to the API project:
```bash
cd src/Quorum.Api
```

2. Place your CSV files in the `Data` folder:
- legislators.csv
- bills.csv
- votes.csv
- vote_results.csv

3. Run the API:
```bash
dotnet run
```

The API will be available at `http://localhost:5000`

### Frontend (Angular)

1. Navigate to the client project:
```bash
cd src/Quorum.Client
```

2. Install dependencies:
```bash
npm install
```

3. Run the application:
```bash
ng serve
```

The application will be available at `http://localhost:4200`

## Running Tests

```bash
cd src/Quorum.Tests
dotnet test
```

## CSV File Requirements

The application expects the following CSV file formats:

### legislators.csv
```
id,name
1,Legislator Name
```

### bills.csv
```
id,title,sponsor_id
1,Bill Title,1
```

### votes.csv
```
id,bill_id
1,1
```

### vote_results.csv
```
id,legislator_id,vote_id,vote_type
1,1,1,1
```

## Configuration

API settings can be modified in `src/Quorum.Api/appsettings.json`:
```json
{
  "DataSettings": {
    "FolderPath": "Data",
    "Legislators": {
      "FileName": "legislators.csv"
    },
    ...
  }
}
```

## Features

- View legislator voting records
- Analyze bill support patterns
- Real-time data updates
- Error handling and validation
- Responsive UI design

## Error Handling

- Invalid CSV format errors are displayed in the UI
- Missing files are reported with clear error messages
- Data validation errors show specific failure reasons 