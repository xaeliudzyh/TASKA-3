using System.Reflection;
using System.Text;
using OneVariableFunction = System.Func<double, double>;
using FunctionName = System.String;

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
                { "sin", Math.Sin },
                { "cube", x => x*x*x },
                { "sqrt", Math.Sqrt },
                { "tens",x=> x/10},
                {"hundreds",x=> x/100},
                {"cos",Math.Cos},
                {"abs", Math.Abs},
                {"log10",Math.Log10},
                {"logE", Math.Log}
                
            };

// Тип данных для представления входных данных
        internal record InputData(double FromX, double ToX, int NumberOfPoints, List<string> FunctionNames);

// Чтение входных данных из параметров командной строки
        private static InputData? prepareData(string[] args)
        {
            var FromX = double.Parse(args[0]);
            var ToX = double.Parse(args[1]);
            var NumberOfPoints = int.Parse(args[2]);
            var FunctionNames = new List<FunctionName>();
            for (int i = 3; i < args.Length - 3; i++)
            {
                FunctionNames.Add(args.ToString());
            }
            return new InputData(FromX, ToX, NumberOfPoints, FunctionNames);
        }

// Тип данных для представления таблицы значений функций
// с заголовками столбцов и строками (первый столбец --- значение x,
// остальные столбцы --- значения функций). Одно из полей --- количество знаков
// после десятичной точки.
        internal record FunctionTable(InputData inData)
        {
            
            // Код, возвращающий строковое представление таблицы (с использованием StringBuilder)
            // Столбец x выравнивается по левому краю, все остальные столбцы по правому.
            // Для форматирования можно использовать функцию String.Format.
            public override string ToString()
            {
                StringBuilder funcTable=new StringBuilder();
                funcTable.Append(String.Format("0,from"))
                /*
                for (double i = 0; i <= (inData.FromX - inData.ToX) * Math.Pow(10, inData.NumberOfPoints); i++)
                {
                    for (int j = 0; j <inData.FunctionNames.Count+2; j++)
                    {
                        string t ="",h=inData.FromX.ToString();
                        for (int m = 0; m < 2 + inData.NumberOfPoints; m++ )
                        {
                            t += h[(int)i];
                        }
                        h = t.PadLeft(3 + inData.NumberOfPoints, ' ');
                        for (int m = 0; m < inData.FunctionNames.Count; m++)
                        {
                            double p= AvailableFunctions(inData.FunctionNames[m],i);
                        }

                    }
                }
            }*/
        }

/*
 * Возвращает таблицу значений заданных функций на заданном отрезке [fromX, toX]
 * с заданным количеством точек.
 */
        internal static FunctionTable tabulate(InputData input)
        {
            throw new NotImplementedException();
        }
        
        public static void Main(string[] args)
        {
            // Входные данные принимаются в аргументах командной строки
            // fromX fromY numberOfPoints function1 function2 function3 ...

            var input = prepareData(args);

            // Собственно табулирование и печать результата (что надо поменять в этой строке?):
            Console.WriteLine(tabulate(input));
        }
        
        private static T TODO<T>()
        {
            throw new NotImplementedException();
        }

    }
}