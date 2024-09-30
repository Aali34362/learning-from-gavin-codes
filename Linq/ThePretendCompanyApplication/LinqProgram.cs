using System.Collections.Generic;
using TCPData;
using TCPExtensions;

namespace ThePretendCompanyApplication;

public static class LinqProgram
{
    public static void LinqProgramMain()
    {
        List<Department> departmentList = Data._departments;
        List<Employee> employeeList = Data._employees;

        Console.WriteLine("GetEmployeeList");
        GetEmployeeList(employeeList);

        Console.WriteLine("GetDepartmentList");
        GetDepartmentList(departmentList);

        Console.WriteLine("ManipulateData");
        ManipulateData(employeeList, departmentList);

        Console.WriteLine("SelectWhereMethodOperators");
        SelectWhereMethodOperators(employeeList);

        Console.WriteLine("SelectWhereQueryOperators");
        SelectWhereQueryOperators(employeeList);

        Console.WriteLine("DeferredExecution");
        DeferredExecution(employeeList);

        Console.WriteLine("ImmediateExecution");
        ImmediateExecution(employeeList);

        Console.WriteLine("JoinMethodOperation");
        JoinMethodOperation(employeeList, departmentList);

        Console.WriteLine("JoinQueryOperation");
        JoinQueryOperation(employeeList, departmentList);

        Console.WriteLine("GroupJoinMethodOperation");
        GroupJoinMethodOperation(employeeList, departmentList);

        Console.WriteLine("GroupJoinQueryOperation");
        GroupJoinQueryOperation(employeeList, departmentList);
    }

    //Lesson 1
    private static void GetEmployeeList(List<Employee> employeeList)
    {
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

    private static void GetDepartmentList(List<Department> departmentList)
    {
        var filteredDepartments = departmentList.Where(dept => dept.ShortName == "TE" || dept.ShortName == "HR");

        foreach (var department in filteredDepartments)
        {
            Console.WriteLine($"Id: {department.Id}");
            Console.WriteLine($"Short Name: {department.ShortName}");
            Console.WriteLine($"Long Name: {department.LongName}");
            Console.WriteLine();
        }
    }

    private static void ManipulateData(List<Employee> employeeList, List<Department> departmentList)
    {
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

    //Lesson 2
    private static void SelectWhereMethodOperators(List<Employee> employeeList)
    {
        var results = employeeList.Select(e => new
        {
            FullName = e.FirstName + " " + e.LastName,
            AnnualSalary = e.AnnualSalary

        }).Where(e => e.AnnualSalary >= 50000);

        foreach (var item in results)
        {
            Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
        }
    }

    private static void SelectWhereQueryOperators(List<Employee> employeeList)
    {
        var results = from emp in employeeList
                      where emp.AnnualSalary >= 50000
                      select new
                      {
                          FullName = emp.FirstName + " " + emp.LastName,
                          AnnualSalary = emp.AnnualSalary
                      };

        foreach (var item in results)
        {
            Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
        }
    }

    private static void DeferredExecution(List<Employee> employeeList)
    {
        var results = from emp in employeeList.GetHighSalariedEmployees()
                      select new
                      {
                          FullName = emp.FirstName + " " + emp.LastName,
                          AnnualSalary = emp.AnnualSalary
                      };

        employeeList.Add(new Employee
        {
            Id = 5,
            FirstName = "Sam",
            LastName = "Davis",
            AnnualSalary = 100000.20m,
            IsManager = true,
            DepartmentId = 2
        });

        foreach (var item in results)
        {
            Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
        }
    }

    private static void ImmediateExecution(List<Employee> employeeList)
    {
        var results = (from emp in employeeList.GetHighSalariedEmployees()
                       select new
                       {
                           FullName = emp.FirstName + " " + emp.LastName,
                           AnnualSalary = emp.AnnualSalary
                       }).ToList();

        employeeList.Add(new Employee
        {
            Id = 5,
            FirstName = "Sam",
            LastName = "Davis",
            AnnualSalary = 100000.20m,
            IsManager = true,
            DepartmentId = 2
        });

        foreach (var item in results)
        {
            Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
        }
    }

    private static void JoinMethodOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = departmentList.Join(employeeList,
                department => department.Id,
                employee => employee.DepartmentId,
                (department, employee) => new
                {
                    FullName = employee.FirstName + " " + employee.LastName,
                    AnnualSalary = employee.AnnualSalary,
                    DepartmentName = department.LongName
                }
            );

        foreach (var item in results)
        {
            Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}\t{item.DepartmentName}");
        }
    }

    private static void JoinQueryOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = from dept in departmentList
                      join emp in employeeList
                      on dept.Id equals emp.DepartmentId
                      select new
                      {
                          FullName = emp.FirstName + " " + emp.LastName,
                          AnnualSalary = emp.AnnualSalary,
                          DepartmentName = dept.LongName

                      };

        foreach (var item in results)
        {
            Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}\t{item.DepartmentName}");
        }
    }

    private static void GroupJoinMethodOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = departmentList.GroupJoin(employeeList,
                dept => dept.Id,
                emp => emp.DepartmentId,
                (dept, employeesGroup) => new
                {
                    Employees = employeesGroup,
                    DepartmentName = dept.LongName
                }
            );

        foreach (var item in results)
        {
            Console.WriteLine($"Department Name: {item.DepartmentName}");
            foreach (var emp in item.Employees)
                Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
        }
    }

    private static void GroupJoinQueryOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = from dept in departmentList
                      join emp in employeeList
                      on dept.Id equals emp.DepartmentId
                      into employeeGroup
                      select new
                      {
                          Employees = employeeGroup,
                          DepartmentName = dept.LongName
                      };

        foreach (var item in results)
        {
            Console.WriteLine($"Department Name: {item.DepartmentName}");
            foreach (var emp in item.Employees)
                Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
        }

    }

    //Lesson 3
}
