namespace Learning_Interfaces.ServiceInterfaces;

public interface IApartment
{
    int Id { get; set; }
    int Floor { get; set; }
    double SizeInSquareMeters { get; set; }
}
