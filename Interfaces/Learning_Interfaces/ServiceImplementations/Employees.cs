
using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class EmployeesProgram
{
    public static void EmployeesProgramMain()
    {
        IEmployee employee = new Employees();
        employee.Id = 1;
        employee.FirstName = "Bob";
        employee.LastName = "Tom";
        employee.JobTitle = "Hacker";
        employee.GetBasicInformation().Dump();
    }
}

public class Employees : IEmployee, IManagerialResponsibilities, IContractEmployee
{
    public int Id { get; set; }
    public string? JobTitle { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal? AnnualSalary { get; set; }
    public char? Gender { get; set; }
    public DateTime? JoinDate { get; set; }
    public string? HighestQualification { get; set; }
    public DateTime ContractEndDate { get; set; }
    public string GetBasicInformation()
    {
        return
            $"{Environment.NewLine}ID : {Id}" +
            $"{Environment.NewLine}Job Title : {JobTitle}" +
            $"{Environment.NewLine}First Name : {FirstName}" +
            $"{Environment.NewLine}Last Name : {LastName}";
    }
    // Specific methods for managers or higher roles
    public void AssignTasks()
    {
        Console.WriteLine("Assigning tasks to employees...");
    }
    public void ConductMeeting()
    {
        Console.WriteLine("Conducting a meeting...");
    }
    public void RenewContract()
    {
        if (ContractEndDate <= DateTime.Now)
        {
            // If the contract is expired, renew from the current date
            ContractEndDate = DateTime.Now.AddYears(1);
            Console.WriteLine($"Contract renewed. New contract end date: {ContractEndDate.ToShortDateString()}");
        }
        else
        {
            // If the contract is still valid, extend from the current contract end date
            ContractEndDate = ContractEndDate.AddYears(1);
            Console.WriteLine($"Contract extended. New contract end date: {ContractEndDate.ToShortDateString()}");
        }
    }
}
