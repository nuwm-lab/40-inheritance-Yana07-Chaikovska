using System;

namespace Geometry3D
{
    /// <summary>
    /// Абстрактний базовий клас для тіл у 3D з координатами центра.
    /// Містить (b1,b2,b3) та абстрактні методи для об'єму та площі.
    /// </summary>
    public abstract class Solid3D
    {
        // Авто-властивості замість бекінг-полів
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double CenterZ { get; set; }

        protected Solid3D(double centerX = 0, double centerY = 0, double centerZ = 0)
        {
            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;
        }

        /// --- Методи, яких вимагає ТЗ ---
        public virtual void SetCoefficients(double b1, double b2, double b3)
        {
            CenterX = b1;
            CenterY = b2;
            CenterZ = b3;
        }

        public virtual string PrintCoefficients()
            => $"Center (b1,b2,b3) = ({CenterX}, {CenterY}, {CenterZ})";
        /// ---------------------------------

        public abstract double Volume();
        public abstract double SurfaceArea();

        public virtual string GetInfo() => PrintCoefficients();

        public override string ToString() => GetInfo();
    }


    /// <summary>
    /// Куля: радіус R та центр (b1,b2,b3).
    /// </summary>
    public class Sphere : Solid3D
    {
        private double _radius;

        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Radius повинен бути додатнім.");
                _radius = value;
            }
        }

        public Sphere(double radius, double b1 = 0, double b2 = 0, double b3 = 0)
            : base(b1, b2, b3)
        {
            Radius = radius;
        }

        /// --- ТЗ: Задання коефіцієнтів ---
        public override void SetCoefficients(double radius, double b1, double b2, double b3)
        {
            Radius = radius;
            base.SetCoefficients(b1, b2, b3);
        }

        public override string PrintCoefficients()
            => $"Radius = {Radius}; " + base.PrintCoefficients();
        /// --------------------------------

        public override double SurfaceArea()
            => 4 * Math.PI * Radius * Radius;

        public override double Volume()
            => 4.0 / 3.0 * Math.PI * Math.Pow(Radius, 3);

        public override string GetInfo()
            => "Sphere: " + PrintCoefficients();
    }


    /// <summary>
    /// Еліпсоїд з напіввіссями a,b,c та центром (b1,b2,b3).
    /// </summary>
    public class Ellipsoid : Solid3D
    {
        private double _a, _b, _c;

        public double SemiAxisA
        {
            get => _a;
            set
            {
                if (value <= 0) throw new ArgumentException("SemiAxisA повинен бути додатнім.");
                _a = value;
            }
        }

        public double SemiAxisB
        {
            get => _b;
            set
            {
                if (value <= 0) throw new ArgumentException("SemiAxisB повинен бути додатнім.");
                _b = value;
            }
        }

        public double SemiAxisC
        {
            get => _c;
            set
            {
                if (value <= 0) throw new ArgumentException("SemiAxisC повинен бути додатнім.");
                _c = value;
            }
        }

        public Ellipsoid(double a, double b, double c, double b1 = 0, double b2 = 0, double b3 = 0)
            : base(b1, b2, b3)
        {
            SemiAxisA = a;
            SemiAxisB = b;
            SemiAxisC = c;
        }

        /// --- ТЗ: Задання коефіцієнтів ---
        public override void SetCoefficients(double a, double b, double c)
        {
            SemiAxisA = a;
            SemiAxisB = b;
            SemiAxisC = c;
        }

        public void SetCoefficients(double a, double b, double c, double b1, double b2, double b3)
        {
            SetCoefficients(a, b, c);
            base.SetCoefficients(b1, b2, b3);
        }

        public override string PrintCoefficients()
            => $"a={SemiAxisA}, b={SemiAxisB}, c={SemiAxisC}; " + base.PrintCoefficients();
        /// ---------------------------------

        public override double Volume()
            => 4.0 / 3.0 * Math.PI * SemiAxisA * SemiAxisB * SemiAxisC;

        /// Апроксимація Кнуд Томсена
        public override double SurfaceArea()
        {
            const double p = 1.6075;
            double a = SemiAxisA, b = SemiAxisB, c = SemiAxisC;

            double t1 = Math.Pow(a * b, p);
            double t2 = Math.Pow(a * c, p);
            double t3 = Math.Pow(b * c, p);

            double mean = (t1 + t2 + t3) / 3.0;

            return 4 * Math.PI * Math.Pow(mean, 1.0 / p);
        }

        public override string GetInfo()
            => "Ellipsoid: " + PrintCoefficients();
    }


    class Program
    {
        static void Main()
        {
            try
            {
                Sphere s = new Sphere(5, 1, 2, 3);
                Console.WriteLine(s);
                Console.WriteLine($"SurfaceArea = {s.SurfaceArea():F6}");
                Console.WriteLine($"Volume      = {s.Volume():F6}\n");

                Ellipsoid e = new Ellipsoid(5, 3, 2);
                Console.WriteLine(e);
                Console.WriteLine($"SurfaceArea (approx) = {e.SurfaceArea():F6}");
                Console.WriteLine($"Volume               = {e.Volume():F6}\n");

                Console.WriteLine("Після зміни центра:");
                e.SetCoefficients(5, 3, 2, 1, -1, 0.5);
                Console.WriteLine(e);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
