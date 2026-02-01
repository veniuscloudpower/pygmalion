using Terminal.Gui;
using PygmalionUI.Services;
using PygmalionUI.Models;

namespace PygmalionUI.Views;

public class ContactsView : BaseView
{
    private readonly NavigationService _navigationService;
    private readonly ContactService _contactService;
    private ListView _contactsList = null!;
    private Label _pageInfoLabel = null!;
    private int _currentPage = 0;
    private const int PageSize = 10;

    public ContactsView(NavigationService navigationService) : base("Contacts")
    {
        _navigationService = navigationService;
        _contactService = new ContactService();
        InitializeComponents();
    }

    private int TotalPages => Math.Max(1, (int)Math.Ceiling(_contactService.Contacts.Count / (double)PageSize));
    
    private List<string> GetCurrentPageContacts()
    {
        return _contactService.Contacts
            .Skip(_currentPage * PageSize)
            .Take(PageSize)
            .Select(c => c.ToString())
            .ToList();
    }

    private void InitializeComponents()
    {
        // Add and Remove buttons at the top
        var addButton = new Button("Add Contact")
        {
            X = 1,
            Y = 1
        };
        addButton.Clicked += OnAddContact;
        Add(addButton);

        var removeButton = new Button("Remove Selected")
        {
            X = Pos.Right(addButton) + 2,
            Y = 1
        };
        removeButton.Clicked += OnRemoveContact;
        Add(removeButton);

        var backButton = new Button("Back")
        {
            X = Pos.Right(removeButton) + 2,
            Y = 1
        };
        backButton.Clicked += () => _navigationService.GoBack();
        Add(backButton);

        var label = new Label("Contact List (PageUp/PageDown to navigate, Enter to edit, ESC to return)")
        {
            X = 1,
            Y = 3
        };
        Add(label);

        _contactsList = new ListView(GetCurrentPageContacts())
        {
            X = 1,
            Y = 5,
            Width = Dim.Fill() - 2,
            Height = PageSize + 1
        };
        _contactsList.OpenSelectedItem += OnContactSelected;
        _contactsList.KeyPress += OnListKeyPress;
        Add(_contactsList);

        _pageInfoLabel = new Label(GetPageInfo())
        {
            X = 1,
            Y = Pos.Bottom(_contactsList) + 1
        };
        Add(_pageInfoLabel);
    }

    private string GetPageInfo()
    {
        return $"Page {_currentPage + 1} of {TotalPages} | Total contacts: {_contactService.Contacts.Count}";
    }

    private void UpdateList()
    {
        _contactsList.SetSource(GetCurrentPageContacts());
        _pageInfoLabel.Text = GetPageInfo();
    }

    private void OnListKeyPress(KeyEventEventArgs e)
    {
        if (e.KeyEvent.Key == Key.PageDown)
        {
            if (_currentPage < TotalPages - 1)
            {
                _currentPage++;
                UpdateList();
            }
            e.Handled = true;
        }
        else if (e.KeyEvent.Key == Key.PageUp)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                UpdateList();
            }
            e.Handled = true;
        }
    }

    private void OnContactSelected(ListViewItemEventArgs args)
    {
        int actualIndex = _currentPage * PageSize + args.Item;
        var contact = _contactService.GetContactAt(actualIndex);
        if (contact != null)
        {
            ShowContactModal(contact, isNew: false);
        }
    }

    private void OnAddContact()
    {
        var newContact = new Contact();
        ShowContactModal(newContact, isNew: true);
    }

    private void ShowContactModal(Contact contact, bool isNew)
    {
        var dialog = new Dialog(isNew ? "Add New Contact" : "Edit Contact", 70, 22);
        
        // Create color schemes for focus indication
        var normalScheme = new ColorScheme
        {
            Normal = Application.Driver.MakeAttribute(Color.Black, Color.White),
            Focus = Application.Driver.MakeAttribute(Color.White, Color.Blue),
            HotNormal = Application.Driver.MakeAttribute(Color.Black, Color.White),
            HotFocus = Application.Driver.MakeAttribute(Color.White, Color.Blue)
        };

        var fields = new List<TextField>();
        int labelWidth = 12;
        int fieldWidth = 50;
        int yPos = 1;

        // Helper to create a field row
        void AddFieldRow(string labelText, string value, int y)
        {
            var lbl = new Label(labelText)
            {
                X = 1,
                Y = y
            };
            dialog.Add(lbl);

            var field = new TextField(value)
            {
                X = labelWidth + 2,
                Y = y,
                Width = fieldWidth,
                ColorScheme = normalScheme
            };
            
            // Handle Enter key as Tab and setup focus change handling
            field.KeyPress += (e) =>
            {
                if (e.KeyEvent.Key == Key.Enter)
                {
                    // Move to next field
                    var currentIndex = fields.IndexOf(field);
                    if (currentIndex >= 0 && currentIndex < fields.Count - 1)
                    {
                        fields[currentIndex + 1].SetFocus();
                    }
                    else if (currentIndex == fields.Count - 1)
                    {
                        fields[0].SetFocus();
                    }
                    e.Handled = true;
                }
            };
            
            fields.Add(field);
            dialog.Add(field);
        }

        AddFieldRow("Name:", contact.Name, yPos++);
        yPos++;
        AddFieldRow("Email:", contact.Email, yPos++);
        yPos++;
        AddFieldRow("Mobile:", contact.Mobile, yPos++);
        yPos++;
        AddFieldRow("Phone:", contact.Phone, yPos++);
        yPos++;
        AddFieldRow("Address:", contact.Address, yPos++);
        yPos++;
        AddFieldRow("Website:", contact.Website, yPos++);
        yPos++;
        AddFieldRow("Social:", contact.SocialLinks, yPos++);
        yPos++;
        AddFieldRow("Notes:", contact.Notes, yPos++);

        var instructionLabel = new Label("Enter=Next Field | ESC=Save & Close")
        {
            X = Pos.Center(),
            Y = yPos + 2
        };
        dialog.Add(instructionLabel);

        // Handle ESC to save with confirmation (no buttons)
        dialog.KeyPress += (e) =>
        {
            if (e.KeyEvent.Key == Key.Esc)
            {
                e.Handled = true;
                
                // Show confirmation dialog
                var confirmResult = MessageBox.Query(40, 7, "Save Contact", 
                    "Do you want to save the contact?", "Save", "Discard");
                
                if (confirmResult == 0) // Save
                {
                    contact.Name = fields[0].Text.ToString() ?? string.Empty;
                    contact.Email = fields[1].Text.ToString() ?? string.Empty;
                    contact.Mobile = fields[2].Text.ToString() ?? string.Empty;
                    contact.Phone = fields[3].Text.ToString() ?? string.Empty;
                    contact.Address = fields[4].Text.ToString() ?? string.Empty;
                    contact.Website = fields[5].Text.ToString() ?? string.Empty;
                    contact.SocialLinks = fields[6].Text.ToString() ?? string.Empty;
                    contact.Notes = fields[7].Text.ToString() ?? string.Empty;

                    if (isNew)
                    {
                        _contactService.AddContact(contact);
                    }
                    else
                    {
                        _contactService.UpdateContact(contact);
                    }
                }
                
                Application.RequestStop();
                UpdateList();
            }
        };

        // Set focus to first field
        if (fields.Count > 0)
        {
            fields[0].SetFocus();
        }

        Application.Run(dialog);
    }

    private void OnRemoveContact()
    {
        if (_contactsList.SelectedItem >= 0)
        {
            int actualIndex = _currentPage * PageSize + _contactsList.SelectedItem;
            if (actualIndex < _contactService.Contacts.Count)
            {
                var contact = _contactService.GetContactAt(actualIndex);
                var confirmResult = MessageBox.Query(50, 7, "Confirm Delete", 
                    $"Delete contact '{contact?.DisplayName}'?", "Delete", "Cancel");
                
                if (confirmResult == 0)
                {
                    _contactService.RemoveContactAt(actualIndex);
                    
                    // Adjust current page if necessary
                    if (_currentPage >= TotalPages && _currentPage > 0)
                    {
                        _currentPage--;
                    }
                    UpdateList();
                }
            }
        }
    }
}
