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

        // Store original values to detect dirty state
        var originalValues = new Dictionary<string, string>
        {
            { "Name", contact.Name },
            { "Email", contact.Email },
            { "Mobile", contact.Mobile },
            { "Phone", contact.Phone },
            { "Address", contact.Address },
            { "Website", contact.Website },
            { "SocialLinks", contact.SocialLinks },
            { "Notes", contact.Notes }
        };

        // Dictionary to map field names to TextFields for maintainability
        var fieldMap = new Dictionary<string, TextField>();
        var fieldOrder = new List<string>();
        int labelWidth = 12;
        int fieldWidth = 50;
        int yPos = 1;

        // Helper to create a field row
        void AddFieldRow(string fieldName, string labelText, string value, int y)
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
            
            // Handle Enter key as Tab
            field.KeyPress += (e) =>
            {
                if (e.KeyEvent.Key == Key.Enter)
                {
                    // Move to next field
                    var currentIndex = fieldOrder.IndexOf(fieldName);
                    if (currentIndex >= 0 && currentIndex < fieldOrder.Count - 1)
                    {
                        fieldMap[fieldOrder[currentIndex + 1]].SetFocus();
                    }
                    else if (currentIndex == fieldOrder.Count - 1)
                    {
                        fieldMap[fieldOrder[0]].SetFocus();
                    }
                    e.Handled = true;
                }
            };
            
            fieldMap[fieldName] = field;
            fieldOrder.Add(fieldName);
            dialog.Add(field);
        }

        AddFieldRow("Name", "Name:", contact.Name, yPos++);
        yPos++;
        AddFieldRow("Email", "Email:", contact.Email, yPos++);
        yPos++;
        AddFieldRow("Mobile", "Mobile:", contact.Mobile, yPos++);
        yPos++;
        AddFieldRow("Phone", "Phone:", contact.Phone, yPos++);
        yPos++;
        AddFieldRow("Address", "Address:", contact.Address, yPos++);
        yPos++;
        AddFieldRow("Website", "Website:", contact.Website, yPos++);
        yPos++;
        AddFieldRow("SocialLinks", "Social:", contact.SocialLinks, yPos++);
        yPos++;
        AddFieldRow("Notes", "Notes:", contact.Notes, yPos++);

        var instructionLabel = new Label("Enter=Next Field | ESC=Close")
        {
            X = Pos.Center(),
            Y = yPos + 2
        };
        dialog.Add(instructionLabel);

        // Helper function to check if form is dirty
        bool IsFormDirty()
        {
            foreach (var field in fieldMap)
            {
                var currentValue = field.Value.Text.ToString() ?? string.Empty;
                if (currentValue != originalValues[field.Key])
                {
                    return true;
                }
            }
            return false;
        }

        // Handle ESC with dirty check
        dialog.KeyPress += (e) =>
        {
            if (e.KeyEvent.Key == Key.Esc)
            {
                // Check if form is dirty (has unsaved changes)
                if (IsFormDirty())
                {
                    // Show confirmation dialog for dirty form
                    var confirmResult = MessageBox.Query(40, 7, "Unsaved Changes", 
                        "You have unsaved changes.\nDo you want to save?", "Save", "Discard", "Cancel");
                    
                    if (confirmResult == 0) // Save
                    {
                        e.Handled = true;
                        contact.Name = fieldMap["Name"].Text.ToString() ?? string.Empty;
                        contact.Email = fieldMap["Email"].Text.ToString() ?? string.Empty;
                        contact.Mobile = fieldMap["Mobile"].Text.ToString() ?? string.Empty;
                        contact.Phone = fieldMap["Phone"].Text.ToString() ?? string.Empty;
                        contact.Address = fieldMap["Address"].Text.ToString() ?? string.Empty;
                        contact.Website = fieldMap["Website"].Text.ToString() ?? string.Empty;
                        contact.SocialLinks = fieldMap["SocialLinks"].Text.ToString() ?? string.Empty;
                        contact.Notes = fieldMap["Notes"].Text.ToString() ?? string.Empty;

                        if (isNew)
                        {
                            _contactService.AddContact(contact);
                        }
                        else
                        {
                            _contactService.UpdateContact(contact);
                        }
                        
                        Application.RequestStop();
                        UpdateList();
                    }
                    else if (confirmResult == 1) // Discard
                    {
                        e.Handled = true;
                        Application.RequestStop();
                        UpdateList();
                    }
                    // Cancel (confirmResult == 2) - do nothing, return to form
                }
                else
                {
                    // If not dirty, just close without prompt
                    e.Handled = true;
                    Application.RequestStop();
                    UpdateList();
                }
            }
        };

        // Set focus to first field
        if (fieldOrder.Count > 0)
        {
            fieldMap[fieldOrder[0]].SetFocus();
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
