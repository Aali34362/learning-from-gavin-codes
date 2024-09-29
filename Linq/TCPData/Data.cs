using Bogus;

namespace TCPData;

public static class Data
{
    private static Faker<Department> GetFakeDepartmentDetails() => 
        new Faker<Department>()
            .RuleFor(e => e.Id, f => f.Random.Int(1000, 9999))
        .RuleFor(e => e.LongName, f => f.Company.CompanyName())
        .RuleFor(d => d.ShortName, (f, d) => GetShortNameFromLongName(d.LongName!));

    private static string GetShortNameFromLongName(string longName)
    {
        var words = longName.Split(' ');
        if (words.Length >= 2)
            return (words[0][0].ToString() + words[1][0].ToString()).ToUpper();
        else if (words.Length == 1 && words[0].Length >= 2)
            return words[0].Substring(0, 2).ToUpper();
        else
            return "NA";
    }

    public static List<Department> GetDepartment() => GetFakeDepartmentDetails().Generate(10);

    private static List<Department> _departments = GetDepartment();

    private static Faker<Employee> GetFakeEmployeeDetails() => 
        new Faker<Employee>()
            .RuleFor(e => e.Id, f => f.Random.Int(1000, 9999))                     // Random ID between 1000 and 9999
        .RuleFor(e => e.FirstName, f => f.Name.FirstName())                    // Random first name
        .RuleFor(e => e.LastName, f => f.Name.LastName())                      // Random last name
        .RuleFor(e => e.AnnualSalary, f => f.Finance.Amount(30000, 100000))          // Random salary between 30,000 and 100,000
        .RuleFor(e => e.IsManager, f => f.Random.Bool())
        .RuleFor(e => e.DepartmentId, f => f.PickRandom(_departments).Id);
    
    public static List<Employee> GetEmployees() => GetFakeEmployeeDetails().Generate(10);
}
