namespace Learning_Interfaces.ServiceInterfaces;

public interface IEmployee
{
    int Id { get; set; }
    string? JobTitle { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    decimal? AnnualSalary { get; set; }
    char? Gender { get; set; }
    DateTime JoinDate { get; set; }
    string? HighestQualification { get; set; }
    string GetBasicInformation();
    string GetAdditionalInformation()
    {
        return $"Join Date : {JoinDate.ToString("yyyy-MM-dd")} and Gender : {Gender}";
    }
}
