namespace BinaryFileHandling.Model;

public class Employee
{
    public const int NameSize = 20;

    private string _firstName = String.Empty;
    private string _lastName = String.Empty;

    public int Id { get; set; }
    public string FirstName
    {
        get { return (_firstName.Length > NameSize) ? _firstName.Substring(0, NameSize) : _firstName.PadRight(NameSize); }
        set { _firstName = value; }
    }
    public string LastName
    {
        get { return (_lastName.Length > NameSize) ? _lastName.Substring(0, NameSize) : _lastName.PadRight(NameSize); }
        set { _lastName = value; }
    }
    public decimal Salary { get;set; }
    public char Gender { get; set; }
    public bool IsManager { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Employee other)
        {
            return Id == other.Id &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Salary == other.Salary &&
                   Gender == other.Gender &&
                   IsManager == other.IsManager;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Salary, Gender, IsManager);
    }
}
