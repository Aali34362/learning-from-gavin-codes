using TCPData;

namespace ThePretendCompanyApplication;

public static class LinqProgramLesson2
{
    public static void LinqProgramLesson2Main(List<Department> departmentList, List<Employee> employeeList)
    {
        #region Lesson 2
        Console.WriteLine("\n SelectWhereMethodOperators :");
        SelectWhereMethodOperators(employeeList);

        Console.WriteLine("\n SelectWhereQueryOperators :");
        SelectWhereQueryOperators(employeeList);

        Console.WriteLine("\n DeferredExecution :");
        DeferredExecution(employeeList);

        Console.WriteLine("\n ImmediateExecution :");
        ImmediateExecution(employeeList);

        Console.WriteLine("\n JoinMethodOperation :");
        JoinMethodOperation(employeeList, departmentList);

        Console.WriteLine("\n JoinQueryOperation :");
        JoinQueryOperation(employeeList, departmentList);

        Console.WriteLine("\n GroupJoinMethodOperation :");
        GroupJoinMethodOperation(employeeList, departmentList);

        Console.WriteLine("\n GroupJoinQueryOperation :");
        GroupJoinQueryOperation(employeeList, departmentList);
        #endregion
    }

    # region Lesson 2
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
    #endregion
}
