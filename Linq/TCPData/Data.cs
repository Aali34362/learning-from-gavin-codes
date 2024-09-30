using Bogus;
using System.Collections;

namespace TCPData;

public class Data
{
    public static List<Department> _departments { get; private set; } = [];
    public static List<Employee> _employees { get; private set; } = [];

    static Data()
    {
        _departments = GetDepartment();
        GetDepartments();

        _employees = GetEmployee();
        GetEmployees();
    }

    #region Department
    private static Faker<Department> GetFakeDepartmentDetails() => 
        new Faker<Department>()
            .RuleFor(e => e.Id, f => f.Random.Int(1000, 9999))
        .RuleFor(e => e.LongName, f => f.Company.CompanyName())
        .RuleFor(d => d.ShortName, (f, d) => GetShortNameFromLongName(d.LongName!));

    private static string GetShortNameFromLongName(string longName)
    {
        var filteredString = new string(longName.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
        var words = filteredString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (words.Length >= 2)
            return (words[0][0].ToString() + words[1][0].ToString()).ToUpper();
        else if (words.Length == 1 && words[0].Length >= 2)
            return words[0].Substring(0, 2).ToUpper();
        else
            return "NA";
    }    

    private static List<Department> GetDepartment() => _departments = GetFakeDepartmentDetails().Generate(10);

    public static void GetDepartments()
    {
        Department department = new Department
        {
            Id = 1,
            ShortName = "HR",
            LongName = "Human Resources"
        };
        _departments.Add(department);
        department = new Department
        {
            Id = 2,
            ShortName = "FN",
            LongName = "Finance"
        };
        _departments.Add(department);
        department = new Department
        {
            Id = 3,
            ShortName = "TE",
            LongName = "Technology"
        };
        _departments.Add(department);
    }
    #endregion

    #region Employee
    private static Faker<Employee> GetFakeEmployeeDetails() => 
        new Faker<Employee>()
            .RuleFor(e => e.Id, f => f.Random.Int(1000, 9999))
        .RuleFor(e => e.FirstName, f => f.Name.FirstName())
        .RuleFor(e => e.LastName, f => f.Name.LastName())
        .RuleFor(e => e.AnnualSalary, f => f.Finance.Amount(30000, 100000))
        .RuleFor(e => e.IsManager, f => f.Random.Bool())
        .RuleFor(e => e.DepartmentId, f => f.PickRandom(_departments).Id);
    
    private static List<Employee> GetEmployee() => _employees = GetFakeEmployeeDetails().Generate(10);

    public static void GetEmployees()
    {
        Employee employee = new Employee
        {
            Id = 1,
            FirstName = "Bob",
            LastName = "Jones",
            AnnualSalary = 60000.3m,
            IsManager = true,
            DepartmentId = 2
        };
        _employees.Add(employee);
        employee = new Employee
        {
            Id = 2,
            FirstName = "Sarah",
            LastName = "Jameson",
            AnnualSalary = 80000.1m,
            IsManager = true,
            DepartmentId = 3
        };
        _employees.Add(employee);
        employee = new Employee
        {
            Id = 3,
            FirstName = "Douglas",
            LastName = "Roberts",
            AnnualSalary = 40000.2m,
            IsManager = false,
            DepartmentId = 1
        };
        _employees.Add(employee);
        employee = new Employee
        {
            Id = 4,
            FirstName = "Jane",
            LastName = "Stevens",
            AnnualSalary = 30000.2m,
            IsManager = false,
            DepartmentId = 3
        };
        _employees.Add(employee);
    }
    #endregion

    public static ArrayList GetHeterogeneousDataCollection()
    {
        ArrayList arrayList = new ArrayList();

        arrayList.Add(100);
        arrayList.Add("Bob Jones");
        arrayList.Add(2000);
        arrayList.Add(3000);
        arrayList.Add("Bill Henderson");
        arrayList.Add(new Employee { Id = 6, FirstName = "Jennifer", LastName = "Dale", AnnualSalary = 90000, IsManager = true, DepartmentId = 1 });
        arrayList.Add(new Employee { Id = 7, FirstName = "Dane", LastName = "Hughes", AnnualSalary = 60000, IsManager = false, DepartmentId = 2 });
        arrayList.Add(new Department { Id = 4, ShortName = "MKT", LongName = "Marketing" });
        arrayList.Add(new Department { Id = 5, ShortName = "R&D", LongName = "Research and Development" });
        arrayList.Add(new Department { Id = 6, ShortName = "PRD", LongName = "Production" });

        return arrayList;
    }
}
