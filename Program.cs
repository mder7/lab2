using System;

class Program
{
    static void Main(string[] args)
    {
        double a, b, c;

        // 1) пробуем взять коэффициенты из командной строки
        if (args.Length >= 3)
        {
            if (!double.TryParse(args[0], out a) ||
                !double.TryParse(args[1], out b) ||
                !double.TryParse(args[2], out c))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: неверные параметры командной строки.");
                Console.ResetColor();
                return;
            }
        }
        else
        {
            // 2) ввод с клавиатуры с проверкой
            a = ReadCoef("A");
            b = ReadCoef("B");
            c = ReadCoef("C");
        }

        Console.WriteLine($"Уравнение: {a} * x^4 + {b} * x^2 + {c} = 0");

        var def = Console.ForegroundColor;

        // решение A*x^4 + B*x^2 + C = 0
        if (a == 0)
        {
            // B*y + C = 0, y = x^2
            if (b == 0)
            {
                if (c == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Бесконечно много корней (тождество 0 = 0).");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Корней нет.");
                }
                Console.ForegroundColor = def;
                return;
            }

            double y = -c / b;
            PrintRootsFromY(y, def);
        }
        else
        {
            // обычный квадрат по y = x^2
            double d = b * b - 4 * a * c;
            Console.WriteLine($"D = {d}");

            if (d < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Корней нет (D < 0).");
                Console.ForegroundColor = def;
                return;
            }

            if (d == 0)
            {
                double y = -b / (2 * a);
                PrintRootsFromY(y, def);
            }
            else
            {
                double sqrtD = Math.Sqrt(d);
                double y1 = (-b + sqrtD) / (2 * a);
                double y2 = (-b - sqrtD) / (2 * a);

                bool any = false;
                Console.ForegroundColor = ConsoleColor.Green;

                any |= PrintRootsFromYReturn(y1);
                any |= PrintRootsFromYReturn(y2);

                if (!any)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Действительных корней нет (y1 < 0 и y2 < 0).");
                }

                Console.ForegroundColor = def;
            }
        }
    }

    // ввод одного коэффициента с проверкой
    static double ReadCoef(string name)
    {
        double x;
        while (true)
        {
            Console.Write($"{name} = ");
            if (double.TryParse(Console.ReadLine(), out x))
                return x;

            Console.WriteLine("Некорректный ввод, попробуйте ещё раз.");
        }
    }

    // печать корней по одному значению y = x^2
    static void PrintRootsFromY(double y, ConsoleColor def)
    {
        if (y < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Действительных корней нет (y < 0).");
        }
        else if (y == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("x = 0");
        }
        else
        {
            double s = Math.Sqrt(y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"x1 = {s}");
            Console.WriteLine($"x2 = {-s}");
        }

        Console.ForegroundColor = def;
    }

    // тот же вывод, но для случая двух y, возвращает были ли корни
    static bool PrintRootsFromYReturn(double y)
    {
        if (y < 0)
            return false;

        if (y == 0)
        {
            Console.WriteLine("x = 0");
        }
        else
        {
            double s = Math.Sqrt(y);
            Console.WriteLine($"x = {s}");
            Console.WriteLine($"x = {-s}");
        }

        return true;
    }
}
