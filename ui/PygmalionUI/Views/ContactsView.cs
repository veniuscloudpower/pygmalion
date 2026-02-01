using Terminal.Gui;
using PygmalionUI.Services;

namespace PygmalionUI.Views;

public class ContactsView : BaseView
{
    private readonly NavigationService _navigationService;
    private ListView _contactsList = null!;
    private List<string> _contacts;

    public ContactsView(NavigationService navigationService) : base("Contacts")
    {
        _navigationService = navigationService;
        _contacts = new List<string>
        {
            "John Doe - john.doe@example.com",
            "Jane Smith - jane.smith@example.com",
            "Bob Johnson - bob.johnson@example.com",
            "Alice Williams - alice.williams@example.com"
        };

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        var label = new Label("Contact List (Press ESC to return to main menu)")
        {
            X = 1,
            Y = 1
        };
        Add(label);

        _contactsList = new ListView(_contacts)
        {
            X = 1,
            Y = 3,
            Width = Dim.Fill() - 2,
            Height = Dim.Fill() - 8
        };
        Add(_contactsList);

        var addButton = new Button("Add Contact")
        {
            X = 1,
            Y = Pos.Bottom(_contactsList) + 1
        };
        addButton.Clicked += OnAddContact;
        Add(addButton);

        var removeButton = new Button("Remove Selected")
        {
            X = Pos.Right(addButton) + 2,
            Y = Pos.Bottom(_contactsList) + 1
        };
        removeButton.Clicked += OnRemoveContact;
        Add(removeButton);

        var backButton = new Button("Back")
        {
            X = Pos.Right(removeButton) + 2,
            Y = Pos.Bottom(_contactsList) + 1
        };
        backButton.Clicked += () => _navigationService.GoBack();
        Add(backButton);
    }

    private void OnAddContact()
    {
        var nameLabel = new Label("Name:")
        {
            X = 1,
            Y = 1
        };

        var nameField = new TextField("")
        {
            X = Pos.Right(nameLabel) + 1,
            Y = 1,
            Width = 30
        };

        var emailLabel = new Label("Email:")
        {
            X = 1,
            Y = 3
        };

        var emailField = new TextField("")
        {
            X = Pos.Right(emailLabel) + 1,
            Y = 3,
            Width = 30
        };

        var dialog = new Dialog("Add New Contact", 50, 10);
        dialog.Add(nameLabel, nameField, emailLabel, emailField);

        var okButton = new Button("OK");
        okButton.Clicked += () =>
        {
            if (!string.IsNullOrWhiteSpace(nameField.Text.ToString()) && 
                !string.IsNullOrWhiteSpace(emailField.Text.ToString()))
            {
                string newContact = $"{nameField.Text} - {emailField.Text}";
                _contacts.Add(newContact);
                _contactsList.SetSource(_contacts);
                Application.RequestStop();
            }
        };

        var cancelButton = new Button("Cancel");
        cancelButton.Clicked += () => Application.RequestStop();

        dialog.AddButton(okButton);
        dialog.AddButton(cancelButton);

        Application.Run(dialog);
    }

    private void OnRemoveContact()
    {
        if (_contactsList.SelectedItem >= 0 && _contactsList.SelectedItem < _contacts.Count)
        {
            _contacts.RemoveAt(_contactsList.SelectedItem);
            _contactsList.SetSource(_contacts);
        }
    }
}
