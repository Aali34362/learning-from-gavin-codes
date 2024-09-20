namespace Learning_OperatorOverloading;

public static class WorkerProgram
{
    public static void WorkerProgramMain()
    {
        Worker emp1 = new Worker();
        emp1.Id = 1;
        emp1.FirstName = "Bob";
        emp1.LastName = "Jones";

        Worker emp2 = new Worker();
        emp2.Id = 1;
        emp2.FirstName = "bob";
        emp2.LastName = "jones";

        if (emp1 == emp2)
        {
            Console.WriteLine("emp1 is equal to emp2");
        }
        else
        {
            Console.WriteLine("emp1 is not equal to emp2");
        }

        Console.ReadKey();
    }
}

public class Worker : IEquatable<Worker>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public static bool operator ==(Worker worker1, Worker worker2)
    {
        if (worker1 is null && worker2 is null)
            return true;

        if ((worker1 is null && worker2 is not null)
            || (worker1 is not null && worker2 is null))
            return false;


        if (worker1.Id.Equals(worker2.Id)
            && worker1.FirstName.Equals(worker2.FirstName, StringComparison.OrdinalIgnoreCase)
            && worker1.LastName.Equals(worker2.LastName, StringComparison.OrdinalIgnoreCase)
            )
        {
            return true;
        }

        return false;
    }
    public static bool operator !=(Worker worker1, Worker worker2)
    {
        if (worker1 is null && worker2 is null)
            return false;

        if ((worker1 is null && worker2 is not null)
            || (worker1 is not null && worker2 is null))
            return true;

        if (!worker1.Id.Equals(worker2.Id)
            || !worker1.FirstName.Equals(worker2.FirstName, StringComparison.OrdinalIgnoreCase)
            || !worker1.LastName.Equals(worker2.LastName, StringComparison.OrdinalIgnoreCase)
            )
        {
            return true;
        }
        return false;
    }

    public bool Equals(Worker other)
    {
        if (other is null)
            return false;

        return other.Id.Equals(this.Id)
            && other.FirstName.Equals(this.FirstName, StringComparison.OrdinalIgnoreCase)
            && other.LastName.Equals(this.LastName, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return this.Id;
    }
}
