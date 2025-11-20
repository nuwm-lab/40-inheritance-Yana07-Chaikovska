using System;

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

        // Масив векторів — згідно з вимогою завдання
        DirectionVector[] vectors = new DirectionVector[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nВведення даних для вектора #{i + 1}:");

            double x, y;

            // X
            while (true)
            {
                Console.Write("  X = ");
                if (double.TryParse(Console.ReadLine(), out x))
                    break;

                Console.WriteLine("  Некоректне число, спробуйте ще.");
            }

            // Y
            while (true)
            {
                Console.Write("  Y = ");
                if (double.TryParse(Console.ReadLine(), out y))
                    break;

                Console.WriteLine("  Некоректне число, спробуйте ще.");
            }

            vectors[i] = new DirectionVector(x, y);
        }

        // Пошук вектора з найбільшою довжиною
        DirectionVector max = vectors[0];

        for (int i = 1; i < n; i++)
        {
            if (vectors[i].LengthSquared > max.LengthSquared)
            {
                max = vectors[i];
            }
        }

        Console.WriteLine("\nНайдовший вектор: " + max);
        Console.WriteLine($"Довжина: {max.Length:F3}");
    }
}
