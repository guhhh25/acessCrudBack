// File: Brand.cs
using System.ComponentModel.DataAnnotations;

public class Brand
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}