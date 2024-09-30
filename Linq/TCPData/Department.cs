namespace TCPData;

public class Department
{
    public int Id { get; set; }
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public IEnumerable<Employee>? Employees { get; set; } = null;
}
