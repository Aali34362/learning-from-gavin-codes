namespace Learning_Interfaces;

public static class InterfacesTutorialPart1
{
    public static void InterfacesTutorialMain(string[] args)
    {

        IList<IEmployees> employees = new List<IEmployees>();

        SeedData(employees);

        Navigation(employees);

        Console.ReadKey();

    }

    private static void SeedData(IList<IEmployees> employees)
    {
        ChiefExecutiveOfficer chiefExecutiveOfficer = new ChiefExecutiveOfficer();

        chiefExecutiveOfficer.Id = 1;
        chiefExecutiveOfficer.JobTitle = "CEO";
        chiefExecutiveOfficer.FirstName = "James";
        chiefExecutiveOfficer.LastName = "Jenkins";
        chiefExecutiveOfficer.AnnualSalary = 200000;
        chiefExecutiveOfficer.HighestQualification = "Masters Degree";
        chiefExecutiveOfficer.Gender = 'm';
        chiefExecutiveOfficer.OfficeId = "E120";
        chiefExecutiveOfficer.JoinDate = new DateTime(2018, 1, 12);
        chiefExecutiveOfficer.SecretaryId = 43;
        chiefExecutiveOfficer.PersonalAssistant = 102;

        employees.Add(chiefExecutiveOfficer);

        Manager projectManager = new ProjectManager();

        projectManager.Id = 2;
        projectManager.JobTitle = "Project Manager";
        projectManager.FirstName = "Bob";
        projectManager.LastName = "Jones";
        projectManager.AnnualSalary = 100000;
        projectManager.HighestQualification = "Honours Degree";
        projectManager.Gender = 'm';
        projectManager.OfficeId = "D017";
        projectManager.JoinDate = new DateTime(2015, 4, 1);
        projectManager.SecretaryId = 110;
        projectManager.Id = 2;

        employees.Add(projectManager);

        Manager safetyManager = new SafetyManager();

        safetyManager.Id = 3;
        safetyManager.JobTitle = "Safety Manager";
        safetyManager.FirstName = "Jane";
        safetyManager.LastName = "Summers";
        safetyManager.AnnualSalary = 100000;
        safetyManager.HighestQualification = "Honours Degree";
        safetyManager.Gender = 'f';
        safetyManager.OfficeId = "D018";
        safetyManager.JoinDate = new DateTime(2016, 2, 3);
        safetyManager.SecretaryId = 145;

        employees.Add(safetyManager);

        Employee craneOperator = new CraneOperator();

        craneOperator.Id = 4;
        craneOperator.JobTitle = "Crane Operator";
        craneOperator.FirstName = "Sam";
        craneOperator.LastName = "Drake";
        craneOperator.AnnualSalary = 50000;
        craneOperator.HighestQualification = "Bachelor's Degree";
        craneOperator.Gender = 'm';
        craneOperator.JoinDate = new DateTime(2012, 5, 7);

        employees.Add(craneOperator);

        Employee electrician = new Electrician();

        electrician.Id = 5;
        electrician.JobTitle = "Electrician";
        electrician.FirstName = "James";
        electrician.LastName = "Ross";
        electrician.AnnualSalary = 45000;
        electrician.HighestQualification = "Bachelor's Degree";
        electrician.Gender = 'm';
        electrician.JoinDate = new DateTime(2013, 1, 16);

        employees.Add(electrician);

        Employee electrician2 = new Electrician();

        electrician2.Id = 6;
        electrician2.JobTitle = "Electrician";
        electrician2.FirstName = "Henry";
        electrician2.LastName = "Sanders";
        electrician2.AnnualSalary = 45000;
        electrician2.HighestQualification = "Bachelor's Degree";
        electrician2.Gender = 'm';
        electrician2.JoinDate = new DateTime(2017, 1, 16);

        employees.Add(electrician2);

    }

    private static void WriteHeading()
    {
        Console.WriteLine("The Construction Company");
        Console.WriteLine("------------------------");
        Console.WriteLine();
    }

    private static void WriteEmployeeInformationToScreen(IEmployees employee)
    {
        Console.Clear();

        WriteHeading();

        Console.WriteLine(employee.GetBasicInformation());

    }

    private static void Navigation(IList<IEmployees> employees)
    {
        int counter = 0;

        while (true)
        {
            if (counter < 0 || counter > employees.Count - 1)
            {
                counter = 0;
            }

            WriteEmployeeInformationToScreen(employees[counter]);

            ConsoleKey consoleKey = Console.ReadKey().Key;

            if (consoleKey == ConsoleKey.RightArrow)
            {
                counter++;
            }
            else if (consoleKey == ConsoleKey.LeftArrow)
            {
                counter--;
            }
            else if (consoleKey == ConsoleKey.Spacebar)
            {
                break;
            }


        }
    }


}

public abstract class Employee : IEmployees
{

    public int Id { get; set; }
    public string JobTitle { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual decimal AnnualSalary { get; set; }
    public char Gender { get; set; }
    public DateTime JoinDate { get; set; }
    public string HighestQualification { get; set; }

    public virtual string GetBasicInformation()
    {
        return $"Id: {Id}{Environment.NewLine}Job Title: {JobTitle}{Environment.NewLine}First Name: {FirstName}{Environment.NewLine}Last Name: {LastName}{Environment.NewLine}Annual Salary: {AnnualSalary}";
    }
    public int GetFullYearsWorked()
    {
        DateTime zeroTime = new DateTime(1, 1, 1);

        TimeSpan span = DateTime.Now.Subtract(JoinDate);

        int years = zeroTime.Add(span).Year - 1;

        return years;
    }

    public string GetAdditionalInformation()
    {
        return $"Highest Qualification: {HighestQualification}";
    }
}

public interface IEmployees
{
    int Id { get; set; }
    string JobTitle { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    decimal AnnualSalary { get; set; }
    char Gender { get; set; }
    DateTime JoinDate { get; set; }
    string HighestQualification { get; set; }
    string GetBasicInformation();
    int GetFullYearsWorked();

}

public class CraneOperator : Employee
{
    private decimal GetDangerPayIncrease()
    {
        return 0.06m;
    }

    public override decimal AnnualSalary { get => base.AnnualSalary + (base.AnnualSalary * GetDangerPayIncrease()); }


}

public class Electrician : Employee
{


}

public class DryWallInstaller : Employee
{

}

public interface IManager
{
    string OfficeId { get; set; }
    int SecretaryId { get; set; }

}

public abstract class Manager : Employee, IManager
{
    public string OfficeId { get; set; }
    public int SecretaryId { get; set; }

    public override string GetBasicInformation()
    {
        return base.GetBasicInformation() + $"{Environment.NewLine}Office Id: {OfficeId}{Environment.NewLine}Secretary Id: {SecretaryId}";
    }
}

public class ProjectManager : Manager
{

}

public class SafetyManager : Manager
{

}


public interface IChiefExecutiveOfficer
{
    public int PersonalAssistant { get; set; }
}
public class ChiefExecutiveOfficer : Manager, IChiefExecutiveOfficer
{
    public int PersonalAssistant { get; set; }

    public override string GetBasicInformation()
    {
        return base.GetBasicInformation() + $"{Environment.NewLine}Personal Assistant: {PersonalAssistant} ";
    }
}
