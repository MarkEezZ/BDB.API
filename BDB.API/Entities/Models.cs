using System.ComponentModel.DataAnnotations;

public class Fine
{
    [Column("FineId")]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double OverSpeed { get; set; }
    public double FineAmount { get; set; }
    public string CarNumber { get; set; }

    [ForeignKey(nameof(Detector))]
    public int DetectorId { get; set; }
    public Detector Detector { get; set; }
}


public class Detector
{
    [Column("DetectorId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Placement Adress is required.")]
    public string PlacementAdress { get; set; } 
}


public class Gratitude
{
    [Column("GratitudeId")]
    public int Id { get; set; }
    public string? Name { get; set; } 
    public DateTime Date { get; set; }
    public int Amount { get; set; }
}


public class Admin
{
    [Column("AdminId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Name is required.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50.")]
    [MinLength(2, ErrorMessage = "Minimum length for the Name is 2.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "The Login is required.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Login is 50.")]
    [MinLength(5, ErrorMessage = "Minimum length for the Login is 5.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "The Password is required.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Password is 50.")]
    [MinLength(5, ErrorMessage = "Minimum length for the Password is 5.")]
    public string Password { get; set; }
}