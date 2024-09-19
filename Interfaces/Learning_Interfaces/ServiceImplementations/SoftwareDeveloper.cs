using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class SoftwareDeveloperProgram
{
    public static void SoftwareDeveloperProgramMain()
    {
        ITEngineering employee = new SoftwareDeveloper();
        employee.Id = 1;
        employee.FirstName = "XYZ";
        employee.LastName = "TUV";
        employee.JobTitle = "Software Developer";
        employee.field = "Software Developer";
        employee.JoinDate = new DateTime(2010, 01, 01);
        employee.GetBasicInformation().Dump();
        employee.work();
    }
}

public class SoftwareDeveloper : ITEngineering
{

}
