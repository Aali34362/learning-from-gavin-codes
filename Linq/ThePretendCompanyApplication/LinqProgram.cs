using TCPData;
using TCPExtensions;

namespace ThePretendCompanyApplication;

public static class LinqProgram
{
    public static void LinqProgramMain()
    {
        GetEmployeeList();

        GetDepartmentList();

        ManipulateData();
    }

    private static void GetEmployeeList()
    {
        List<Employee> employeeList = Data.GetEmployees();

        var filteredEmployees = employeeList.Filter(emp => emp.AnnualSalary < 50000);

        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine($"First Name: {employee.FirstName}");
            Console.WriteLine($"Last Name: {employee.LastName}");
            Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
            Console.WriteLine($"Manager: {employee.IsManager}");
            Console.WriteLine();
        }
    }

    private static void GetDepartmentList()
    {
        List<Department> departmentList = Data.GetDepartment();

        var filteredDepartments = departmentList.Where(dept => dept.ShortName == "TE" || dept.ShortName == "HR");

        foreach (var department in filteredDepartments)
        {
            Console.WriteLine($"Id: {department.Id}");
            Console.WriteLine($"Short Name: {department.ShortName}");
            Console.WriteLine($"Long Name: {department.LongName}");
            Console.WriteLine();
        }
    }

    private static void ManipulateData()
    {
        List<Employee> employeeList = Data.GetEmployees();
        List<Department> departmentList = Data.GetDepartment();

        var resultList = from emp in employeeList
                         join dept in departmentList
                         on emp.DepartmentId equals dept.Id
                         //  where dept.ShortName == "FN" || dept.ShortName == "TE"
                         select new
                         {
                             FirstName = emp.FirstName,
                             LastName = emp.LastName,
                             AnnualSalary = emp.AnnualSalary,
                             Manager = emp.IsManager,
                             Department = dept.LongName
                         };

        foreach (var employee in resultList)
        {
            Console.WriteLine($"First Name: {employee.FirstName}");
            Console.WriteLine($"Last Name: {employee.LastName}");
            Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
            Console.WriteLine($"Manager: {employee.Manager}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine();
        }

        var averageAnnualSalary = resultList.Average(a => a.AnnualSalary);
        var highestAnnualSalary = resultList.Max(a => a.AnnualSalary);
        var lowestAnnualSalary = resultList.Min(a => a.AnnualSalary);

        Console.WriteLine($"Average Annual Salary: {averageAnnualSalary}");
        Console.WriteLine($"Highest Annual Salary: {highestAnnualSalary}");
        Console.WriteLine($"Lowest Annual Salary: {lowestAnnualSalary}");

        Console.ReadKey();
    }
}
