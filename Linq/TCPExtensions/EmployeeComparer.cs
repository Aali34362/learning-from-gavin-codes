using System.Diagnostics.CodeAnalysis;
using TCPData;

namespace TCPExtensions;

public class EmployeeComparer : IEqualityComparer<Employee>
{
    public bool Equals([AllowNull] Employee x, [AllowNull] Employee y) =>
        (x?.Id == y?.Id && x?.FirstName?.ToLower() == y?.FirstName?.ToLower() && x?.LastName?.ToLower() == y?.LastName?.ToLower()) ? true : false;
           
    public int GetHashCode([DisallowNull] Employee obj) => obj.Id.GetHashCode();
}
