using Terminal.Gui;
using PygmalionUI.Services;
using PygmalionUI.Views;

namespace PygmalionUI;

public class App
{
    private Toplevel _toplevel = null!;
    private Window _mainWindow = null!;
    private FrameView _menuFrame = null!;
    private NavigationService _navigationService = null!;

    public void Run()
    {
        Application.Init();
        
        _toplevel = Application.Top;

        _mainWindow = new Window("Pygmalion UI - Terminal Dashboard")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = new ColorScheme
            {
                Normal = Application.Driver.MakeAttribute(Color.Black, Color.BrightGreen),
                Focus = Application.Driver.MakeAttribute(Color.White, Color.Green),
                HotNormal = Application.Driver.MakeAttribute(Color.Black, Color.BrightGreen),
                HotFocus = Application.Driver.MakeAttribute(Color.White, Color.Green)
            }
        };
        
        _toplevel.Add(_mainWindow);
        
        _navigationService = new NavigationService(_mainWindow, ShowMainMenu);
        
        ShowMainMenu();
        
        Application.Run();
        Application.Shutdown();
    }

    private void ShowMainMenu()
    {
        // Remove existing menu if present
        if (_menuFrame != null)
        {
            _mainWindow.Remove(_menuFrame);
        }

        // Create centered menu frame
        _menuFrame = new FrameView("Main Menu")
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = 40,
            Height = 8,
            ColorScheme = new ColorScheme
            {
                Normal = Application.Driver.MakeAttribute(Color.White, Color.Green),
                Focus = Application.Driver.MakeAttribute(Color.Black, Color.BrightGreen),
                HotNormal = Application.Driver.MakeAttribute(Color.White, Color.Green),
                HotFocus = Application.Driver.MakeAttribute(Color.Black, Color.BrightGreen)
            }
        };

        var menuItems = new[]
        {
            "Contacts",
            "Files"
        };

        var menuItemColorScheme = new ColorScheme
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Green),
            Focus = Application.Driver.MakeAttribute(Color.Black, Color.BrightGreen),
            HotNormal = Application.Driver.MakeAttribute(Color.White, Color.Green),
            HotFocus = Application.Driver.MakeAttribute(Color.Black, Color.BrightGreen)
        };

        int menuWidth = 38; // Inner width of the frame
        for (int i = 0; i < menuItems.Length; i++)
        {
            int index = i; // Capture for closure
            string text = $"{menuItems[i]} ->";
            int padding = (menuWidth - text.Length) / 2;
            string centeredText = new string(' ', padding) + text;
            
            var label = new Label(centeredText)
            {
                X = 0,
                Y = i,
                Width = Dim.Fill(),
                CanFocus = true,
                ColorScheme = menuItemColorScheme
            };
            label.MouseClick += (e) => OnMenuItemSelected(index);
            label.KeyPress += (e) =>
            {
                if (e.KeyEvent.Key == Key.Enter)
                {
                    OnMenuItemSelected(index);
                    e.Handled = true;
                }
            };
            _menuFrame.Add(label);
        }

        // Add exit button
        string exitText = "Exit ->";
        int exitPadding = (menuWidth - exitText.Length) / 2;
        string centeredExitText = new string(' ', exitPadding) + exitText;
        
        var exitLabel = new Label(centeredExitText)
        {
            X = 0,
            Y = menuItems.Length + 1,
            Width = Dim.Fill(),
            CanFocus = true,
            ColorScheme = menuItemColorScheme
        };
        exitLabel.MouseClick += (e) => Application.RequestStop();
        exitLabel.KeyPress += (e) =>
        {
            if (e.KeyEvent.Key == Key.Enter)
            {
                Application.RequestStop();
                e.Handled = true;
            }
        };
        _menuFrame.Add(exitLabel);

        _mainWindow.Add(_menuFrame);
    }

    private void OnMenuItemSelected(int index)
    {
        // Hide the menu
        _mainWindow.Remove(_menuFrame);

        // Create and navigate to the appropriate view
        BaseView view = index switch
        {
            0 => new ContactsView(_navigationService),
            1 => new FilesView(_navigationService),
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

        _navigationService.NavigateTo(view);
    }
}
