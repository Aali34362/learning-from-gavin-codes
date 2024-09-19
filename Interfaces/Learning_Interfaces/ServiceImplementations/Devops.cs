using Dumpify;
using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class DevopsEngineeringProgram
{
    public static void DevopsEngineeringProgramMain()
    {
        ITEngineering employee = new Devops();
        employee.Id = 1;
        employee.FirstName = "XYZ";
        employee.LastName = "TUV";
        employee.JobTitle = "Devops";
        employee.field = "Devops";
        employee.JoinDate = new DateTime(2010, 01, 01);
        employee.GetBasicInformation().Dump();
        employee.work();
    }
}

public class Devops : ITEngineering
{

}
