namespace Lab5;

public class RandomValue
{
    private Func<double, double> distributionFunction;
    private Func<double, double> densityFunction;
    private double left, right;
    private const double accuracy = 0.01;
    private Random rand;

    public RandomValue(
        Func<double, double> func, 
        double left, 
        double right,
        bool isDistributionFunction = true)
    {
        this.left = left;
        this.right = right;
        rand = new Random(DateTime.Now.Second);
        
        if (isDistributionFunction)
            distributionFunction = func;
        else
        {
            densityFunction = func;
            distributionFunction = Distribution;
        }
    }

    private double Distribution(double x)
    {
        double result = 0;
        double prev = left;
        for (double i = left+accuracy; i <= x; i+=accuracy)
        {
            result += densityFunction((i + prev) / 2) * accuracy;
            prev = i;
        }
        return result;
    }

    public double GenerateRandomValue() => InverseDistributionFunction(rand.NextDouble());

    public double[] GenerateRandomValues(int n)
    {
        return new double[n].Select(x => GenerateRandomValue()).ToArray();
    }

    public double InverseDistributionFunction(double p)
    {
        double a = left, b = right;
        double x;
        while(Math.Abs(a - b) > accuracy)
        {
            x = (a + b) / 2;
            if (distributionFunction(x) >= p)
            {
                b = x;
            }
            else
            {
                a = x;
            }
        }

        return b;
    }

    public static Func<double, double> DensityOfSum(RandomValue x, RandomValue y)
    {
        return t => DensityOfSum(x, y, t);
    }

    public static double DensityOfSum(RandomValue x, RandomValue y, double t)
    {
        double result = 0;
        var left = x.left + y.left;
        var right = x.right + y.right;
        double prev = left;
        for (double i = left+accuracy; i < right + accuracy; i+=accuracy)
        {
            var ti = (i + prev) / 2;
            result += x.densityFunction(ti) * y.densityFunction(t - ti) * accuracy;
            prev = i;
        }
        return result;
    }
}