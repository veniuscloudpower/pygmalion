using Terminal.Gui;

namespace PygmalionUI.Services;

public class NavigationService
{
    private readonly Stack<View> _viewStack = new();
    private readonly Toplevel _toplevel;
    private readonly Action _showMainMenu;

    public NavigationService(Toplevel toplevel, Action showMainMenu)
    {
        _toplevel = toplevel;
        _showMainMenu = showMainMenu;
    }

    public void NavigateTo(View view)
    {
        // Hide current view if any
        if (_viewStack.Count > 0)
        {
            var currentView = _viewStack.Peek();
            _toplevel.Remove(currentView);
        }

        // Push new view onto stack
        _viewStack.Push(view);
        _toplevel.Add(view);

        // Setup Esc key handling for the view
        view.KeyPress += (e) =>
        {
            if (e.KeyEvent.Key == Key.Esc)
            {
                GoBack();
                e.Handled = true;
            }
        };
    }

    public void GoBack()
    {
        if (_viewStack.Count > 0)
        {
            var currentView = _viewStack.Pop();
            _toplevel.Remove(currentView);
            currentView.Dispose();
        }

        if (_viewStack.Count > 0)
        {
            // Show previous view
            var previousView = _viewStack.Peek();
            _toplevel.Add(previousView);
        }
        else
        {
            // No more views, show main menu
            _showMainMenu();
        }
    }

    public void ClearStack()
    {
        while (_viewStack.Count > 0)
        {
            var view = _viewStack.Pop();
            _toplevel.Remove(view);
            view.Dispose();
        }
    }

    public bool HasViews => _viewStack.Count > 0;
}
