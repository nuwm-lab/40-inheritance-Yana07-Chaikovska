using System;

namespace Geometry3D
{
    /// <summary>
    /// Базовий клас для геометричної фігури "Куля".
    /// </summary>
    public class Sphere
    {
        private double _radius;

        /// <summary>
        /// Радіус кулі.
        /// </summary>
        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Радіус повинен бути додатнім.");
                _radius = value;
            }
        }

        /// <summary>
        /// Конструктор кулі.
        /// </summary>
        public Sphere(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Обчислення площі поверхні кулі.
        /// </summary>
        public virtual double SurfaceArea() => 4 * Math.PI * _radius * _radius;

        /// <summary>
        /// Обчислення об'єму кулі.
        /// </summary>
        public virtual double Volume() => 4.0 / 3.0 * Math.PI * Math.Pow(_radius, 3);

        /// <summary>
        /// Текстове представлення об’єкта.
        /// </summary>
        public override string ToString() =>
            $"Куля: радіус = {_radius}";
    }

    /// <summary>
    /// Похідний клас: Еліпсоїд.
    /// </summary>
    public class Ellipsoid : Sphere
    {
        private double _b, _c;

        /// <summary>
        /// Напіввісі еліпсоїда.
        /// </summary>
        public double B
        {
            get => _b;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Напіввісь B повинна бути додатною.");
                _b = value;
            }
        }

        public double C
        {
            get => _c;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Напіввісь C повинна бути додатною.");
                _c = value;
            }
        }

        /// <summary>
        /// Конструктор еліпсоїда.
        /// A — успадковується як Radius.
        /// </summary>
        public Ellipsoid(double a, double b, double c) : base(a)
        {
            B = b;
            C = c;
        }

        /// <summary>
        /// Обчислення об'єму еліпсоїда.
        /// </summary>
        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * Radius * B * C;
        }

        /// <summary>
        /// Площа поверхні (апроксимація Кнуд Томсена).
        /// </summary>
        public override double SurfaceArea()
        {
            double p = 1.6;
            double a = Radius;

            return 4 * Math.PI *
                   Math.Pow(
                       (Math.Pow(a * b: a, p) + Math.Pow(B, p) + Math.Pow(C, p)) / 3,
                       1.0 / p);
        }

        public override string ToString() =>
            $"Еліпсоїд: a={Radius}, b={B}, c={C}";
    }

    class Program
    {
        static void Main()
        {
            Sphere sphere = new Sphere(5);
            Console.WriteLine(sphere);
            Console.WriteLine("Площа: " + sphere.SurfaceArea());
            Console.WriteLine("Об’єм: " + sphere.Volume());
            Console.WriteLine();

            Ellipsoid ellipsoid = new Ellipsoid(5, 3, 2);
            Console.WriteLine(ellipsoid);
            Console.WriteLine("Площа: " + ellipsoid.SurfaceArea());
            Console.WriteLine("Об’єм: " + ellipsoid.Volume());
        }
    }
}
