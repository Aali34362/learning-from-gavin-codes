using BinaryFileHandling.Model;
using Bogus;
using System.Globalization;

namespace BinaryFileHandling;

public static class BinaryFileProgram
{
    public static void BinaryFileProgramMain()
    {
        try
        {
            int recSize = sizeof(int) + ((Employee.NameSize + 1) * 2) + sizeof(decimal) + (sizeof(char) - 1) + sizeof(bool);

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            string binaryFile = Path.Combine(rootPath, "Employees.dat");

            SeedData(binaryFile);

            do
            {
                ShowMainScreen(binaryFile, recSize);

                Console.WriteLine();
                Console.WriteLine("Please press the 'y' key if you'd like to update a particular record \nor press any other key to end the application");

                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the Id of the record you wish to update");

                    int inputId = Convert.ToInt32(Console.ReadLine());

                    UpdateEmployeeRecord(inputId, binaryFile, recSize);
                }
                else
                {
                    break;
                }

            } while (true);

            Console.Clear();
            Console.WriteLine("Thank you!");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

    #region Main Screen Display
    private static void ShowMainScreen(string binaryFile, int recSize)
    {
        Console.Clear();

        DisplayHeadings();

        DisplayAllRecordsOnScreen(binaryFile, GetTotalRecords(binaryFile, recSize));
    }

    private static void DisplayAllRecordsOnScreen(string binaryFile, int totalRecords)
    {
        using (BinaryReader reader = new BinaryReader(new FileStream(binaryFile, FileMode.Open)))
        {
            for (int count = 0; count < totalRecords; count++)
            {
                Console.WriteLine($"{reader.ReadInt32().ToString().PadRight(7)} {reader.ReadString().PadRight(Employee.NameSize)} {reader.ReadString().PadRight(Employee.NameSize)} {reader.ReadDecimal().ToString().PadRight(8)} {reader.ReadChar().ToString().PadRight(7)} {reader.ReadBoolean().ToString().PadRight(8)}");
            }
        }
    }
    #endregion

    #region Displaying Heading
    private static void DisplayHeadings()
    {
        Console.WriteLine(GetMainHeading());
        Console.WriteLine(GetUnderLine(GetMainHeading()));
        Console.WriteLine();

        Console.WriteLine(GetFieldHeadings());
        Console.WriteLine(GetUnderLine(GetFieldHeadings()));
        Console.WriteLine();
    }

    private static string GetUnderLine(string heading) => new string('-', heading.Length);

    private static string GetMainHeading() => "Employee Records binary Application";

    private static string GetFieldHeadings() =>
        $"{"Id".PadRight(7)} {"First Name".PadRight(Employee.NameSize)} {"Last Name".PadRight(Employee.NameSize)} {"Salary".PadRight(8)} {"Gender".PadRight(7)} {"Manager".PadRight(8)}";
    #endregion

    #region Records Count
    private static int GetNumberOfRecords(int fileLength, int recSize) => fileLength / recSize;

    private static int GetFileSize(string binaryFile) => Convert.ToInt32(new FileInfo(binaryFile).Length);

    private static int GetTotalRecords(string binaryFile, int recSize) => GetNumberOfRecords(GetFileSize(binaryFile), recSize);

    private static int GetRecordPosition(int recSize, int position) => recSize * position;

    private static int FindRecordById(string binaryFile, int inputId, int recSize, int totalRecords)
    {
        int recPosition = -1;
        int readId = -1;

        using (FileStream fileStream = new FileStream(binaryFile, FileMode.Open))
        {
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                for (int count = 0; count < totalRecords; count++)
                {
                    recPosition = GetRecordPosition(recSize, count);

                    fileStream.Seek(recPosition, SeekOrigin.Begin);

                    readId = reader.ReadInt32();

                    if (readId == inputId)
                        return recPosition;
                    else
                        recPosition = -1;
                }
            }
        }
        return recPosition;
    }
    #endregion

    #region Add Data
    ////private static void SeedData(string binaryFile)
    ////{
    ////    if (!File.Exists(binaryFile))
    ////    {
    ////        Employee employee1 = new Employee { Id = 1001, FirstName = "Andy", LastName = "Thompson", Salary = 50000, Gender = 'm', IsManager = true };
    ////        Employee employee2 = new Employee { Id = 1002, FirstName = "Sarah", LastName = "Smith", Salary = 60000, Gender = 'f', IsManager = true };
    ////        Employee employee3 = new Employee { Id = 1003, FirstName = "Bob", LastName = "Harris", Salary = 40000, Gender = 'm', IsManager = false };

    ////        //Bogus Data
    ////        var employeeFaker = GetFakeEmployeeDetails();
    ////        List<Employee> employeeList = employeeFaker.Generate(10);

    ////        using (BinaryWriter writer = new BinaryWriter(new FileStream(binaryFile, FileMode.Create)))
    ////        {
    ////            AddEmployeeRecord(writer, employee1);
    ////            AddEmployeeRecord(writer, employee2);
    ////            AddEmployeeRecord(writer, employee3);

    ////            foreach(Employee employee in employeeList)
    ////            {
    ////                AddEmployeeRecord(writer, employee);
    ////            }
    ////        }
    ////    }
    ////}
    private static void SeedData(string binaryFile)
    {
        List<Employee> existingRecords = ReadExistingRecordIds(binaryFile);

        List<Employee> employeeList = GetSampleEmployees();

        List<Employee> newEmployees = existingRecords.FindAll(e => !employeeList.Contains(e));

        if (newEmployees.Count > 0)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(binaryFile, FileMode.Append)))
            {
                foreach (Employee employee in newEmployees)
                {
                    AddEmployeeRecord(writer, employee);
                }
            }
            Console.WriteLine("New employees added to the file.");
        }
        else
        {
            Console.WriteLine("No new employees to add. All employee records already exist.");
        }
    }

    private static List<Employee> ReadExistingRecordIds(string binaryFile)
    {
        List<Employee> existingRecords = new List<Employee>();

        if (File.Exists(binaryFile))
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(binaryFile, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int id = reader.ReadInt32();
                    string firstName = reader.ReadString();
                    string lastName = reader.ReadString();
                    decimal salary = reader.ReadDecimal();
                    char gender = reader.ReadChar();
                    bool isManager = reader.ReadBoolean();

                    // Create and add the employee record
                    existingRecords.Add(new Employee
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        Salary = salary,
                        Gender = gender,
                        IsManager = isManager
                    });
                }
            }
        }
        return existingRecords;
    }

    private static List<Employee> GetSampleEmployees()
    {
        Employee employee1 = new Employee { Id = 1001, FirstName = "Andy", LastName = "Thompson", Salary = 50000, Gender = 'm', IsManager = true };
        Employee employee2 = new Employee { Id = 1002, FirstName = "Sarah", LastName = "Smith", Salary = 60000, Gender = 'f', IsManager = true };
        Employee employee3 = new Employee { Id = 1003, FirstName = "Bob", LastName = "Harris", Salary = 40000, Gender = 'm', IsManager = false };

        // Bogus Data (fake data generation)
        var employeeFaker = GetFakeEmployeeDetails();
        List<Employee> employeeList = employeeFaker.Generate(10);

        // Adding initial employees
        employeeList.InsertRange(0, new List<Employee> { employee1, employee2, employee3 });

        return employeeList;
    }

    private static Faker<Employee> GetFakeEmployeeDetails()
    {
        return new Faker<Employee>()
            .RuleFor(e => e.Id, f => f.Random.Int(1000, 9999))                     // Random ID between 1000 and 9999
        .RuleFor(e => e.FirstName, f => f.Name.FirstName())                    // Random first name
        .RuleFor(e => e.LastName, f => f.Name.LastName())                      // Random last name
        .RuleFor(e => e.Salary, f => f.Finance.Amount(30000, 100000))          // Random salary between 30,000 and 100,000
        .RuleFor(e => e.Gender, f => f.PickRandom(new[] { 'm', 'f' }))         // Random gender 'm' or 'f'
        .RuleFor(e => e.IsManager, f => f.Random.Bool())
        ;
    }

    private static void AddEmployeeRecord(BinaryWriter writer, Employee employee)
    {
        writer.Write(employee.Id);
        writer.Write(employee.FirstName);
        writer.Write(employee.LastName);
        writer.Write(employee.Salary);
        writer.Write(employee.Gender);
        writer.Write(employee.IsManager);
    }
    #endregion

    #region Update Data
    private static void UpdateEmployeeRecord(int inputId, string binaryFile, int recSize)
    {
        int totalRecords = GetTotalRecords(binaryFile, recSize);

        int pos = FindRecordById(binaryFile, inputId, recSize, totalRecords);

        if (pos != -1)
        {
            using (FileStream fileStream = new FileStream(binaryFile, FileMode.Open))
            {
                fileStream.Seek(pos * sizeof(int), SeekOrigin.Begin);

                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    UpdateName(fileStream, writer, "first name");
                    UpdateName(fileStream, writer, "last name");
                    UpdateSalary(fileStream, writer, "salary");
                    UpdateGender(fileStream, writer, "gender");
                    UpdateIsManager(fileStream, writer);
                }
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Unable to find record. Please press any key to navigate to the main screen");
            Console.ReadKey();
        }
    }

    private static void UpdateName(FileStream fileStream, BinaryWriter writer, string fieldLabel)
    {
        Console.WriteLine($"Please enter a value for the employee's  {fieldLabel}");

        string name = Console.ReadLine()!;

        if (String.IsNullOrEmpty(name))
            fileStream.Seek(Employee.NameSize + 1, SeekOrigin.Begin);
        else
            writer.Write(name.PadRight(Employee.NameSize));
    }

    private static void UpdateSalary(FileStream fileStream, BinaryWriter writer, string fieldLabel)
    {
        Console.WriteLine($"Please enter a value for the employee's {fieldLabel}");

        string salaryInput = Console.ReadLine()!;

        if (String.IsNullOrEmpty(salaryInput))
            fileStream.Seek(sizeof(decimal), SeekOrigin.Begin);
        else
            writer.Write(Decimal.Parse(salaryInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
    }

    private static void UpdateGender(FileStream fileStream, BinaryWriter writer, string fieldLabel)
    {
        Console.WriteLine($"Please enter a value for the employee's {fieldLabel} ('m'/'f')");

        string genderInput = Console.ReadLine()!;

        if (String.IsNullOrEmpty(genderInput))
            fileStream.Seek(sizeof(decimal), SeekOrigin.Begin);
        else
            writer.Write(Convert.ToChar(genderInput));
    }

    private static void UpdateIsManager(FileStream fileStream, BinaryWriter writer)
    {
        Console.WriteLine("The employee is a manager (true/false)");

        string isManagerInput = Console.ReadLine()!;

        if (!String.IsNullOrEmpty(isManagerInput))
            writer.Write(Convert.ToBoolean(isManagerInput));
    }

    #endregion
}
