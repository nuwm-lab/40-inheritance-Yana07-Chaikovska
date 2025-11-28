using System;

class FractionLinearFunction
{
    protected double a1, a0; 
    protected double b1, b0;

    public void SetCoefficients(double a1, double a0, double b1, double b0)
    {
        this.a1 = a1;
        this.a0 = a0;
        this.b1 = b1;
        this.b0 = b0;
    }

    public virtual void Print()
    {
        Console.WriteLine($"f(x) = ({a1}x + {a0}) / ({b1}x + {b0})");
    }

    public virtual double Calculate(double x)
    {
        double numerator = a1 * x + a0;
        double denominator = b1 * x + b0;

        if (denominator == 0)
        {
            Console.WriteLine("Помилка: знаменник = 0");
            return double.NaN;
        }

        return numerator / denominator;
    }
}


class FractionFunction : FractionLinearFunction
{
    protected double a2, b2;

    public void SetCoefficients(double a2, double a1, double a0,
                                double b2, double b1, double b0)
    {
        this.a2 = a2;
        this.a1 = a1;
        this.a0 = a0;
        this.b2 = b2;
        this.b1 = b1;
        this.b0 = b0;
    }

    public override void Print()
    {
        Console.WriteLine($"f(x) = ({a2}x^2 + {a1}x + {a0}) / ({b2}x^2 + {b1}x + {b0})");
    }

    public override double Calculate(double x)
    {
        double numerator = a2 * x * x + a1 * x + a0;
        double denominator = b2 * x * x + b1 * x + b0;

        if (denominator == 0)
        {
            Console.WriteLine("Помилка: знаменник = 0");
            return double.NaN;
        }

        return numerator / denominator;
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Дробово-лінійна функція ===");
        FractionLinearFunction f1 = new FractionLinearFunction();
        f1.SetCoefficients(2, 1, 1, 3);  // a1=2, a0=1, b1=1, b0=3
        f1.Print();
        Console.WriteLine("f(2) = " + f1.Calculate(2));

        Console.WriteLine("\n=== Дробова функція ===");
        FractionFunction f2 = new FractionFunction();
        f2.SetCoefficients(1, 2, 3, 4, 5, 6);
        f2.Print();
        Console.WriteLine("f(2) = " + f2.Calculate(2));
    }
}