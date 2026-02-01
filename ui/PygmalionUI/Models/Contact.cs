using System.Text.Json.Serialization;

namespace PygmalionUI.Models;

public class Contact
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("mobile")]
    public string Mobile { get; set; } = string.Empty;
    
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;
    
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;
    
    [JsonPropertyName("website")]
    public string Website { get; set; } = string.Empty;
    
    [JsonPropertyName("socialLinks")]
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
}
