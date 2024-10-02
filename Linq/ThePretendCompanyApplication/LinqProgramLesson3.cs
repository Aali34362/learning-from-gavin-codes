using System.Collections;
using TCPData;
using TCPExtensions;

namespace ThePretendCompanyApplication;

public static class LinqProgramLesson3
{
    public static void LinqProgramLesson3Main(List<Department> departmentList, List<Employee> employeeList)
    {
        #region Lesson 3
        Console.WriteLine("\n OrderByMethodOperation :");
        OrderByMethodOperation(employeeList, departmentList);

        Console.WriteLine("\n OrderByQueryOperation :");
        OrderByQueryOperation(employeeList, departmentList);

        Console.WriteLine("\n OrderByDescendingMethodOperation :");
        OrderByDescendingMethodOperation(employeeList, departmentList);

        Console.WriteLine("\n OrderByDescendingQueryOperation :");
        OrderByDescendingQueryOperation(employeeList, departmentList);

        Console.WriteLine("\n ThenByMethodOperation :");
        ThenByMethodOperation(employeeList, departmentList);

        Console.WriteLine("\n ThenByDescendingMethodOperation :");
        ThenByDescendingMethodOperation(employeeList, departmentList);

        Console.WriteLine("\n GroupingOperators :");
        GroupingOperators(employeeList, departmentList);

        Console.WriteLine("\n ToLookupOperators :");
        ToLookupOperators(employeeList, departmentList);

        Console.WriteLine("\n All :");
        All(employeeList);

        Console.WriteLine("\n Any :");
        Any(employeeList);

        Console.WriteLine("\n ContainsOperator :");
        ContainsOperator(employeeList);

        Console.WriteLine("\n OfTypeOperator :");
        OfTypeOperator();

        Console.WriteLine("\n ElementAt :");
        ElementAt(employeeList);

        Console.WriteLine("\n ElementAtOrDefault :");
        ElementAtOrDefault(employeeList);

        Console.WriteLine("\n First :");
        First();

        Console.WriteLine("\n FirstOrDefault :");
        FirstOrDefault();

        Console.WriteLine("\n Last :");
        Last();

        Console.WriteLine("\n LastOrDefault :");
        LastOrDefault();

        Console.WriteLine("\n Single :");
        //Single(employeeList);

        Console.WriteLine("\n SingleOrDefault :");
        SingleOrDefault(employeeList);
        #endregion
    }

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
    private static void First()
    {
        List<int> integerList = new List<int> { 3, 13, 23, 17, 26, 87 };
        int result = integerList.First(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    private static void FirstOrDefault()
    {
        List<int> integerList = new List<int> { 3, 13, 23, 17, 26, 87 };
        int result = integerList.FirstOrDefault(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    private static void Last()
    {
        List<int> integerList = new List<int> { 3, 13, 23, 17, 26, 87 };
        int result = integerList.Last(i => i % 2 == 0);

        if (result != 0)
            Console.WriteLine(result);
        else
            Console.WriteLine("There are no even numbers in the collection");
    }

    private static void LastOrDefault()
    {
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
