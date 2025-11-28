using System;

class Sphere
{
    private double _b1, _b2, _b3;  
    private double _radius;

    public double B1 { get => _b1; set => _b1 = value; }
    public double B2 { get => _b2; set => _b2 = value; }
    public double B3 { get => _b3; set => _b3 = value; }

    public double Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Радіус має бути > 0");
            _radius = value;
        }
    }

    public Sphere(double b1, double b2, double b3, double radius)
    {
        B1 = b1;
        B2 = b2;
        B3 = b3;
        Radius = radius;
    }

    public virtual double Volume()
    {
        return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
    }

    public override string ToString()
    {
        return $"Sphere: center=({B1},{B2},{B3}), R={Radius}";
    }
}

class Ellipsoid : Sphere
{
    private double _a1, _a2, _a3;

    public double A1
    {
        get => _a1;
        set
        {
            if (value <= 0)
                throw new ArgumentException("A1 має бути > 0");
            _a1 = value;
        }
    }
    public double A2
    {
        get => _a2;
        set
        {
            if (value <= 0)
                throw new ArgumentException("A2 має бути > 0");
            _a2 = value;
        }
    }
    public double A3
    {
        get => _a3;
        set
        {
            if (value <= 0)
                throw new ArgumentException("A3 має бути > 0");
            _a3 = value;
        }
    }

    // radius тут використовується як радіус опорної сфери (як просив викладач)
    public Ellipsoid(double b1, double b2, double b3, double radius,
                     double a1, double a2, double a3)
        : base(b1, b2, b3, radius)
    {
        A1 = a1;
        A2 = a2;
        A3 = a3;
    }

    public override double Volume()
    {
        return (4.0 / 3.0) * Math.PI * A1 * A2 * A3;
    }

    public override string ToString()
    {
        return $"Ellipsoid: center=({B1},{B2},{B3}), axes=({A1},{A2},{A3}), sphereR={Radius}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Sphere Test ===");
        Sphere s = new Sphere(0, 0, 0, 5);
        Console.WriteLine(s);
        Console.WriteLine($"Volume: {s.Volume():F4}");

        Console.WriteLine("\n=== Ellipsoid Test ===");
        Ellipsoid e = new Ellipsoid(0, 0, 0, 3, 2, 4, 1);
        Console.WriteLine(e);
        Console.WriteLine($"Volume: {e.Volume():F4}");
    }
}
