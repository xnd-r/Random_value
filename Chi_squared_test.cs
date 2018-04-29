using System;
using System.Web.UI.DataVisualization.Charting;

namespace Random_value
{
    public class Chi_squared_test
    {
        StatisticFormula SF;
        public double get_density(double x, double r) // r == k - 1 -- amount of degress of liberty
        {
            return (Math.Pow(2, -r/2) / SF.GammaFunction(r / 2)) * Math.Pow(x, (r/2) - 1) * Math.Exp(-x/2);
        }
    }
}
