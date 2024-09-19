using Dumpify;

namespace Learning_Interfaces.ServiceImplementations;

public static class CivilEngineeringProgram
{
    public static void CivilEngineeringProgramMain()
    {
        Engineering employee = new CivilEngineering();
        employee.Id = 1;
        employee.FirstName = "XYZ";
        employee.LastName = "TUV";
        employee.JobTitle = "Civil Developer";
        employee.JoinDate = new DateTime(2010, 01, 01);
        employee.GetBasicInformation().Dump();
        employee.work();
        //employee.GetAdditionalInformation();
        
    }
}

public class CivilEngineering : Engineering
{
    public override void work()
    {
        $"Working as Hotels Building Developer".Dump();
    }
}
