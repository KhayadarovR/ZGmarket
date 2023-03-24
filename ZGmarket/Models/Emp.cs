using System.ComponentModel.DataAnnotations;

namespace ZGmarket.Models;

public class Emp
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Position { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Password { get; set; }
}
