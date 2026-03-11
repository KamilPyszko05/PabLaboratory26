namespace AppCore.Models;

public class Contact: EntityBase
{
    public required int Id { get; set; }
    
    public required string Email { get; set; }
    
    public required string Phone { get; set; }
    
    public required Address Address { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public required DateTime UpdatedAt { get; set; }
    
    public required ContactStatus Status { get; set; }
    
    
    
    
}