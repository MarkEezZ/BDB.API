public class Fine
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Place { get; set; }
    public int OverSpeed { get; set; }
    public int FineAmount { get; set; }

    [ForeignKey("Cars")]
    public Guid CarID { get; set; }
}
