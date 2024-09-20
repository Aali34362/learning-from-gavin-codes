namespace Learning_OperatorOverloading;

public static class EmployeeProgram
{
    public static void EmployeeProgramMain()
    {
        Employee employee1 = new Employee { Id = 1, FirstName = "Bob", LastName = "Jones" };
        Employee employee2 = new Employee { Id = 1, FirstName = "Bob", LastName = "Jones" };

        if (employee1 == employee2)
        {
            Console.WriteLine($"employee1 is equal to employee2");
        }
        else
        {
            Console.WriteLine($"employee1 is not equal to employee2");
        }
    }
}

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public static bool operator ==(Employee emp1, Employee emp2)
    {
        if (emp1.Id == emp2.Id)
            return true;
        return false;

    }
    public static bool operator !=(Employee emp1, Employee emp2)
    {
        if (emp1.Id != emp2.Id)
            return true;
        return false;
    }
    public override bool Equals(object obj)
    {
        return (obj as Employee).Id.Equals(this.Id);
    }
    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}
