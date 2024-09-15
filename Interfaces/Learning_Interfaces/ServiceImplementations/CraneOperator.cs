
using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public class CraneOperatorService
{
    private readonly IEmployee _employee;
    private readonly ICraneOperatorResponsibilities _responsibilities;
    private readonly IContractEmployee _contractEmployee;

    public CraneOperatorService(
        IEmployee employee,
        ICraneOperatorResponsibilities responsibilities,
        IContractEmployee contractEmployee)
    {
        _employee = employee;
        _responsibilities = responsibilities;
        _contractEmployee = contractEmployee;
    }

    public void ProcessCraneOperator()
    {
        // Since we used Singleton, the object is shared
        _employee.FirstName = "Jesse";  // You can still set properties
        _employee.LastName = "Thompson";
        _employee.JobTitle = "Crane Operator";
        _employee.JoinDate = new DateTime(2010, 01, 01);

        Console.WriteLine(_employee.GetBasicInformation());

        // Crane-specific tasks
        _responsibilities.InspectCrane();
        _responsibilities.OperateCrane();

        // Contract-related
        Console.WriteLine($"Current contract end date: {_contractEmployee.ContractEndDate.ToShortDateString()}");
        _contractEmployee.RenewContract();  // Renew the contract
    }
}

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
        //employee.ContractEndDate = new DateTime(2023, 09, 01); // Contract has expired
        employee.GetBasicInformation().Dump();
        
        // You can cast the IEmployee to CraneOperator to access the method
        var craneOperator = (CraneOperator)employee;
        craneOperator.GetFullYearWorked().Dump();  // Works because it's casted to CraneOperator

        ICraneOperatorResponsibilities responsibilities = new CraneOperator();
        responsibilities.InspectCrane();
        responsibilities.OperateCrane();


        // Output current contract details
        Console.WriteLine($"Current contract end date: {craneOperator.ContractEndDate.ToShortDateString()}");
        IContractEmployee contract = new CraneOperator();
        // Renew the contract
        contract.RenewContract(); // Renew logic is applied based on the current date

        // Renew the contract again (just to show the behavior when the contract is still valid)
        contract.RenewContract(); // This extends the contract further
    }
}

public static class CraneOperatorGPTProgram
{
    public static void CraneOperatorGPTProgramMain()
    {
        // Use dependency injection to get instances, or directly instantiate for simplicity
        CraneOperator craneOperator = new();
        craneOperator.Id = 1;
        craneOperator.FirstName = "Jesse";
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
        Console.WriteLine("Operating crane...");
    }
    public void InspectCrane()
    {
        Console.WriteLine("Inspecting crane...");
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
