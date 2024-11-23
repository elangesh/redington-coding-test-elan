# Probability Calculator

This project was developed as part of the Redington Developer Test. It's a probability calculator tool that allows investment consultants to enter probabilities and perform basic calculations.

## Project Overview

The solution consists of two main components:

1. **Frontend Application** (`/app`)
   - Built with React 18 and Vite
   - Provides user interface for probability calculations

2. **Backend API** (`/api`)
   - Built with .NET Core Web API (.NET 8)
   - Handles calculation logic and validation

## Features

- Input validation for probability values (0 to 1)
- Two calculation functions:
  1. **CombinedWith**: P(A)P(B) e.g., 0.5 * 0.5 = 0.25
  2. **Either**: P(A) + P(B) – P(A)P(B) e.g., 0.5 + 0.5 – 0.5*0.5 = 0.75
- Logging functionality for calculations
- Clean and maintainable architecture
- Unit tests

## Prerequisites

- Node.js and npm
- .NET 8 SDK
- IDE (Visual Studio 2022 recommended for API development)

## Installation & Setup

### Backend API Setup

1. Navigate to the `api` folder
2. Open the Redington.ProbabilityCalculator.sln solution in Visual Studio
3. Run the application using IIS Express
   - This will open Swagger UI in your default browser
   - Note the API base URL (e.g., `https://localhost:44335/`)

### Frontend Setup

1. Navigate to the `app` folder
2. Configure the API base URL:
   - Locate `.env.development` file in the root of `app` folder
   - Update the base URL to match your running API
   - Example: `https://localhost:44335/api`
   - **Important**: Do not include a trailing slash after 'api'
   - ✅ Correct: `https://localhost:44335/api`
   - ❌ Incorrect: `https://localhost:44335/api/`

3. Install dependencies and run the application:
   ```bash
   npm install
   npm run dev
   ```

4. The React application should now be running and accessible in your browser

## Project Structure

```
├── app/                # Frontend React application
│   ├── src/           # Source files
│   └── .env.development   # Environment configuration
│
├── api/               # Backend .NET Core Web API
    └── Redington.ProbabilityCalculator.Api/           # Source files
```

## Technical Stack

### Frontend
- React 18
- Vite
- Modern JavaScript/TypeScript

### Backend
- .NET 8
- ASP.NET Core Web API
- C#

## Contributing

Please refer to the original requirements document for coding standards and expectations.

## Notes

- No authentication/authorization is implemented as per requirements
- No database is used; logging is done via text files
- The application focuses on maintainable code structure and extensibility