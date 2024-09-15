namespace Learning_Interfaces.ServiceInterfaces;

public interface IContractEmployee
{
    DateTime ContractEndDate { get; set; }
    void RenewContract();
}
