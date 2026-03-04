using AppCore.ValueObjects;

namespace AppCore.Models;

public class Address
{
    public required string Street { get; set; }
    
    public required string City { get; set; }
    
    public required string PostalCode { get; set; }
    
    public required Country Country { get; set; }
    
    public required AddressType Type { get; set; }
    

    
}