using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public abstract class ITEngineering : Engineering, IComputerEngineering
{
    public string? field { get; set; }

    public override void work()
    {
        $"Working in {field}".Dump();
    }
    public override string GetBasicInformation()
    {
        return base.GetBasicInformation() + $"{field}";
    }
}
