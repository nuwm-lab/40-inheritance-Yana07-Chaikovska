using System;

public class FractionalLinearFunction
{
    // Закриті поля (інкапсуляція)
    private double a1, a0;
    private double b1, b0;

    // Конструктор
    public FractionalLinearFunction(double a1, double a0, double b1, double b0)
    {
        SetCoefficients(a1, a0, b1, b0);
    }

    // Метод завдання коефіцієнтів
    public virtual void SetCoefficients(double a1, double a0, double b1, double b0)
    {
        this.a1 = a1;
        this.a0 = a0;
        this.b1 = b1;
        this.b0 = b0;
    }

    // Метод виведення коефіцієнтів
    public virtual void Print()
    {
        Console.WriteLine("Дробово-лінійна функція:");
        Console.WriteLine($"  Чисельник:   {a1}·x + {a0}");
        Console.WriteLine($"  Знаменник:   {b1}·x + {b0}");
    }

    // Обчислення значення функції в точці x0
    public virtual double Value(double x0)
    {
        double numerator = a1 * x0 + a0;
        double denominator = b1 * x0 + b0;
        return numerator / denominator;
    }
}

// =====================================================================

public class FractionFunction : FractionalLinearFunction
{
    private double a2, b2;

    // Конструктор
    public FractionFunction(double a2, double a1, double a0,
                            double b2, double b1, double b0)
        : base(a1, a0, b1, b0)
    {
        this.a2 = a2;
        this.b2 = b2;
    }

    // Перевантажений метод завдання коефіцієнтів
    public void SetCoefficients(double a2, double a1, double a0,
                                double b2, double b1, double b0)
    {
        base.SetCoefficients(a1, a0, b1, b0);
        this.a2 = a2;
        this.b2 = b2;
    }

    // Перевизначений метод виведення
    public override void Print()
    {
        Console.WriteLine("Дробова функція (квадрат/квадрат):");
        Console.WriteLine($"  Чисельник:   {a2}·x² + {GetA1()}·x + {GetA0()}");
        Console.WriteLine($"  Знаменник:   {b2}·x² + {GetB1()}·x + {GetB0()}");
    }

    // Метод доступу до захищених значень (інкапсуляція)
    private double GetA1() => typeof(FractionalLinearFunction)
        .GetField("a1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        .GetValue(this) as double? ?? 0;

    private double GetA0() => typeof(FractionalLinearFunction)
        .GetField("a0", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        .GetValue(this) as double? ?? 0;

    private double GetB1() => typeof(FractionalLinearFunction)
        .GetField("b1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        .GetValue(this) as double? ?? 0;

    private double GetB0() => typeof(FractionalLinearFunction)
        .GetField("b0", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        .GetValue(this) as double? ?? 0;

    // Обчислення значення в точці
    public override double Value(double x0)
    {
        double numerator = a2 * x0 * x0 + GetA1() * x0 + GetA0();
        double denominator = b2 * x0 * x0 + GetB1() * x0 + GetB0();
        return numerator / denominator;
    }
}

// =====================================================================

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Дробово-лінійна функція
        FractionalLinearFunction f1 = new FractionalLinearFunction(2, 5, 1, 3);
        f1.Print();
        Console.WriteLine("Значення при x0 = 2: " + f1.Value(2));

        Console.WriteLine();

        // Дробова функція (квадрат/квадрат)
        FractionFunction f2 = new FractionFunction(1, 2, 3, 4, 5, 6);
        f2.Print();
        Console.WriteLine("Значення при x0 = 2: " + f2.Value(2));
    }
}
