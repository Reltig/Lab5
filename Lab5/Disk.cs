namespace Lab5;

public class Disk
{
    private List<double> d;
    public bool IsNotEmpty => d.Count != 0;

    public Disk(double[] d)
    {
        this.d = new List<double>(d);
    }

    public double Take(double k)
    {
        int i = (int)(k * d.Count);
        var res = d[i];
        d.RemoveAt(i);
        return res;
    }
}