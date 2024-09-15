using Learning_Interfaces.ServiceImplementations;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.Services;

public class RoleServices(
    IEmployee employee,
    ICraneOperatorResponsibilities responsibilities,
    IContractEmployee contractEmployee)
{
    private readonly IEmployee _employee = employee;
    private readonly ICraneOperatorResponsibilities _responsibilities = responsibilities;
    private readonly IContractEmployee _contractEmployee = contractEmployee;

    public void ProcessCraneOperator()
    {
        var craneOperator = _employee as CraneOperator;

        if (craneOperator != null)
        {
            craneOperator.FirstName = "James";
            craneOperator.LastName = "Thompson";
            craneOperator.JobTitle = "Crane Operator";
            craneOperator.JoinDate = new DateTime(2010, 01, 01);
            craneOperator.ContractEndDate = new DateTime(2025, 01, 01);

            _responsibilities.InspectCrane();
            _responsibilities.OperateCrane();

            _contractEmployee.ContractEndDate = new DateTime(2025, 01, 01);
            _contractEmployee.RenewContract();

            Console.WriteLine(craneOperator.GetBasicInformation());
            Console.WriteLine($"Years worked: {craneOperator.GetFullYearWorked()}");
        }
        else
        {
            Console.WriteLine("The employee is not a CraneOperator.");
        }
    }
}
