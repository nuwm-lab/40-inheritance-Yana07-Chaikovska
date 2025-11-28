using System;

class Sphere
{
    private double b1, b2, b3; // координати центру
    private double radius;    // радіус

    public double B1 { get => b1; set => b1 = value; }
    public double B2 { get => b2; set => b2 = value; }
    public double B3 { get => b3; set => b3 = value; }

    public double Radius
    {
        get => radius;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Радіус має бути > 0");
            radius = value;
        }
    }

    public virtual void SetCoefficients(double b1, double b2, double b3, double r)
    {
        B1 = b1;
        B2 = b2;
        B3 = b3;
        Radius = r;
    }

    public virtual void Print()
    {
        Console.WriteLine($"Куля: центр ({B1}, {B2}, {B3}), радіус = {Radius}");
    }

    public virtual double Volume()
    {
        return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
    }
}


class Ellipsoid : Sphere
{
    private double a1, a2, a3;

    public double A1 { get => a1; set => a1 = value; }
    public double A2 { get => a2; set => a2 = value; }
    public double A3 { get => a3; set => a3 = value; }

    public override void SetCoefficients(double b1, double b2, double b3, double r)
    {
        base.SetCoefficients(b1, b2, b3, r);
    }

    public void SetCoefficients(double b1, double b2, double b3, double r,
                                double a1, double a2, double a3)
    {
        base.SetCoefficients(b1, b2, b3, r);
        A1 = a1;
        A2 = a2;
        A3 = a3;
    }

    public override void Print()
    {
        Console.WriteLine(
            $"Еліпсоїд: центр ({B1}, {B2}, {B3}), радіус-кулі = {Radius}, " +
            $"півосі: a1={A1}, a2={A2}, a3={A3}"
        );
    }

    public override double Volume()
    {
        return (4.0 / 3.0) * Math.PI * A1 * A2 * A3;
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Тест класу Sphere ===");
        Sphere s = new Sphere();
        s.SetCoefficients(0, 0, 0, 5);
        s.Print();
        Console.WriteLine($"Обʼєм кулі = {s.Volume():F3}");

        Console.WriteLine("\n=== Тест класу Ellipsoid ===");
        Ellipsoid e = new Ellipsoid();
        e.SetCoefficients(0, 0, 0, 3, 2, 4, 1);
        e.Print();
        Console.WriteLine($"Обʼєм еліпсоїда = {e.Volume():F3}");
    }
}
