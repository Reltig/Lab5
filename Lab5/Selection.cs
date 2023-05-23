namespace Lab5;

public class Selection
{
    private double[] data;

    private double _expectedValue, _dispertion, _standardDeviation, _asymmetry, _kurtosis;

    public double ExpectedValue => _expectedValue;

    public double Dispertion => _dispertion;
    
    public double StandardDeviation => _standardDeviation;
    public double Asymmetry => _asymmetry;
    public double Kurtosis => _kurtosis;

    public double CoefficientOfVariation => StandardDeviation / ExpectedValue;

    public Selection(double[] data)
    {
        this.data = data;
        _expectedValue = data.Sum() / data.Length;
        _dispertion = data.Select(x => (x - _expectedValue) * (x - _expectedValue)).Sum() / (data.Length - 1);
        _standardDeviation = Math.Sqrt(_dispertion);
        _asymmetry = data.Select(x => Math.Pow(x - _expectedValue, 3)).Sum()/(data.Length * Math.Pow(StandardDeviation, 3));
        _kurtosis = data.Select(x => Math.Pow(x - _expectedValue, 4)).Sum()/(data.Length * Math.Pow(StandardDeviation, 4));
    }
    
    
}