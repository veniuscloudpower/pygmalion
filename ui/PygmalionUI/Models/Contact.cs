namespace PygmalionUI.Models;

/// <summary>
/// Struct for binary serialization of contact data (Pascal record-style)
/// </summary>
public struct ContactRecord
{
    public string Id;
    public string Name;
    public string Email;
    public string Mobile;
    public string Phone;
    public string Address;
    public string Notes;
    public string Website;
    public string SocialLinks;

    public ContactRecord()
    {
        Id = Guid.NewGuid().ToString();
        Name = string.Empty;
        Email = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Notes = string.Empty;
        Website = string.Empty;
        SocialLinks = string.Empty;
    }

    /// <summary>
    /// Write this record to a BinaryWriter
    /// </summary>
    public readonly void WriteTo(BinaryWriter writer)
    {
        writer.Write(Id ?? string.Empty);
        writer.Write(Name ?? string.Empty);
        writer.Write(Email ?? string.Empty);
        writer.Write(Mobile ?? string.Empty);
        writer.Write(Phone ?? string.Empty);
        writer.Write(Address ?? string.Empty);
        writer.Write(Notes ?? string.Empty);
        writer.Write(Website ?? string.Empty);
        writer.Write(SocialLinks ?? string.Empty);
    }

    /// <summary>
    /// Read a record from a BinaryReader
    /// </summary>
    public static ContactRecord ReadFrom(BinaryReader reader)
    {
        return new ContactRecord
        {
            Id = reader.ReadString(),
            Name = reader.ReadString(),
            Email = reader.ReadString(),
            Mobile = reader.ReadString(),
            Phone = reader.ReadString(),
            Address = reader.ReadString(),
            Notes = reader.ReadString(),
            Website = reader.ReadString(),
            SocialLinks = reader.ReadString()
        };
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
        return new ContactRecord
        {
            Id = Id,
            Name = Name,
            Email = Email,
            Mobile = Mobile,
            Phone = Phone,
            Address = Address,
            Notes = Notes,
            Website = Website,
            SocialLinks = SocialLinks
        };
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
