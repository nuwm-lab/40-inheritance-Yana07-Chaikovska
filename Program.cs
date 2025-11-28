using System;

namespace FractionFunctions
{
    // ----------------------- БАЗОВИЙ КЛАС -------------------------
    class FractionLinear
    {
        // Інкапсульовані коефіцієнти
        protected double a1, a0;
        protected double b1, b0;

        // Метод задавання коефіцієнтів
        public virtual void SetCoefficients(double a1, double a0, double b1, double b0)
        {
            this.a1 = a1;
            this.a0 = a0;
            this.b1 = b1;
            this.b0 = b0;
        }

        // Виведення коефіцієнтів
        public virtual void Print()
        {
            Console.WriteLine($"f(x) = ({a1}x + {a0}) / ({b1}x + {b0})");
        }

        // Обчислення значення в точці
        public virtual double Value(double x)
        {
            double denominator = b1 * x + b0;
            if (denominator == 0)
                throw new DivideByZeroException("Знаменник дорівнює нулю!");

            return (a1 * x + a0) / denominator;
        }
    }

    // ----------------------- ПОХІДНИЙ КЛАС -------------------------
    class FractionQuadratic : FractionLinear
    {
        protected double a2, b2;

        // Перевантажений метод задавання коефіцієнтів
        public void SetCoefficients(double a2, double a1, double a0, double b2, double b1, double b0)
        {
            this.a2 = a2;
            this.a1 = a1;
            this.a0 = a0;

            this.b2 = b2;
            this.b1 = b1;
            this.b0 = b0;
        }

        // Перевантажений метод виведення
        public override void Print()
        {
            Console.WriteLine($"g(x) = ({a2}x^2 + {a1}x + {a0}) / ({b2}x^2 + {b1}x + {b0})");
        }

        // Перевантажений метод обчислення
        public override double Value(double x)
        {
            double denominator = b2 * x * x + b1 * x + b0;
            if (denominator == 0)
                throw new DivideByZeroException("Знаменник дорівнює нулю!");

            return (a2 * x * x + a1 * x + a0) / denominator;
        }
    }

    // ----------------------- ТЕСТУВАННЯ -------------------------
    class Program
    {
        static void Main()
        {
            // Створення об'єкту дробово-лінійної функції
            FractionLinear f = new FractionLinear();
            f.SetCoefficients(2, 3, 4, 5);  // f(x) = (2x + 3) / (4x + 5)
            f.Print();
            Console.WriteLine("f(2) = " + f.Value(2));

            Console.WriteLine();

            // Створення об'єкту дробової квадратичної функції
            FractionQuadratic g = new FractionQuadratic();
            g.SetCoefficients(1, 2, 3, 1, 0, 4); // g(x) = (1x² + 2x + 3) / (1x² + 0x + 4)
            g.Print();
            Console.WriteLine("g(2) = " + g.Value(2));
        }
    }
}
