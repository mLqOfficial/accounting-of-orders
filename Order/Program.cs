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
class Repository
{
    public List<Order> Orders { get; set; } = new List<Order>();
    public void AddOrder(Order order)
    {
        Orders.Add(order);
    }
    public Order Read(int id)
    {
        return Orders.ToList().Find(x => x.Id == id);
    }
    public List<Order> ReadAll()
    {
        return Orders.ToList();
    }
    public void DeleteOrder(int id)
    {
        Orders.Remove(Read(id));
    }
    public int CompleteOrders()
    {
        return Orders.Count(o => o.Status == Status.Complete);
    }
    public TimeSpan AverageExecutionTime()
    {
        var completeOrders = Orders.Where(o => o.Status == Status.Complete);
        if (completeOrders.Any())
        {
            return TimeSpan.FromSeconds(completeOrders.Average(o => (o.EndDate - o.StartDate).Seconds));
        }
        return TimeSpan.Zero;
    }
    public Dictionary<string, int> ProblemTypeStatictics()
    {
        return Orders.ToList()
           .GroupBy(o => o.ProblemType)
           .ToDictionary(g => g.Key, g => g.Count());
    }
}