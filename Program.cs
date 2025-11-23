using System;

public class Sphere
{
    protected double _radius;

    public Sphere(double radius)
    {
        SetCoefficients(radius);
    }

    /// <summary>Задати радіус кулі</summary>
    public virtual void SetCoefficients(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Радіус повинен бути > 0.");

        _radius = radius;
    }

    /// <summary>Вивести параметри</summary>
    public virtual void Print()
    {
        Console.WriteLine("Куля:");
        Console.WriteLine($"  Радіус: {_radius}");
    }

    /// <summary>Об'єм кулі: 4/3πR^3</summary>
    public virtual double Volume()
    {
        return 4.0 / 3.0 * Math.PI * Math.Pow(_radius, 3);
    }
}

///////////////////////////////////////////////////////////

public class Ellipsoid : Sphere
{
    protected double _a1, _a2, _a3;

    public Ellipsoid(double a1, double a2, double a3)
        : base(a1)
    {
        SetCoefficients(a1, a2, a3);
    }

    /// <summary>Задати півосі еліпсоїда</summary>
    public void SetCoefficients(double a1, double a2, double a3)
    {
        if (a1 <= 0 || a2 <= 0 || a3 <= 0)
            throw new ArgumentException("Всі півосі мають бути > 0.");

        _a1 = a1;
        _a2 = a2;
        _a3 = a3;

        // базовий радіус = одна з осей, щоб не ламати базу
        base.SetCoefficients(a1);
    }

    public override void Print()
    {
        Console.WriteLine("Еліпсоїд:");
        Console.WriteLine($"  Піввісь a1 = {_a1}");
        Console.WriteLine($"  Піввісь a2 = {_a2}");
        Console.WriteLine($"  Піввісь a3 = {_a3}");
    }

    /// <summary>Об'єм еліпсоїда: 4/3π·a1·a2·a3</summary>
    public override double Volume()
    {
        return 4.0 / 3.0 * Math.PI * _a1 * _a2 * _a3;
    }
}

///////////////////////////////////////////////////////////

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Куля
        Sphere s = new Sphere(5);
        s.Print();
        Console.WriteLine("Об'єм кулі = " + s.Volume());

        Console.WriteLine();

        // Еліпсоїд
        Ellipsoid e = new Ellipsoid(3, 4, 5);
        e.Print();
        Console.WriteLine("Об'єм еліпсоїда = " + e.Volume());
    }
}
