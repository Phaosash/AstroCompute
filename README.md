# Astronomical Processing

The **Astronomical Processing** project is a .NET-based application designed to provide astronomical calculations such as star velocity, star distance, temperature conversion, and black hole event horizon. The application follows an Agile development methodology and uses Windows Communication Foundation (WCF) for inter-process communication (IPC) between clients and the server.

This project aims to modernize the existing system at **Malin Space Science Systems (MSSS)**, replacing the current socket-based system with a more flexible IPC-based solution.

## Features

* **Astronomical Calculations**: Perform calculations on star velocity, star distance, temperature conversion (Celsius to Kelvin), and black hole event horizon.
* **Multi-Language Support**: Users can select from English, French, or German as the language of the client application during runtime.
* **UI Customization**: The client application supports dynamic UI customizations, including day/night mode toggle, color schemes, and font/size changes.
* **IPC Communication**: Client and server communicate using Windows Communication Foundation (WCF) via Named Pipes, ensuring efficient and reliable data transfer.
* **Error Handling & Validation**: Proper input validation for each calculation and error handling to ensure that erroneous results are avoided.
* **Cross-Location Support**: The application is designed to support teams from different global locations (UK, France, Germany) seamlessly.

## Installation

### Prerequisites

* .NET Framework 9.0 or higher
* Visual Studio 2019 or later
* Git (for version control)

### Steps

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/Phaosash/Astro-Compute.git
   ```

2. Open the solution in **Visual Studio**.

3. Build the project by clicking on **Build > Build Solution**.

4. Run the application:

   * The **server** will be run as a console application.
   * The **client** will open a Windows Forms UI for calculation and customization.

### Dependencies

* **AstroMath.DLL**: Custom third-party library for performing astronomical calculations. Ensure the correct version of the DLL is referenced in your project.
* **WCF Libraries**: For implementing inter-process communication (IPC).

## Usage

Once the project is set up, users can perform the following:

1. **Select the Language**: Choose between English, French, and German from the language dropdown in the client application.
2. **Input Data**: Enter the required values in the text fields (e.g., star distance, velocity, etc.).
3. **Calculate**: Press the corresponding button for the desired calculation (e.g., "Calculate Star Velocity").
4. **View Results**: The results will be displayed in a read-only text box in scientific notation.
5. **Customize UI**: Use the options for switching between night/day modes, adjusting colors, or changing fonts and sizes.

### Example Calculation

* **Star Velocity**: Enter a value for the star's distance and the gravitational force to compute its velocity.

## Contributing

We welcome contributions to the **Astronomical Processing** project! If you'd like to contribute, follow these steps:

1. Fork the repository.
2. Create a new branch for your feature (`git checkout -b feature/YourFeature`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add feature'`).
5. Push to the branch (`git push origin feature/YourFeature`).
6. Create a pull request describing your changes.

---

## Agile Development Process

This project follows an **Agile** development process. Key elements include:

* **Sprints**: Short iterative cycles of 1-2 weeks where new features are developed and tested.
* **Kanban Board**: A GitHub Project is used to track progress, with columns for tasks in various stages (e.g., To Do, In Progress, Testing, Done).
* **Daily Standups**: Short daily meetings to align the team and resolve blockers.
* **Sprint Reviews & Retrospectives**: End-of-sprint reviews to assess progress and identify areas for improvement.
