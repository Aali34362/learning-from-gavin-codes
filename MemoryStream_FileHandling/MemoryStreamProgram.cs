using MemoryStream_FileHandling.Utilities;
using System.IO;
using System.Text;

namespace MemoryStream_FileHandling;

public static class MemoryStreamProgram
{
    public static void MemoryStreamProgramMain()
    {
        UnicodeEncoding unicodeEncoding = new UnicodeEncoding();

        MemoryStream memoryStream = new MemoryStream(ConstantClass.RecordLength);

        SeedData(unicodeEncoding, memoryStream);

        EmployeeRecordBefore(unicodeEncoding, memoryStream);

        Console.WriteLine();
        Console.WriteLine("Press any key to update the above employee's record...");
        Console.ReadKey();
        Console.WriteLine();

        memoryStream.Seek(0, SeekOrigin.Begin);

        UpdateSalary(unicodeEncoding, memoryStream, 80000);
        UpdateIsManager(unicodeEncoding, memoryStream, true);

        memoryStream.Seek(0, SeekOrigin.Begin);

        EmployeeRecordAfter(unicodeEncoding, memoryStream);
    }    

    #region Get Fields
    private static string GetField(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream, int offset, int length)
    {
        memoryStream.Seek(offset, SeekOrigin.Begin);

        byte[] byteArray = new byte[length];

        int count = memoryStream.Read(byteArray, 0, length);

        string fieldValue = new string(ReturnCharArrayFromByteArray(unicodeEncoding, byteArray, count));

        return fieldValue.Trim();

    }

    private static string GetField(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream, int length)
    {
        memoryStream.Seek(0, SeekOrigin.Current);

        byte[] byteArray = new byte[length];

        int count = memoryStream.Read(byteArray, 0, length);

        string fieldValue = new string(ReturnCharArrayFromByteArray(unicodeEncoding, byteArray, count));

        return fieldValue.Trim();

    }

    private static char[] ReturnCharArrayFromByteArray(UnicodeEncoding unicodeEncoding, byte[] byteArray, int count)
    {
        char[] charArray = new char[unicodeEncoding.GetCharCount(byteArray, 0, count)];

        unicodeEncoding.GetDecoder().GetChars(byteArray, 0, count, charArray, 0);

        return charArray;

    }
    #endregion 

    #region Add
    public static void SeedData(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream)
    {
        int id = 1003;
        string firstName = "John";
        string lastName = "Jenkins";
        decimal salary = 60000;
        char gender = 'm';
        bool isManager = false;

        string employeeRecord = id.ToString().PadRight(ConstantClass.IdLength / 2) +
            firstName.PadRight(ConstantClass.FirstNameLength / 2) +
            lastName.PadRight(ConstantClass.LastNameLength / 2) +
            salary.ToString().PadRight(ConstantClass.SalaryLength / 2) +
            gender.ToString().PadRight(ConstantClass.GenderLength / 2) +
            isManager.ToString().PadRight(ConstantClass.IsManagerLength / 2);

        byte[] employeeData = unicodeEncoding.GetBytes(employeeRecord);

        memoryStream.Write(employeeData, 0, employeeRecord.Length * 2);
    }
    #endregion

    #region Update
    private static void Updatefield(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream, int offset, int length, string newValue)
    {
        byte[] data = unicodeEncoding.GetBytes(newValue);

        memoryStream.Seek(offset, SeekOrigin.Begin);
        memoryStream.Write(data, 0, length);
    }

    private static void UpdateIsManager(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream, bool isManager)
    {

        string newValue = isManager.ToString().PadRight(ConstantClass.IsManagerLength / 2);

        Updatefield(unicodeEncoding, memoryStream, ConstantClass.IsManagerOffset, ConstantClass.IsManagerLength, newValue);

    }

    private static void UpdateSalary(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream, decimal salary)
    {
        string newValue = salary.ToString().PadRight(ConstantClass.SalaryLength / 2);

        Updatefield(unicodeEncoding, memoryStream, ConstantClass.SalaryOffset, ConstantClass.SalaryLength, newValue);

    }
    #endregion

    #region Show Data
    private static void EmployeeRecordBefore(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream)
    {
        Console.WriteLine("Employee Record before Promotion");
        Console.WriteLine("--------------------------------");

        Console.WriteLine($"Id: {GetField(unicodeEncoding, memoryStream, ConstantClass.IdOffset, ConstantClass.IdLength)}");
        Console.WriteLine($"FirstName: {GetField(unicodeEncoding, memoryStream, ConstantClass.FirstNameOffset, ConstantClass.FirstNameLength)}");
        Console.WriteLine($"LastName: {GetField(unicodeEncoding, memoryStream, ConstantClass.LastNameOffset, ConstantClass.LastNameLength)}");
        Console.WriteLine($"Salary: {GetField(unicodeEncoding, memoryStream, ConstantClass.SalaryOffset, ConstantClass.SalaryLength)}");
        Console.WriteLine($"Gender: {GetField(unicodeEncoding, memoryStream, ConstantClass.GenderOffset, ConstantClass.GenderLength)}");
        Console.WriteLine($"Manager: {GetField(unicodeEncoding, memoryStream, ConstantClass.IsManagerOffset, ConstantClass.IsManagerLength)}");
    }

    private static void EmployeeRecordAfter(UnicodeEncoding unicodeEncoding, MemoryStream memoryStream)
    {
        Console.WriteLine("Employee Record after Promotion");
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Id: {GetField(unicodeEncoding, memoryStream, ConstantClass.IdLength)}");
        Console.WriteLine($"FirstName: {GetField(unicodeEncoding, memoryStream, ConstantClass.FirstNameLength)}");
        Console.WriteLine($"LastName: {GetField(unicodeEncoding, memoryStream, ConstantClass.LastNameLength)}");
        Console.WriteLine($"Salary: {GetField(unicodeEncoding, memoryStream, ConstantClass.SalaryLength)}");
        Console.WriteLine($"Gender: {GetField(unicodeEncoding, memoryStream, ConstantClass.GenderLength)}");
        Console.WriteLine($"Manager: {GetField(unicodeEncoding, memoryStream, ConstantClass.IsManagerLength)}");
    }
    #endregion
}
