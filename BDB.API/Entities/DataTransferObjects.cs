using System.ComponentModel.DataAnnotations;

public class FineDto
{
    public double OverSpeed { get; set; }
    public string CarNumber { get; set; }
    public int DetectorId { get; set; }
}

public class DetectorDto
{
    public string PlacementAdress { get; set; }
}

public class GratitudeDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }
}


public class AdminDto
{
    public string UserName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}