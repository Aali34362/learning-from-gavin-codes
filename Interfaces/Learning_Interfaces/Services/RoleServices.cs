using Dumpify;
using Learning_Interfaces.ServiceImplementations;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.Services;

public class RoleServices
{
    private readonly IEmployee _humanResource;
    private readonly IEmployee _craneOperator;

    private readonly ICraneOperatorResponsibilities _craneResponsibilities;
    private readonly IManagerialResponsibilities _managerResponsibilities;

    private readonly IContractEmployee _humanResourceContractEmployee;
    private readonly IContractEmployee _craneOperatorContractEmployee;

    public RoleServices(IEnumerable<IEmployee> employee,
    ICraneOperatorResponsibilities craneResponsibilities,
    IManagerialResponsibilities managerResponsibilities,
    IEnumerable<IContractEmployee> contractEmployee)
    {
        _humanResource = employee.OfType<HumanResource>().FirstOrDefault()!;
        _craneOperator = employee.OfType<CraneOperator>().FirstOrDefault()!;

        _craneResponsibilities = craneResponsibilities;
        _managerResponsibilities = managerResponsibilities;

        _humanResourceContractEmployee = contractEmployee.OfType<HumanResource>().FirstOrDefault()!;
        _craneOperatorContractEmployee = contractEmployee.OfType<CraneOperator>().FirstOrDefault()!;
    }

    public void ProcessCraneOperator()
    {
        var craneOperator = _craneOperator as CraneOperator;

        if (craneOperator is not null)
        {
            craneOperator.FirstName = "James";
            craneOperator.LastName = "Thompson";
            craneOperator.JobTitle = "Crane Operator";
            craneOperator.JoinDate = new DateTime(2010, 01, 01);
            craneOperator.ContractEndDate = new DateTime(2025, 01, 01);

            _craneResponsibilities.InspectCrane();
            _craneResponsibilities.OperateCrane();

            _craneOperatorContractEmployee.ContractEndDate = new DateTime(2025, 01, 01);
            _craneOperatorContractEmployee.RenewContract();

            Console.WriteLine(craneOperator.GetBasicInformation());
            Console.WriteLine($"Years worked: {craneOperator.GetFullYearWorked()}");
        }
        else
        {
            Console.WriteLine("The employee is not a CraneOperator.");
        }
    }

    public void ProcessEmployee()
    {
        var processEmployee = _humanResource as HumanResource;
        if (processEmployee is not null)
        {
            processEmployee.Id = 1;
            processEmployee.FirstName = "Bob";
            processEmployee.LastName = "Tom";
            processEmployee.JobTitle = "Hacker";
            processEmployee.GetBasicInformation().Dump();

            _managerResponsibilities.ConductMeeting();
            _managerResponsibilities.AssignTasks();

            _humanResourceContractEmployee.ContractEndDate = new DateTime(2025, 01, 01);
            _humanResourceContractEmployee.RenewContract();
        }
    }
}