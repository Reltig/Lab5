// See https://aka.ms/new-console-template for more information

using System.Xml;
using Lab5;

//Задание случайной величины через фнкцию плотности на отрезки [0, 1]
var Z = new RandomValue(x => x, 0, 1);
var val = Z.GenerateRandomValues(15);
Array.Sort(val);
foreach (var e in val)
{
    Console.Write($"{e} ");
}

//Характеристики выборки
var Zs = new Selection(val);
Console.WriteLine($"ExpectedValue: {Zs.ExpectedValue}");
Console.WriteLine($"Dispertion: {Zs.Dispertion}");
Console.WriteLine($"StandardDeviation: {Zs.StandardDeviation}");
Console.WriteLine($"Asymmetry: {Zs.Asymmetry}");
Console.WriteLine($"Kurtosis: {Zs.Kurtosis}");
Console.WriteLine($"CoefficientOfVariation: {Zs.CoefficientOfVariation}");

Console.WriteLine();

//Работа с курсом валют
XmlDocument xDoc = new XmlDocument();
xDoc.Load("currency.xml");
XmlElement? xRoot = xDoc.DocumentElement;
var dict = new Dictionary<string, List<double>>();
if (xRoot != null)
{
    foreach (XmlElement date in xRoot)
    {
        foreach (XmlElement curr in date)
        {
            if (!dict.ContainsKey(curr.Name))
                dict[curr.Name] = new();
            dict[curr.Name].Add(double.Parse(curr.FirstChild.Value.Replace('.',',')));
        }
    }
}

Console.WriteLine("Значение для курсов валют");

foreach (var key in dict.Keys)
{
    Console.WriteLine($"{key}:" );
    var v = new Selection(dict[key].ToArray());
    Console.WriteLine($"ExpectedValue: {v.ExpectedValue}");
    Console.WriteLine($"Dispertion: {v.Dispertion}");
    Console.WriteLine($"StandardDeviation: {v.StandardDeviation}");
    Console.WriteLine($"Asymmetry: {v.Asymmetry}");
    Console.WriteLine($"Kurtosis: {v.Kurtosis}");
    Console.WriteLine($"CoefficientOfVariation: {v.CoefficientOfVariation}");

    Console.WriteLine();
}

//Генератор равномерно распределённого выбора
var d = new Disk(new []{1.2, 3.2, 7, 0.1, 144});
while (d.IsNotEmpty)
{
    Console.WriteLine(d.Take(Z.GenerateRandomValue()));
}

//Плотность суммы величнин
double Density(double x)
{
    if (0 <= x && x <= 2)
        return 0.5;
    return 0;
}

var X = new RandomValue(Density, 0, 2, isDistributionFunction:false);
var Y = new RandomValue(Density, 0, 2, isDistributionFunction:false);

var f = RandomValue.DensityOfSum(X, Y);
for (double i = 0; i < 4; i+=0.1)
{
    Console.Write($"x = {i:F1} f(x)={f(i):F2}\n");
}