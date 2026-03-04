using AppCore.ValueObjects;

namespace AppCore.Models;

public class Person
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string MiddleName { get; set; }
    
    public required DateTime? BirthDate { get; set; }
    
    public required Gender Gender { get; set; }
    
    public required string? Position { get; set; }
    
    public required Organization? Organization { get; set; }
    
    public required Company? Employer { get; set; }
    
}