using OneVariableFunction = System.Func<double, double>;
using FunctionName = System.String;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Drawing;
using System;
using System.CodeDom.Compiler;

namespace Task2
{
    public class Task2
    {

        /*
        * В этом задании необходимо написать программу, способную табулировать сразу несколько
        * функций одной вещественной переменной на одном заданном отрезке.
        */


        // Сформируйте набор как минимум из десяти вещественных функций одной переменной
        internal static Dictionary<FunctionName, OneVariableFunction> AvailableFunctions =
                new Dictionary<FunctionName, OneVariableFunction>
                {
                    { "square", x => x * x },
                    { "sqrt", Math.Sqrt },
                    { "frac", x => 1 / x },
                    { "log", Math.Log },
                    { "log2", Math.Log2 },
                    { "log10", Math.Log10 },
                    { "exp",  Math.Exp},
                    { "sin", Math.Sin },
                    { "cos", Math.Cos },
                    { "tg",  Math.Tan},
                };

        // Тип данных для представления входных данных
        internal record InputData(double FromX, double ToX, int NumberOfPoints, List<string> FunctionNames);

        // Чтение входных данных из параметров командной строки
        private static InputData prepareData(string[] args)
        {
            var FromX = double.Parse(args[0]);
            var ToX = double.Parse(args[1]);
            var NumberOfPoints = int.Parse(args[2]);

            var FunctionNames = new List<FunctionName>();

            foreach (var arg in new ArraySegment<string>(args, 3, args.Length - 3))
            {
                FunctionNames.Add(arg);
            }

            return new InputData(FromX, ToX, NumberOfPoints, FunctionNames);
        }

        // Тип данных для представления таблицы значений функций
        // с заголовками столбцов и строками (первый столбец --- значение x,
        // остальные столбцы --- значения функций). Одно из полей --- количество знаков
        // после десятичной точки.
        internal record FunctionTable
        {
            internal static List<FunctionName> FunctionNames { get; set; }
            internal static Dictionary<double, List<double>> Points { get; set; }
            internal static int Indent { get; set; }

            internal FunctionTable(List<FunctionName> functionNames, Dictionary<double, List<double>> points, int indent)
            {
                FunctionNames = functionNames;
                Points = points;
                Indent = indent;
            }

            // Код, возвращающий строковое представление таблицы (с использованием StringBuilder)
            // Столбец x выравнивается по левому краю, все остальные столбцы по правому.
            // Для форматирования можно использовать функцию String.Format.
            public override string ToString()
            {
                var stringFunctionTable = new StringBuilder();

                string names = new String("points".PadRight(Indent));

                foreach (var func in FunctionNames)
                {
                    names += func.PadLeft(Indent);
                }

                stringFunctionTable.Append(names + Environment.NewLine);

                foreach (var point in Points)
                {
                    var row = point.Key.ToString().PadRight(Indent);

                    foreach (var value in point.Value)
                    {
                        row += value.ToString().PadLeft(Indent);
                    }

                    stringFunctionTable.Append(row + Environment.NewLine);
                }

                return stringFunctionTable.ToString().Substring(0, stringFunctionTable.Length - 1);
            }
        }

        //Возвращает таблицу значений заданных функций на заданном отрезке [fromX, toX]
        //с заданным количеством точек.
        internal static FunctionTable tabulate(InputData input)
        {
            var step = (input.ToX - input.FromX) / (input.NumberOfPoints - 1);

            var points = new Dictionary<double, List<double>>();

            var maxIndent = input.FunctionNames.Max(x => x.Length);

            for (var i = input.FromX; Math.Abs(i - input.ToX - step) >= 1 / 1e3; i += step)
            {
                var index = Math.Round(i, 2);
                maxIndent = Math.Max(maxIndent, index.ToString().Length);

                points[index] = new List<double>();

                foreach (var func in input.FunctionNames)
                {
                    var point = Math.Round(AvailableFunctions[func](i), 2);
                    points[index].Add(point);
                    maxIndent = Math.Max(maxIndent, point.ToString().Length);

                }
            }

            return new FunctionTable(input.FunctionNames, points, maxIndent + 2);
        }

        public static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            // Входные данные принимаются в аргументах командной строки
            // fromX fromY numberOfPoints function1 function2 function3 ...
            var input = prepareData(args);

            // Табулирование и печать результата
            Console.WriteLine(tabulate(input));
        }
    }
}