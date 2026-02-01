# Pygmalion UI

A terminal-based UI application for managing contacts and files, built with Terminal.Gui and .NET 10.

## Features

- **Terminal GUI Interface**: Modern terminal-based user interface with a green color scheme
- **Main Menu**: Navigate between Contacts and Files management
- **Contact Management**: 
  - View list of contacts
  - Add new contacts with name and email
  - Remove contacts from the list
- **File Management**:
  - View list of files
  - Add new files
  - Remove files from the list
- **Navigation**: ESC key to go back to the main menu from any view
- **Clean Architecture**: Follows the pattern from catapeltUI with:
  - Service layer (NavigationService)
  - View layer with base class inheritance
  - Separation of concerns

## Requirements

- .NET 10.0 SDK

## Building and Running

### Build the application
```bash
cd ui/PygmalionUI
dotnet build
```

### Run the application
```bash
dotnet run
```

### Show help
```bash
dotnet run -- --help
```

## Project Structure

```
ui/PygmalionUI/
├── Program.cs                          # Main entry point
├── App.cs                              # Application and main menu logic
├── PygmalionUI.csproj                  # Project file with dependencies
├── Services/
│   └── NavigationService.cs           # Navigation between views
└── Views/
    ├── BaseView.cs                    # Base class for all views
    ├── ContactsView.cs                # Contact management view
    └── FilesView.cs                   # File management view
```

## Usage

1. Launch the application
2. Use arrow keys and Enter to navigate the menu
3. Select "Contacts" to manage contacts
4. Select "Files" to manage files
5. Press ESC to return to the main menu
6. Select "Exit" to quit the application

## Dependencies

- Terminal.Gui 1.19.0 - Terminal UI toolkit for .NET

## License

This project is part of the Pygmalion repository.
