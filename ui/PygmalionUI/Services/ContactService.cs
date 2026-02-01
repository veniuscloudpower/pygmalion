using PygmalionUI.Models;

namespace PygmalionUI.Services;

public class ContactService
{
    private readonly string _filePath;
    private List<Contact> _contacts;
    
    // Binary file header magic number to identify valid contact.dat files
    private const int FileHeader = 0x434F4E54; // "CONT" in hex

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
                using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(stream);
                
                // Read and verify file header
                int header = reader.ReadInt32();
                if (header != FileHeader)
                {
                    // Invalid file format - start with empty list
                    _contacts = new List<Contact>();
                    return;
                }
                
                // Read record count
                int count = reader.ReadInt32();
                _contacts = new List<Contact>(count);
                
                // Read each contact record
                for (int i = 0; i < count; i++)
                {
                    var record = ContactRecord.ReadFrom(reader);
                    _contacts.Add(Contact.FromRecord(record));
                }
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
        catch (EndOfStreamException)
        {
            // Corrupted file - start with empty list
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
            using var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(stream);
            
            // Write file header
            writer.Write(FileHeader);
            
            // Write record count
            writer.Write(_contacts.Count);
            
            // Write each contact record
            foreach (var contact in _contacts)
            {
                var record = contact.ToRecord();
                record.WriteTo(writer);
            }
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
