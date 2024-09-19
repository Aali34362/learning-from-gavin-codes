using Learning_Interfaces.ServiceInterfaces;

namespace Learning_Interfaces.ServiceImplementations;

public static class ApartmentProgram
{
    public static void ApartmentProgramMain()
    {
        Apartment apartment = new Apartment
        {
            Id = 1,
            Floor = 2,
            SizeInSquareMeters = 60
        };

        IDimensionImperial dimensionImperial = apartment;
        IDimensionMetric dimensionMetric = apartment;

        Console.WriteLine($"Apartment size (square feet): {dimensionImperial.GetSize()}");
        Console.WriteLine($"Apartment size (square meters): {dimensionMetric.GetSize()}");
    }
}


public class Apartment : IApartment, IDimensionMetric, IDimensionImperial
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public double SizeInSquareMeters { get; set; }

    double IDimensionMetric.GetSize()
    {
        return SizeInSquareMeters;
    }

    double IDimensionImperial.GetSize()
    {
        return Math.Round(SizeInSquareMeters * 10.764, 2);
    }
}