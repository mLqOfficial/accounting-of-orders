var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
public enum Status
{
    InWaiting, InProcess, Complete
}
class Order
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Device { get; set; }
    public string ProblemType { get; set; }
    public string Description { get; set; }
    public string Client { get; set; }
    public string Master { get; set; }
    public string Comment { get; set; }

    private Status status;
    public Status Status { get; set; }
    public Order() { } 
    public Order(string device, string problemType, string description, string client)
    {
        Id = IdChek++;
        StartDate = DateTime.Now;
        EndDate = DateTime.MinValue;
        Device = device;
        ProblemType = problemType;
        Description = description;
        Client = client;
        Master = "";
        Comment = "";
        Status = Status.InWaiting;
    }
    public static int IdChek { get; set; } = 1;
}