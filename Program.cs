using System;

public class DirectionVector
{
    // Приватні поля
    private double _x;
    private double _y;

    // Властивості
    public double X
    {
        get => _x;
        set => _x = value;
    }

    public double Y
    {
        get => _y;
        set => _y = value;
    }

    // Конструктор
    public DirectionVector(double x, double y)
    {
        _x = x;
        _y = y;
    }

    // Довжина вектора (корінь)
    public double Length => Math.Sqrt(_x * _x + _y * _y);

    // Квадрат довжини для оптимізації порівняння
    public double LengthSquared => _x * _x + _y * _y;

    // Для красивого виводу
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введіть кількість векторів n (>0): ");

        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Помилка: n повинно бути додатним числом.");
            return;
        }

        DirectionVector[] vectors = new DirectionVector[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nВведення даних для вектора #{i + 1}:");

            double x, y;

            // Ввід X
            while (true)
            {
                Console.Write("  X = ");
                if (double.TryParse(Console.ReadLine(), out x))
                    break;

                Console.WriteLine("  Некоректне число. Спробуйте ще раз.");
            }

            // Ввід Y
            while (true)
            {
                Console.Write("  Y = ");
                if (double.TryParse(Console.ReadLine(), out y))
                    break;

                Console.WriteLine("  Некоректне число. Спробуйте ще раз.");
            }

            vectors[i] = new DirectionVector(x, y);
        }

        // Пошук найдовшого вектора
        DirectionVector maxVector = vectors[0];

        for (int i = 1; i < n; i++)
        {
            if (vectors[i].LengthSquared > maxVector.LengthSquared)
            {
                maxVector = vectors[i];
            }
        }

        // Вивід результату
        Console.WriteLine("\nНайдовший вектор: " + maxVector);
        Console.WriteLine($"Довжина: {maxVector.Length:F3}");
    }
}
