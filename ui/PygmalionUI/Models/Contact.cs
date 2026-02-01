namespace PygmalionUI.Models;

/// <summary>
/// Struct for binary serialization of contact data (Pascal record-style)
/// Fields are readonly to prevent accidental mutation after creation.
/// </summary>
public readonly struct ContactRecord
{
    public readonly string Id;
    public readonly string Name;
    public readonly string Email;
    public readonly string Mobile;
    public readonly string Phone;
    public readonly string Address;
    public readonly string Notes;
    public readonly string Website;
    public readonly string SocialLinks;

    public ContactRecord(string id, string name, string email, string mobile, string phone, 
        string address, string notes, string website, string socialLinks)
    {
        Id = id ?? string.Empty;
        Name = name ?? string.Empty;
        Email = email ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Phone = phone ?? string.Empty;
        Address = address ?? string.Empty;
        Notes = notes ?? string.Empty;
        Website = website ?? string.Empty;
        SocialLinks = socialLinks ?? string.Empty;
    }

    /// <summary>
    /// Write this record to a BinaryWriter
    /// </summary>
    public void WriteTo(BinaryWriter writer)
    {
        writer.Write(Id);
        writer.Write(Name);
        writer.Write(Email);
        writer.Write(Mobile);
        writer.Write(Phone);
        writer.Write(Address);
        writer.Write(Notes);
        writer.Write(Website);
        writer.Write(SocialLinks);
    }

    /// <summary>
    /// Read a record from a BinaryReader
    /// </summary>
    public static ContactRecord ReadFrom(BinaryReader reader)
    {
        return new ContactRecord(
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString(),
            reader.ReadString()
        );
    }
}

public class Contact
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Mobile { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    public string Notes { get; set; } = string.Empty;
    
    public string Website { get; set; } = string.Empty;
    
    public string SocialLinks { get; set; } = string.Empty;

    public string DisplayName => string.IsNullOrWhiteSpace(Name) ? "(No Name)" : Name;
    
    public override string ToString()
    {
        var parts = new List<string> { DisplayName };
        if (!string.IsNullOrWhiteSpace(Email))
            parts.Add(Email);
        if (!string.IsNullOrWhiteSpace(Mobile))
            parts.Add(Mobile);
        return string.Join(" - ", parts);
    }

    /// <summary>
    /// Convert Contact to ContactRecord struct for binary serialization
    /// </summary>
    public ContactRecord ToRecord()
    {
        return new ContactRecord(
            Id, Name, Email, Mobile, Phone, 
            Address, Notes, Website, SocialLinks
        );
    }

    /// <summary>
    /// Create Contact from ContactRecord struct
    /// </summary>
    public static Contact FromRecord(ContactRecord record)
    {
        return new Contact
        {
            Id = record.Id,
            Name = record.Name,
            Email = record.Email,
            Mobile = record.Mobile,
            Phone = record.Phone,
            Address = record.Address,
            Notes = record.Notes,
            Website = record.Website,
            SocialLinks = record.SocialLinks
        };
    }
}
