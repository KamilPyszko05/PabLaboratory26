using AppCore.ValueObjects;

namespace AppCore.Models;

public class Organization
{
    public required string Name { get; set; }
    
    public required OrganizationType Type { get; set; }
    
    public required string? KRS { get; set; }
    
    public required string? Website { get; set; }
    
    public required string? Mission { get; set; }

    public List<Person> Members;

    public Person? PrimaryContact { get; set; }
}