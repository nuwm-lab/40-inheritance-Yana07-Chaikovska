using System;

namespace Geometry3D
{
    /// <summary>
    /// Абстрактний базовий клас для тіл у 3D з координатами центра.
    /// Містить координати центра (b1,b2,b3) та віртуальні методи для об'єму і площі.
    /// </summary>
    public abstract class Solid3D
    {
        private double _centerX;
        private double _centerY;
        private double _centerZ;

        /// <summary>Координата центра X (b1).</summary>
        public double CenterX
        {
            get => _centerX;
            set => _centerX = value;
        }

        /// <summary>Координата центра Y (b2).</summary>
        public double CenterY
        {
            get => _centerY;
            set => _centerY = value;
        }

        /// <summary>Координата центра Z (b3).</summary>
        public double CenterZ
        {
            get => _centerZ;
            set => _centerZ = value;
        }

        /// <summary>Конструктор базового класу з центром (b1,b2,b3).</summary>
        protected Solid3D(double centerX = 0, double centerY = 0, double centerZ = 0)
        {
            _centerX = centerX;
            _centerY = centerY;
            _centerZ = centerZ;
        }

        /// <summary>Задати центр одразу трьома координатами.</summary>
        public void SetCenter(double x, double y, double z)
        {
            CenterX = x; CenterY = y; CenterZ = z;
        }

        /// <summary>Повернути рядок з інформацією про центр (b1,b2,b3).</summary>
        public virtual string GetCenterInfo() => $"Center (b1,b2,b3) = ({CenterX}, {CenterY}, {CenterZ})";

        /// <summary>Об'єм тіла (повинно бути реалізовано в похідних).</summary>
        public abstract double Volume();

        /// <summary>Площа поверхні тіла (повинно бути реалізовано в похідних).</summary>
        public abstract double SurfaceArea();

        /// <summary>Загальна інформація про об'єкт; рекомендується перевизначити в похідних.</summary>
        public virtual string GetInfo() => GetCenterInfo();

        public override string ToString() => GetInfo();
    }

    /// <summary>
    /// Клас Куля (Sphere).
    /// Radius відповідає "a" у варіанті; центр береться з бази (b1,b2,b3).
    /// </summary>
    public class Sphere : Solid3D
    {
        private double _radius;

        /// <summary>Радіус кулі (повинен бути > 0).</summary>
        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Radius повинен бути додатнім (уточніть значення).");
                _radius = value;
            }
        }

        /// <summary>Конструктор кулі (radius, центр b1,b2,b3).</summary>
        public Sphere(double radius, double centerX = 0, double centerY = 0, double centerZ = 0)
            : base(centerX, centerY, centerZ)
        {
            Radius = radius;
        }

        /// <summary>Площа поверхні кулі: 4πr^2</summary>
        public override double SurfaceArea() => 4.0 * Math.PI * Radius * Radius;

        /// <summary>Об'єм кулі: 4/3 π r^3</summary>
        public override double Volume() => 4.0 / 3.0 * Math.PI * Math.Pow(Radius, 3);

        /// <summary>Повертає інформативний рядок з радіусом та центром.</summary>
        public override string GetInfo() =>
            $"Sphere: Radius = {Radius}; {GetCenterInfo()}";

        public override string ToString() => GetInfo();
    }

    /// <summary>
    /// Клас Еліпсоїд (Ellipsoid).
    /// Напіввісі: a = SemiAxisA, b = SemiAxisB, c = SemiAxisC.
    /// Центр -- з базового класу (b1,b2,b3).
    /// Площа поверхні -- апроксимація Кнуд Томсена.
    /// </summary>
    public class Ellipsoid : Solid3D
    {
        private double _semiAxisA;
        private double _semiAxisB;
        private double _semiAxisC;

        /// <summary>Напіввісь a (SemiAxisA). Повинна бути > 0.</summary>
        public double SemiAxisA
        {
            get => _semiAxisA;
            set
            {
                if (value <= 0) throw new ArgumentException("SemiAxisA повинен бути додатнім.");
                _semiAxisA = value;
            }
        }

        /// <summary>Напіввісь b (SemiAxisB). Повинна бути > 0.</summary>
        public double SemiAxisB
        {
            get => _semiAxisB;
            set
            {
                if (value <= 0) throw new ArgumentException("SemiAxisB повинен бути додатнім.");
                _semiAxisB = value;
            }
        }

        /// <summary>Напіввісь c (SemiAxisC). Повинна бути > 0.</summary>
        public double SemiAxisC
        {
            get => _semiAxisC;
            set
            {
                if (value <= 0) throw new ArgumentException("SemiAxisC повинен бути додатнім.");
                _semiAxisC = value;
            }
        }

        /// <summary>Конструктор еліпсоїда: a,b,c та опційно центр (b1,b2,b3).</summary>
        public Ellipsoid(double a, double b, double c, double centerX = 0, double centerY = 0, double centerZ = 0)
            : base(centerX, centerY, centerZ)
        {
            SemiAxisA = a;
            SemiAxisB = b;
            SemiAxisC = c;
        }

        /// <summary>
        /// Об'єм еліпсоїда: 4/3 π a b c
        /// </summary>
        public override double Volume() => 4.0 / 3.0 * Math.PI * SemiAxisA * SemiAxisB * SemiAxisC;

        /// <summary>
        /// Площа поверхні -- апроксимація Кнуд Томсена:
        /// S ≈ 4π * [ ( (a^p * b^p) + (a^p * c^p) + (b^p * c^p) ) / 3 ]^(1/p)
        /// p ≈ 1.6075 (добре для більшості еліпсоїдів)
        /// </summary>
        public override double SurfaceArea()
        {
            const double p = 1.6075;
            double a = SemiAxisA;
            double b = SemiAxisB;
            double c = SemiAxisC;

            // Обчислюємо (a^p * b^p) як (a*b)^p, але використовуємо Math.Pow для наочності:
            double term1 = Math.Pow(a * b, p);
            double term2 = Math.Pow(a * c, p);
            double term3 = Math.Pow(b * c, p);

            double mean = (term1 + term2 + term3) / 3.0;
            double approx = 4.0 * Math.PI * Math.Pow(mean, 1.0 / p);
            return approx;
        }

        /// <summary>
        /// Повертає інфо зі значеннями напіввісей (a,b,c) та центра (b1,b2,b3).
        /// </summary>
        public override string GetInfo() =>
            $"Ellipsoid: a={SemiAxisA}, b={SemiAxisB}, c={SemiAxisC}; {GetCenterInfo()}";

        public override string ToString() => GetInfo();
    }

    class Program
    {
        static void Main()
        {
            try
            {
                // Куля: radius = 5, центр (1,2,3)
                Sphere s = new Sphere(5.0, 1.0, 2.0, 3.0);
                Console.WriteLine(s);
                Console.WriteLine($"SurfaceArea = {s.SurfaceArea():F6}");
                Console.WriteLine($"Volume      = {s.Volume():F6}");
                Console.WriteLine();

                // Еліпсоїд: a=5, b=3, c=2, центр (0,0,0)
                Ellipsoid e = new Ellipsoid(5.0, 3.0, 2.0);
                Console.WriteLine(e);
                Console.WriteLine($"SurfaceArea (approx) = {e.SurfaceArea():F6}");
                Console.WriteLine($"Volume               = {e.Volume():F6}");
                Console.WriteLine();

                // Зміна центра через SetCenter
                e.SetCenter(1.0, -1.0, 0.5);
                Console.WriteLine("Після зміни центра:");
                Console.WriteLine(e);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Помилка ініціалізації: " + ex.Message);
            }
        }
    }
}

