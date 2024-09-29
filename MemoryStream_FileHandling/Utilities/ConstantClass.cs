namespace MemoryStream_FileHandling.Utilities;

public static class ConstantClass
{
    public const int IdOffset = 0;
    public const int IdLength = 16;

    public const int FirstNameOffset = 16;
    public const int FirstNameLength = 40;

    public const int LastNameOffset = 56;
    public const int LastNameLength = 40;

    public const int SalaryOffset = 96;
    public const int SalaryLength = 20;

    public const int GenderOffset = 116;
    public const int GenderLength = 4;

    public const int IsManagerOffset = 120;
    public const int IsManagerLength = 10;

    public const int RecordLength = IdLength + FirstNameLength + LastNameLength + SalaryLength + GenderLength + IsManagerLength;

}
