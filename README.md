# Fitness Tracker Application

An application for tracking gym workouts that allows users to log their training sessions and monitor progress over time.

## Demo

Watch the demo video to see the application in action: [![Fitness Tracker Demo](https://img.youtube.com/vi/W3W985UYMaY/maxresdefault.jpg)](https://youtu.be/W3W985UYMaY)

---


## Contents
- [Demo](#demo)
- [Technical Requirements](#technical-requirements)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Docker Setup](#docker-setup)
- [Contact](#contact)


---

## Technical Requirements

- **Docker**: 20.10 or newer
- **Docker Compose**: 1.29 or newer
- **Node.js**: 21.x or newer 
- **.NET SDK**: 8.0 or newer

---

## Installation

1. **Clone the repository**  
   Clone the project from GitHub:
   ```bash
   git clone https://github.com/Anja0906/FitnessApp.git
   cd FitnessApp
   ```

2. **Install dependencies for testing**  
   Angular dependencies:
   ```bash
   cd frontend
   npm install
   ```

   .NET backend dependencies:
   ```bash
   cd FitnessApp
   dotnet restore
   ```

---

## Running the Application

### Running with Docker
1. Start Docker:
   ```bash
   docker-compose up --build
   ```
   This will start:
   - Backend application at `http://localhost:32768`
   - Angular frontend at `http://localhost:4200`
   - PostgreSQL database at `localhost:5432`

---

## Testing

### Backend Tests
Run tests for the .NET application:
```bash
cd FitnessApp
dotnet test
```

### Frontend Tests
Run tests for the Angular application:
```bash
cd frontend
ng test
```

---

## Docker Setup

### Service Configuration
The Docker Compose file (`docker-compose.yml`) includes:
- **FitnessApp**: .NET backend
- **Angular-app**: Angular frontend
- **Postgres**: PostgreSQL database

### Commands
- **Build and start**:
  ```bash
  docker-compose up --build
  ```
- **Stop and remove containers**:
  ```bash
  docker-compose down
  ```

---

## Contact
For any questions, contact:  
[Anja Petković]  
Email: [anjapetkovi92@gmail.com]  
GitHub: [https://github.com/Anja0906](https://github.com/Anja0906)

