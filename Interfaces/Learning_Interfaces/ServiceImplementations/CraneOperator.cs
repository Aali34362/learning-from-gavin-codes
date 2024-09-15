
using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class CraneOperatorProgram
{
    public static void CraneOperatorProgramMain()
    {
        IEmployee employee = new CraneOperator();
        employee.Id = 1;
        employee.FirstName = "Jesse";
        employee.LastName = "Thompson";
        employee.JobTitle = "Crane Operator";
        employee.JoinDate = new DateTime(2010, 01, 01);
        employee.GetBasicInformation().Dump();
        
        // You can cast the IEmployee to CraneOperator to access the method
        var craneOperator = (CraneOperator)employee;
        //craneOperator.ContractEndDate = new DateTime(2023, 09, 01);
        $"Years worked: {craneOperator.GetFullYearWorked()}".Dump(); // Works because it's casted to CraneOperator

        ICraneOperatorResponsibilities responsibilities = craneOperator;
        responsibilities.InspectCrane();
        responsibilities.OperateCrane();

        // Output current contract details
        IContractEmployee contract = craneOperator;
        // Renew the contract
        contract.ContractEndDate = new DateTime(2024, 01, 01);
        contract.RenewContract(); // Renew logic is applied based on the current date

    }
}

public static class CraneOperatorGPTProgram
{
    public static void CraneOperatorGPTProgramMain()
    {
        // Use dependency injection to get instances, or directly instantiate for simplicity
        CraneOperator craneOperator = new();
        craneOperator.Id = 1;
        craneOperator.FirstName = "Jane";
        craneOperator.LastName = "Thompson";
        craneOperator.JobTitle = "Crane Operator";
        craneOperator.JoinDate = new DateTime(2010, 01, 01);
        craneOperator.ContractEndDate = new DateTime(2023, 09, 01);
        // Output basic information
        Console.WriteLine(craneOperator.GetBasicInformation());

        // Full years worked
        Console.WriteLine($"Years worked: {craneOperator.GetFullYearWorked()}");

        // Crane-specific tasks
        craneOperator.InspectCrane();
        craneOperator.OperateCrane();

        // Output current contract details
        Console.WriteLine($"Current contract end date: {craneOperator.ContractEndDate.ToShortDateString()}");

        // Renew the contract
        craneOperator.RenewContract();
    }
}

public class CraneOperator : IEmployee, ICraneOperatorResponsibilities, IContractEmployee
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
            $"{Environment.NewLine}Last Name : {LastName}"+
            $"{Environment.NewLine}Contract End Date : {ContractEndDate}";
    }
    public int GetFullYearWorked()
    {
        DateTime zeroTime = new DateTime(1, 1, 1);
        TimeSpan span = DateTime.Now.Subtract((DateTime)JoinDate!);
        return zeroTime.Add(span).Year - 1;
    }
    // Specific methods for CraneOperator
    public void OperateCrane()
    {
        Console.WriteLine($"{FirstName} is Operating crane...");
    }
    public void InspectCrane()
    {
        Console.WriteLine($"{FirstName} is Inspecting crane...");
    }
    public void RenewContract()
    {
        if (ContractEndDate <= DateTime.Now)
        {
            // If the contract is expired, renew from the current date
            ContractEndDate = DateTime.Now.AddYears(1);
            Console.WriteLine($"Contract renewed of {FirstName} {LastName}. New contract end date: {ContractEndDate.ToShortDateString()}");
        }
        else
        {
            // If the contract is still valid, extend from the current contract end date
            ContractEndDate = ContractEndDate.AddYears(1);
            Console.WriteLine($"Contract extended of {FirstName} {LastName}. New contract end date: {ContractEndDate.ToShortDateString()}");
        }
    }
}
