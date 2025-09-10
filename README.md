# Astronomical Processing Project

## Overview

The Astronomical Processing Project provides a client-server solution for Malin Space Science Systems (MSSS) to perform essential astronomical calculations. The server is developed as an **ASP.NET Core Web API**, and the client is a **WPF application** that communicates directly with the API endpoints. The project supports calculations like star velocity, distance, temperature conversion, and black hole event horizon. The client application also offers internationalization (i18n) support with language options and UI customization features.

---

## Key Features

* **Astronomical Calculations**:

  * **Star Velocity**: Calculates velocity using Doppler shift.
  * **Star Distance**: Calculates distance using the parallax angle.
  * **Temperature Conversion**: Converts temperature from Celsius to Kelvin.
  * **Blackhole Event Horizon**: Calculates the Schwarzschild radius.

* **Client-Server Architecture**:

  * **ASP.NET Core Web API**: Provides the server-side calculations through API endpoints.
  * **WPF Client Application**: Connects directly to the API for data processing.

* **UI Customization**:

  * **Language Support**: English, French, and German language options at runtime.
  * **Visual Customization**: Support for night mode, background color, and font customization.

* **Input Validation**: Ensures all inputs are validated to prevent errors.

---

## Technologies Used

* **ASP .NET Core Web API** for the server-side services.
* **WPF** for the client-side application.
* **WPF Class Library** for shared components.
* **RESTful API Communication**: Client communicates directly with the API endpoints using HTTP requests.

---

## Requirements

### Third-Party Library: `AstroMath.DLL`

The project includes a custom third-party library, `AstroMath.DLL`, which provides the following four astronomical calculation methods:

1. **Star Velocity**: Uses the Doppler shift formula to calculate the velocity of a star.

   * Formula:
     $V = \frac{\Delta \lambda}{\lambda_o} \times C$
   * Inputs: Observed Wavelength, Rest Wavelength (both `double`).
   * Output: Velocity (`double`).

2. **Star Distance**: Uses the parallax angle to calculate the star's distance.

   * Formula:
     $D = \frac{1}{P}$
   * Input: Parallax Angle (`double` in arcseconds).
   * Output: Distance in parsecs (`double`).

3. **Temperature Conversion (Celsius to Kelvin)**: Converts Celsius temperature to Kelvin.

   * Formula:
     $K = C + 273.15$
   * Input: Temperature in Celsius (`double`).
   * Output: Temperature in Kelvin (`double`).

4. **Blackhole Event Horizon (Schwarzschild Radius)**: Calculates the event horizon for a black hole.

   * Formula:
     $R = \frac{2GM}{C^2}$
   * Input: Mass of the black hole (`double` in kg).
   * Output: Schwarzschild radius (`double` in meters).

---

### Server Application

The server application is developed using the **ASP.NET Core Web API** and provides the following:

1. **IAstroContract Interface**: Defines endpoints for the four astronomical calculations.
2. **AstroController**: Implements the API methods and integrates the `AstroMath.DLL` methods to provide calculation services.
3. **API Endpoints**:

   * `/api/Astro/velocity`: Calculate star velocity.
   * `/api/Astro/distance`: Calculate star distance.
   * `/api/Astro/temperature`: Convert temperature from Celsius to Kelvin.
   * `/api/Astro/eventHorizon`: Calculate the Schwarzschild radius.

### Client Application

The client application is a **WPF-based Windows Forms application**, which communicates directly with the ASP.NET Core API. The client includes:

1. **IAstroContract Interface**: Defined similarly to the server interface but does not reference the `AstroMath.DLL` directly.
2. **UI Components**:

   * Textboxes for input values (astronomical data).
   * A listview or datagrid to display processed information.
   * Buttons to initiate calculations.
   * Menus for language selection (English, French, German).
   * Menus for UI customization (color, font, and background selection).
   * Options for night mode and visual appearance changes.

### UI Features:

* **Language Selection**: Dynamic language change for all controls and labels at runtime.
* **Visual Customization**:

  * Change background color and font using a color dialog and font dialog respectively.
  * Night mode support for a dark-themed UI.
* **Error Handling**: All inputs are validated to ensure they meet the required criteria and avoid erroneous results.

---

## Installation & Setup

### Prerequisites

1. **.NET SDK** (version 5.0 or later)
2. **Visual Studio** (for building and running the project)
3. **WPF** and **ASP .NET Core** workloads installed in Visual Studio

### Steps to Run

1. **Clone the repository** to your local machine:

   ```bash
   git clone https://github.com/Phaosash/AstroCompute
   ```

2. **Build the Project**:

   * Open the solution in Visual Studio.
   * Build the solution to restore dependencies and compile the project.

3. **Run the Server**:

   * Run the **ASP.NET Core Web API** server.
   * The server will start listening for client requests on the configured API endpoints.

4. **Run the Client**:

   * Open the **WPF Client** project in Visual Studio.
   * Launch the application and use the UI to test astronomical calculations.
