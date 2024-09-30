using System.Collections;
using TCPData;
using TCPExtensions;

namespace ThePretendCompanyApplication;

public static class LinqProgram
{
    public static void LinqProgramMain()
    {
        List<Department> departmentList = Data._departments;
        List<Employee> employeeList = Data._employees;

        #region Lesson 1
        Console.WriteLine("GetEmployeeList");
        GetEmployeeList(employeeList);

        Console.WriteLine("GetDepartmentList");
        GetDepartmentList(departmentList);

        Console.WriteLine("ManipulateData");
        ManipulateData(employeeList, departmentList);
        #endregion

        #region Lesson 2
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
        #endregion

        #region Lesson 3
        Console.WriteLine("OrderByMethodOperation");
        OrderByMethodOperation(employeeList, departmentList);

        Console.WriteLine("OrderByQueryOperation");
        OrderByQueryOperation(employeeList, departmentList);

        Console.WriteLine("OrderByDescendingMethodOperation");
        OrderByDescendingMethodOperation(employeeList, departmentList);

        Console.WriteLine("OrderByDescendingQueryOperation");
        OrderByDescendingQueryOperation(employeeList, departmentList);

        Console.WriteLine("ThenByMethodOperation");
        ThenByMethodOperation(employeeList, departmentList);

        Console.WriteLine("ThenByDescendingMethodOperation");
        ThenByDescendingMethodOperation(employeeList, departmentList);

        Console.WriteLine("GroupingOperators");
        GroupingOperators(employeeList, departmentList);

        Console.WriteLine("ToLookupOperators");
        ToLookupOperators(employeeList, departmentList);

        Console.WriteLine("All");
        All(employeeList);

        Console.WriteLine("Any");
        Any(employeeList);

        Console.WriteLine("ContainsOperator");
        ContainsOperator(employeeList);

        Console.WriteLine("OfTypeOperator");
        OfTypeOperator();

        Console.WriteLine("ElementAt");
        ElementAt(employeeList);

        Console.WriteLine("ElementAtOrDefault");
        ElementAtOrDefault(employeeList);

        Console.WriteLine("First");
        First();

        Console.WriteLine("FirstOrDefault");
        FirstOrDefault();

        Console.WriteLine("Last");
        Last();

        Console.WriteLine("LastOrDefault");
        LastOrDefault();

        Console.WriteLine("Single");
        Single(employeeList);

        Console.WriteLine("SingleOrDefault");
        SingleOrDefault(employeeList);
        #endregion


    }

    #region Lesson 1
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
    #endregion

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

    #region Lesson 3
    //// Sorting Operations OrderBy, OrderByDescending, ThenBy, ThenByDescending,Grouping and ToLookup Operators
    private static void OrderByMethodOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = employeeList.Join(departmentList, e => e.DepartmentId, d => d.Id,
            (emp, dept) => new
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                AnnualSalary = emp.AnnualSalary,
                DepartmentId = emp.DepartmentId,
                DepartmentName = dept.LongName
            }).OrderBy(o => o.DepartmentId);

        foreach (var item in results)
            Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");

    }

    private static void OrderByQueryOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = from emp in employeeList
                      join dept in departmentList
                      on emp.DepartmentId equals dept.Id
                      orderby emp.DepartmentId, emp.AnnualSalary
                      select new
                      {
                          Id = emp.Id,
                          FirstName = emp.FirstName,
                          LastName = emp.LastName,
                          AnnualSalary = emp.AnnualSalary,
                          DepartmentId = emp.DepartmentId,
                          DepartmentName = dept.LongName
                      };
        foreach (var item in results)
            Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");
    }

    private static void OrderByDescendingMethodOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = employeeList.Join(departmentList, e => e.DepartmentId, d => d.Id,
           (emp, dept) => new
           {
               Id = emp.Id,
               FirstName = emp.FirstName,
               LastName = emp.LastName,
               AnnualSalary = emp.AnnualSalary,
               DepartmentId = emp.DepartmentId,
               DepartmentName = dept.LongName
           }).OrderByDescending(o => o.DepartmentId);

        foreach (var item in results)
            Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");

    }

    private static void OrderByDescendingQueryOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = from emp in employeeList
                      join dept in departmentList
                      on emp.DepartmentId equals dept.Id
                      orderby emp.DepartmentId, emp.AnnualSalary descending
                      select new
                      {
                          Id = emp.Id,
                          FirstName = emp.FirstName,
                          LastName = emp.LastName,
                          AnnualSalary = emp.AnnualSalary,
                          DepartmentId = emp.DepartmentId,
                          DepartmentName = dept.LongName
                      };
        foreach (var item in results)
            Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");
    }

    private static void ThenByMethodOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = employeeList.Join(departmentList, e => e.DepartmentId, d => d.Id,
                    (emp, dept) => new
                    {
                        Id = emp.Id,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        AnnualSalary = emp.AnnualSalary,
                        DepartmentId = emp.DepartmentId,
                        DepartmentName = dept.LongName
                    }).OrderBy(o => o.DepartmentId).ThenBy(o => o.AnnualSalary);

        foreach (var item in results)
            Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");
    }

    private static void ThenByDescendingMethodOperation(List<Employee> employeeList, List<Department> departmentList)
    {
        var results = employeeList.Join(departmentList, e => e.DepartmentId, d => d.Id,
            (emp, dept) => new
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                AnnualSalary = emp.AnnualSalary,
                DepartmentId = emp.DepartmentId,
                DepartmentName = dept.LongName
            }).OrderByDescending(o => o.DepartmentId).ThenByDescending(o => o.AnnualSalary);

        foreach (var item in results)
            Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");
    }

    private static void GroupingOperators(List<Employee> employeeList, List<Department> departmentList)
    {
        var groupResult = from emp in employeeList
                              //orderby emp.DepartmentId descending
                          group emp by emp.DepartmentId;

        foreach (var empGroup in groupResult)
        {
            Console.WriteLine($"Department Id: {empGroup.Key}");

            foreach (Employee emp in empGroup)
            {
                Console.WriteLine($"\tEmployee Fullname: {emp.FirstName} {emp.LastName}");
            }
        }
    }

    private static void ToLookupOperators(List<Employee> employeeList, List<Department> departmentList)
    {
        var groupResult = employeeList.ToLookup(e => e.DepartmentId);

        foreach (var empGroup in groupResult)
        {
            Console.WriteLine($"Department Id: {empGroup.Key}");

            foreach (Employee emp in empGroup)
            {
                Console.WriteLine($"\tEmployee Full Name: {emp.FirstName} {emp.LastName}");
            }
        }
    }

    ////All, Any, Contains Quantifier Operators
    ////All and Any Operators
    private static void All(List<Employee> employeeList)
    {
        var annualSalaryCompare = 40000;
        bool isTrueAll = employeeList.All(e => e.AnnualSalary > annualSalaryCompare);
        if (isTrueAll)
            Console.WriteLine($"All employee annual salaries are above {annualSalaryCompare}");
        else
            Console.WriteLine($"Not all employee annual salaries are above {annualSalaryCompare}");
    }

    private static void Any(List<Employee> employeeList)
    {
        var annualSalaryCompare = 40000;
        bool isTrueAny = employeeList.Any(e => e.AnnualSalary > annualSalaryCompare);
        if (isTrueAny)
            Console.WriteLine($"At least one employee has an annual salary above {annualSalaryCompare}");
        else
            Console.WriteLine($"No employees have an annual salary above {annualSalaryCompare}");
    }

    ////Contains Operator
    private static void ContainsOperator(List<Employee> employeeList)
    {
        var searchEmployee = new Employee
        {
            Id = 3,
            FirstName = "Douglas",
            LastName = "Roberts",
            AnnualSalary = 40000.2m,
            IsManager = false,
            DepartmentId = 1
        };

        bool containsEmployee = employeeList.Contains(searchEmployee, new EmployeeComparer());

        if (containsEmployee)
            Console.WriteLine($"An employee record for {searchEmployee.FirstName} {searchEmployee.LastName} was found");
        else
            Console.WriteLine($"An employee record for {searchEmployee.FirstName} {searchEmployee.LastName} was not found");
    }

    ////OfType filter Operator
    private static void OfTypeOperator()
    {
        ArrayList mixedCollection = Data.GetHeterogeneousDataCollection();

        Console.WriteLine("mixedCollection stringResult");
        var stringResult = from s in mixedCollection.OfType<string>()
                           select s;

        foreach (var item in stringResult)
            Console.WriteLine(item);

        Console.WriteLine("mixedCollection intResult");
        var intResult = from i in mixedCollection.OfType<int>()
                        select i;

        foreach (var item in intResult)
            Console.WriteLine(item);

        Console.WriteLine("mixedCollection employeeResults");
        var employeeResults = from e in mixedCollection.OfType<Employee>()
                              select e;

        foreach (var emp in employeeResults)
            Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");

        Console.WriteLine("mixedCollection departmentResults");
        var departmentResults = from d in mixedCollection.OfType<Department>()
                                select d;

        foreach (var dept in departmentResults)
            Console.WriteLine($"{dept.Id,-5} {dept.LongName,-30} {dept.ShortName,-10}");
    }

    ////ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single and SingleOrDefault Element Operators
    ////ElementAt, ElementAtOrDefault Operators
    private static void ElementAt(List<Employee> employeeList)
    {
        var emp = employeeList.ElementAt(12);

        if (emp != null)
            Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");
        else
            Console.WriteLine("This employee record does not exist within the collection");
    }

    private static void ElementAtOrDefault(List<Employee> employeeList)
    {
        var emp = employeeList.ElementAtOrDefault(12);

        if (emp != null)
            Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");
        else
            Console.WriteLine("This employee record does not exist within the collection");
    }

    ////First, FirstOrDefault, Last, LastOrDefault Operators
    private static void First() {
        List<int> integerList = new List<int> {3,13,23,17,26,87};
        int result = integerList.First(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    private static void FirstOrDefault() {
        List<int> integerList = new List<int> { 3, 13, 23, 17, 26, 87 };
        int result = integerList.FirstOrDefault(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    private static void Last() {
        List<int> integerList = new List<int> { 3, 13, 23, 17, 26, 87 };
        int result = integerList.Last(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    private static void LastOrDefault() {
        List<int> integerList = new List<int> { 3, 13, 23, 17, 26, 87 };
        int result = integerList.LastOrDefault(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    ////Single, SingleOrDefault Operators
    private static void Single(List<Employee> employeeList)
    {
        var emp = employeeList.Single();
        if (emp != null)
            Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");
        else
            Console.WriteLine("This employee does not exist within the collection");
    }

    private static void SingleOrDefault(List<Employee> employeeList)
    {
        var emp = employeeList.SingleOrDefault();
        if (emp != null)
            Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");
        else
            Console.WriteLine("This employee does not exist within the collection");
    }
    #endregion
}
