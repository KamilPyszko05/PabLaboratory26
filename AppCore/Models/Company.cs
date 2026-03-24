namespace AppCore.Models;

public class Company : EntityBase
{
    public required string Name { get; set; }
    public required string? NIP { get; set; }
    public required string? REGON { get; set; }
    public required string? KRS { get; set; }
    public required string? Industry { get; set; }
    public required int? EmployeeCount { get; set; }
    public required decimal? AnnualRevenue { get; set; }
    public required string? Website { get; set; }
    private List<Person> Employees;
    private Person? PrimaryContact { get; set; }
}