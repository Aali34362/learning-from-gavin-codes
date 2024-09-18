using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class MechanicalEngineeringProgram
{
    public static void MechanicalEngineeringProgramMain()
    {
        Engineering employee = new MechanicalEngineering();
        employee.Id = 1;
        employee.FirstName = "ABC";
        employee.LastName = "LMNO";
        employee.JobTitle = "Motor Developer";
        employee.JoinDate = new DateTime(2010, 01, 01);
        employee.GetBasicInformation().Dump();
        employee.work();
    }
}

public class MechanicalEngineering : Engineering
{
    public override void work()
    {
        $"Working as Motors Engine Developer".Dump();
    }
}
