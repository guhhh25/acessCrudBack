using System.ComponentModel.DataAnnotations;

public class Car
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BrandId { get; set; }

    [Required]
    public string Model { get; set; }

    [Required]
    public int Year { get; set; }

    public string Color { get; set; }

    public string BrandName { get; set; }
}