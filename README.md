# ProgramUpdater

ProgramUpdater is a C# desktop application designed to identify installed programs on Windows, detect changes between saved and current states, check for newer versions online (via Winget), and assist the user in updating them easily.

## Features

### Current Features

- Detects installed programs through Windows Registry (LocalMachine and CurrentUser).
- Saves the program list into a JSON file (ProgramSaver).
- Compares current state with saved state (ProgramComparer).
- Identifies new, removed, or modified programs (different versions).
- Checks for newer versions available via **Winget**.
- Displays results in the console.
- Asks the user if they want to update programs one by one.
- Executes automatic updates via **Winget**.

### Future Features (Roadmap)

- Graphical user interface (Windows Forms).
- Integration with update sources beyond Winget (official sites, APIs).
- Notification system for new available updates.
- Scheduled automatic updates.
- Automatic backups of program lists in multiple formats (JSON, CSV, XML).
- Detailed reports on update history and changes.
- Option to ignore specific applications in the check.

## System Requirements

- **Operating System:** Windows 10 or higher  
- **Framework:** Microsoft .NET 6 or higher  
- **Dependencies:** Winget installed and configured  
- **Permissions:** Administrator rights for installing/updating programs  

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ProgramUpdater.git
2. Open the solution in Visual Studio (2022 or later).
3. Build and run the project.

##Usage

- Run ProgramUpdater.
- The system scans installed programs.
- If a previous JSON list exists, it will be loaded for comparison.
- Differences (new, removed, changed programs) are displayed.
- Online version check is performed via Winget.
- The user is asked whether to update each outdated program.
- Updates are executed as chosen.
- The JSON file can be updated with the new state.

##Technologies

- C# / .NET 6
- Windows Registry
- JSON serialization
- Winget