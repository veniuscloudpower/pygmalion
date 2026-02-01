using System.Text.Json;
using PygmalionUI.Models;

namespace PygmalionUI.Services;

public class ContactService
{
    private readonly string _filePath;
    private List<Contact> _contacts;

    public ContactService(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
            "contact.dat");
        _contacts = new List<Contact>();
        LoadContacts();
    }

    public IReadOnlyList<Contact> Contacts => _contacts.AsReadOnly();

    public void LoadContacts()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var contacts = JsonSerializer.Deserialize<List<Contact>>(json);
                _contacts = contacts ?? new List<Contact>();
            }
            else
            {
                // Initialize with sample contacts if file doesn't exist
                _contacts = new List<Contact>
                {
                    new Contact { Name = "John Doe", Email = "john.doe@example.com", Mobile = "555-0101", Phone = "555-1001" },
                    new Contact { Name = "Jane Smith", Email = "jane.smith@example.com", Mobile = "555-0102", Phone = "555-1002" },
                    new Contact { Name = "Bob Johnson", Email = "bob.johnson@example.com", Mobile = "555-0103", Phone = "555-1003" },
                    new Contact { Name = "Alice Williams", Email = "alice.williams@example.com", Mobile = "555-0104", Phone = "555-1004" }
                };
                SaveContacts();
            }
        }
        catch (JsonException)
        {
            // Invalid JSON format - start with empty list
            _contacts = new List<Contact>();
        }
        catch (IOException)
        {
            // File system error - start with empty list
            _contacts = new List<Contact>();
        }
    }

    public void SaveContacts()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_contacts, options);
            File.WriteAllText(_filePath, json);
        }
        catch (UnauthorizedAccessException)
        {
            // Permission denied - silently fail
        }
        catch (IOException)
        {
            // File system error - silently fail
        }
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
        SaveContacts();
    }

    public void UpdateContact(Contact contact)
    {
        var index = _contacts.FindIndex(c => c.Id == contact.Id);
        if (index >= 0)
        {
            _contacts[index] = contact;
            SaveContacts();
        }
    }

    public void RemoveContact(Contact contact)
    {
        _contacts.Remove(contact);
        SaveContacts();
    }

    public void RemoveContactAt(int index)
    {
        if (index >= 0 && index < _contacts.Count)
        {
            _contacts.RemoveAt(index);
            SaveContacts();
        }
    }

    public Contact? GetContactAt(int index)
    {
        if (index >= 0 && index < _contacts.Count)
        {
            return _contacts[index];
        }
        return null;
    }
}
