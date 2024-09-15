
using Dumpify;

namespace Learning_Interfaces;

public static class EmployeesProgram
{
    public static void EmployeesProgramMain()
    {
        Employees employee = new();
        employee.Id = 1;
        employee.FirstName = "Bob";
        employee.LastName = "Tom";
        employee.JobTitle = "Hacker";

        employee.GetBasicInformation().Dump();
    }
}

public class Employees : IEmployee
{
    public int Id { get ; set ; }
    public string? JobTitle { get ; set ; }
    public string? FirstName { get ; set ; }
    public string? LastName { get ; set ; }
    public decimal? AnnualSalary { get ; set ; }
    public char? Gender { get ; set ; }
    public DateTime? JoinDate { get ; set ; }
    public string? HighestQualification { get ; set ; }

    public string GetBasicInformation()
    {
        return
            $"{Environment.NewLine}ID : {Id}" +
            $"{Environment.NewLine}Job Title : {JobTitle}" +
            $"{Environment.NewLine}First Name : {FirstName}" +
            $"{Environment.NewLine}Last Name : {LastName}";
    }
}
