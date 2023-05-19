using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DetectorsConfiguration : IEntityTypeConfiguration<Detector>
{
    public void Configure(EntityTypeBuilder<Detector> builder)
    {
        builder.HasData
        (
            new Detector
            {
                Id = 1,
                PlacementAdress = "Nezavisimosty, 176",
            },
            new Detector
            {
                Id = 2,
                PlacementAdress = "Surganova, 22",
            },
            new Detector
            {
                Id = 3,
                PlacementAdress = "Kolasa, 55",
            }
        );
    }
}

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasData
        (
            new Admin
            {
                Id = Guid.NewGuid(),
                UserName = "Главный админ",
                Login = "TheFirstAdmin",
                Password = "TFA335544",
            }
        );
    }
}