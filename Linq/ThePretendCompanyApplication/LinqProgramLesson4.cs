using TCPData;
using TCPExtensions;

namespace ThePretendCompanyApplication;

public static class LinqProgramLesson4
{
    public static void LinqProgramLesson4Main(List<Department> departmentList, List<Employee> employeeList)
    {
        #region Lesson 4
        List<Employee> employeeList2 = EmployeeList2();

        Console.WriteLine("\n SequenceEqual :");
        SequenceEqual(employeeList);

        Console.WriteLine("\n Concatenation :");
        Concatenation(employeeList);

        Console.WriteLine("\n AggregateOperators :");
        AggregateOperators(employeeList);

        Console.WriteLine("\n Average :");
        Average(employeeList);

        Console.WriteLine("\n Count :");
        Count(employeeList);

        Console.WriteLine("\n Sum :");
        Sum(employeeList);

        Console.WriteLine("\n Max :");
        Max(employeeList);

        Console.WriteLine("\n DefaultIfEmpty :");
        DefaultIfEmpty();

        Console.WriteLine("\n DefaultIfEmptyEmployee :");
        DefaultIfEmptyEmployee();

        Console.WriteLine("\n Empty :");
        Empty();

        Console.WriteLine("\n Range :");
        Range();

        Console.WriteLine("\n Repeat :");
        Repeat();

        Console.WriteLine("\n Distinct :");
        Distinct();

        Console.WriteLine("\n Except :");
        Except(employeeList, employeeList2);

        Console.WriteLine("\n Intersect :");
        Intersect(employeeList, employeeList2);

        Console.WriteLine("\n Union :");
        Union(employeeList, employeeList2);

        Console.WriteLine("\n Skip :");
        Skip(employeeList);

        Console.WriteLine("\n SkipWhile :");
        SkipWhile(employeeList);

        Console.WriteLine("\n Take :");
        Take(employeeList);

        Console.WriteLine("\n TakeWhile :");
        TakeWhile(employeeList);

        Console.WriteLine("\n ToList :");
        ToList(employeeList);

        Console.WriteLine("\n ToDictionary :");
        ToDictionary(employeeList);

        Console.WriteLine("\n ToArray :");
        ToArray(employeeList);

        Console.WriteLine("\n Let :");
        Let(employeeList);

        Console.WriteLine("\n Into :");
        Into(employeeList);

        Console.WriteLine("\n Select :");
        Select(departmentList);

        Console.WriteLine("\n Select :");
        SelectMany(departmentList);
        #endregion
    }

    #region Lesson 4

    private static List<Employee> EmployeeList2()
    {
        List<Employee> employeeList2 = new List<Employee>();

        employeeList2.Add(new Employee
        {
            Id = 1,
            FirstName = "Bob",
            LastName = "Jones",
            AnnualSalary = 60000.3m,
            IsManager = true,
            DepartmentId = 2
        }
        );
        employeeList2.Add(new Employee
        {
            Id = 3,
            FirstName = "Douglas",
            LastName = "Roberts",
            AnnualSalary = 40000.2m,
            IsManager = false,
            DepartmentId = 1
        }
        );
        employeeList2.Add(new Employee
        {
            Id = 5,
            FirstName = "Craig",
            LastName = "Laurence",
            AnnualSalary = 20000.2m,
            IsManager = false,
            DepartmentId = 1
        }
        );
        employeeList2.Add(new Employee
        {
            Id = 6,
            FirstName = "Elizabeth",
            LastName = "Tate",
            AnnualSalary = 85000,
            IsManager = true,
            DepartmentId = 1
        }
        );

        return employeeList2;
    }

    ////Equality Operator
    ////SequenceEqual
    private static void SequenceEqual(List<Employee> employeeList)
    {
        var integerList1 = new List<int> { 1, 2, 3, 4, 5, 6 };
        var integerList2 = new List<int> { 1, 2, 3, 4, 5, 6 };

        var boolSequenceEqual = integerList1.SequenceEqual(integerList2);

        Console.WriteLine(boolSequenceEqual);
        var employeeListCompare = Data._employees;
        bool boolSE = employeeList.SequenceEqual(employeeListCompare, new EmployeeComparer());

        Console.WriteLine($"Result: {boolSE}");
    }

    ////Concatenation Operator
    ////Concat
    private static void Concatenation(List<Employee> employeeList)
    {
        List<int> integerList1 = new List<int> { 1, 2, 3, 4 };
        List<int> integerList2 = new List<int> { 5, 6, 7, 8, 9, 10 };

        IEnumerable<int> integerListConcat = integerList1.Concat(integerList2);

        foreach (var item in integerListConcat)
            Console.WriteLine(item);

        List<Employee> employeeList2 = new List<Employee> { new Employee { Id = 5, FirstName = "Tony", LastName = "Stark" }, new Employee { Id = 6, FirstName = "Debbie", LastName = "Townsend" } };

        IEnumerable<Employee> results = employeeList.Concat(employeeList2);

        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");
    }
    ////Aggregate Operators - Aggregate, Average, Count, Sum, Max
    ////Aggregate Operator
    private static void AggregateOperators(List<Employee> employeeList)
    {
        decimal totalAnnualSalary = employeeList.Aggregate<Employee, decimal>(0, (totAnnualSalary, e) =>
        {
            var bonus = (e.IsManager) ? 0.04m : 0.02m;

            totAnnualSalary = (e.AnnualSalary + (e.AnnualSalary * bonus)) + totAnnualSalary;

            return totAnnualSalary;
        });

        Console.WriteLine($"Total Annual Salary of all employees (including bonus): {totalAnnualSalary}");

        string data = employeeList.Aggregate<Employee, string, string>("Employee Annual Salaries (including bonus): ",
            (s, e) =>
            {
                var bonus = (e.IsManager) ? 0.04m : 0.02m;

                s += $"{e.FirstName} {e.LastName} - {e.AnnualSalary + (e.AnnualSalary * bonus)}, ";
                return s;
            }, s => s.Substring(0, s.Length - 2)
        );

        Console.WriteLine(data);
    }

    ////Average
    private static void Average(List<Employee> employeeList) =>
        Console.WriteLine($"Average Annual Salary (Technology Department): {employeeList.Where(e => e.DepartmentId == 3).Average(e => e.AnnualSalary)}");

    ////Count
    private static void Count(List<Employee> employeeList) =>
        Console.WriteLine($"Number of Employees (Technology Department): {employeeList.Count(e => e.DepartmentId == 3)}");

    ////Sum
    private static void Sum(List<Employee> employeeList) =>
        Console.WriteLine($"Total Annual Salaries: {employeeList.Sum(e => e.AnnualSalary)}");

    ////Max
    private static void Max(List<Employee> employeeList) =>
        Console.WriteLine($"Highest Annual Salary: {employeeList.Max(e => e.AnnualSalary)}");

    ////Generation Operators - DefaultIfEmpty, Empty, Range, Repeat
    ////DefaultIfEmpty
    private static void DefaultIfEmpty()
    {
        List<int> intList = new List<int>();
        var newList = intList.DefaultIfEmpty();
        Console.WriteLine(newList.ElementAt(0));
    }
    private static void DefaultIfEmptyEmployee()
    {
        List<Employee> employees = new List<Employee>();
        var newList = employees.DefaultIfEmpty(new Employee { Id = 0 });

        var result = newList.ElementAt(0);

        if (result.Id == 0)
            Console.WriteLine($"The list is empty");
    }

    ////Empty
    private static void Empty()
    {
        List<Employee> emptyEmployeeList = Enumerable.Empty<Employee>().ToList();

        emptyEmployeeList.Add(new Employee { Id = 7, FirstName = "Dan", LastName = "Brown" });

        foreach (var item in emptyEmployeeList)
            Console.WriteLine($"{item.FirstName} {item.LastName}");
    }

    ////Range
    private static void Range()
    {
        var intCollection = Enumerable.Range(25, 20);
        foreach (var item in intCollection)
            Console.WriteLine(item);
    }

    ////Repeat
    private static void Repeat()
    {
        var strCollection = Enumerable.Repeat<string>("Placeholder", 10);
        foreach (var item in strCollection)
            Console.WriteLine(item);
    }

    ////Set Operators - Distinct, Except, Intersect, Union
    ////Distinct
    private static void Distinct()
    {
        List<int> list = new List<int> { 1, 2, 1, 4, 6, 2, 6, 7, 8, 7, 7, 7 };
        var results = list.Distinct();
        foreach (var item in results)
            Console.WriteLine(item);
    }

    ////Except
    private static void Except(List<Employee> employeeList, List<Employee> employeeList2)
    {
        IEnumerable<int> collection1 = new List<int> { 1, 2, 3, 4 };
        IEnumerable<int> collection2 = new List<int> { 3, 4, 5, 6 };

        var results = collection1.Except(collection2);
        foreach (var item in results)
            Console.WriteLine(item);

        var result1 = employeeList.Except(employeeList2, new EmployeeComparer());

        foreach (var item in result1)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");
    }

    ////Intersect
    private static void Intersect(List<Employee> employeeList, List<Employee> employeeList2)
    {
        var results = employeeList.Intersect(employeeList2, new EmployeeComparer());
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");
    }

    ////Union
    private static void Union(List<Employee> employeeList, List<Employee> employeeList2)
    {
        var results = employeeList.Union(employeeList2, new EmployeeComparer());
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");
    }

    ////Partitioning Operators - Skip, SkipWhile, Take, TakeWhile
    ////Skip
    private static void Skip(List<Employee> employeeList)
    {
        var results = employeeList.Skip(2);
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");
    }
    ////SkipWhile
    private static void SkipWhile(List<Employee> employeeList)
    {
        employeeList.Add(new Employee { Id = 5, FirstName = "Sam", LastName = "Davis", AnnualSalary = 100000.0m });
        var results = employeeList.SkipWhile(e => e.AnnualSalary > 50000);
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}");
    }
    
    ////Take
    private static void Take(List<Employee> employeeList)
    {
        var results = employeeList.Take(2);
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");
    }
    
    ////TakeWhile
    private static void TakeWhile(List<Employee> employeeList)
    {
        employeeList.Add(new Employee { Id = 5, FirstName = "Sam", LastName = "Davis", AnnualSalary = 100000 });
        var results = employeeList.TakeWhile(e => e.AnnualSalary > 50000);
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}");
    }

    ////Conversion Operators - ToList, ToDictionary, ToArray
    ////ToList
    private static void ToList(List<Employee> employeeList)
    {
        List<Employee> results = (from emp in employeeList
                                  where emp.AnnualSalary > 50000
                                  select emp).ToList();
        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}");
    }
    ////ToDictionary
    private static void ToDictionary(List<Employee> employeeList)
    {
        Dictionary<int, Employee> dictionary = (from emp in employeeList
                                                where emp.AnnualSalary > 50000
                                                select emp).ToDictionary<Employee, int>(e => e.Id);
        foreach (var key in dictionary.Keys)
            Console.WriteLine($"Key: {key}, Value: {dictionary[key].FirstName} {dictionary[key].LastName}");
    }
    ////ToArray
    private static void ToArray(List<Employee> employeeList)
    {
        Employee[] results = (from emp in employeeList
                              where emp.AnnualSalary > 50000
                              select emp).ToArray();

        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}");
    }

    ////Let Clause and Into Clause
    ////Let
    private static void Let(List<Employee> employeeList)
    {
        var results = from emp in employeeList
                      let Initials = emp.FirstName?.Substring(0, 1).ToUpper() + emp.LastName?.Substring(0, 1).ToUpper()
                      let AnnualSalaryPlusBonus = (emp.IsManager) ? emp.AnnualSalary + (emp.AnnualSalary * 0.04m) : emp.AnnualSalary + (emp.AnnualSalary * 0.02m)
                      where Initials == "JS" || Initials == "SJ" && AnnualSalaryPlusBonus > 50000
                      select new
                      {
                          Initials = Initials,
                          FullName = emp.FirstName + " " + emp.LastName,
                          AnnualSalaryPlusBonus = AnnualSalaryPlusBonus
                      };

        foreach (var item in results)
            Console.WriteLine($"{item.Initials,-5} {item.FullName,-20} {item.AnnualSalaryPlusBonus,10}");
    }
    ////Into
    private static void Into(List<Employee> employeeList)
    {
        var results = from emp in employeeList
                      where emp.AnnualSalary > 50000
                      select emp
                      into HighEarners
                      where HighEarners.IsManager == true
                      select HighEarners;

        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}\t{item.IsManager}");
    }
    
    ////Projection Operators - Select, SelectMany
    ////Select
    private static void Select(List<Department> departmentList)
    {
        var results = departmentList.Select(d => d.Employees);

        foreach (var items in results)
            foreach (var item in items!)
                Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}\t{item.IsManager}");
    }
    ////SelectMany
    private static void SelectMany(List<Department> departmentList)
    {
        var results = departmentList.SelectMany(d => d.Employees!); //.OrderBy(o => o.Id);

        foreach (var item in results)
            Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10}\t{item.IsManager}");
    }
        #endregion
    }
