using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class TesterProgram
{
    public static void TesterProgramMain()
    {
        Engineering employee = new Tester();
        employee.Id = 1;
        employee.FirstName = "XYZ";
        employee.LastName = "TUV";
        employee.JobTitle = "Software Developer";
        //employee.field = "Software Developer";
        employee.JoinDate = new DateTime(2010, 01, 01);
        employee.GetBasicInformation().Dump();
        employee.work();
    }
}

public class Tester : Engineering, IComputerEngineering
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
