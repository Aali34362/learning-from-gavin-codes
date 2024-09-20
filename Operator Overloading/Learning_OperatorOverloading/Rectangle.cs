using Dumpify;

namespace Learning_OperatorOverloading;

public static class RectangleProgram
{
    public static void RectangleProgramMain()
    {
        Rectangle rectangle1 = new(1,1);
        Rectangle rectangle2 = new(2,2);
        Rectangle rectangle = rectangle1 + rectangle2;
        rectangle.Dump();
        rectangle.Height.Dump("Height");
        rectangle.Width.Dump("Width");
    }
}

public class Rectangle(int width, int height)
{
    public int Width = width;
    public int Height = height;

    public static Rectangle operator +(Rectangle rectangle1, Rectangle rectangle2) => 
        new(rectangle1.Width + rectangle2.Width, rectangle1.Height + rectangle2.Height);
}
