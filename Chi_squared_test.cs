using System;
using System.Web.UI.DataVisualization.Charting;
//using alglib;

namespace Random_value
{
    public class Chi_squared_test
    {
        StatisticFormula SF;
        public double get_density(double x, double r) // r == k - 1 -- amount of degress of liberty
        {
            return (Math.Pow(2, -r/2) / SF.GammaFunction(r / 2)) * Math.Pow(x, (r/2) - 1) * Math.Exp(-x/2);
        }

        public void distribute_chi()
        {
            double res = 0.0;
            for (int k = 1; k < 1000; ++k)
            {
                //a = -inf
                //b = +inf
                //res += (get_density(a + ((b - a) * (k - 1) / 1000)) +
                //    get_density(a + ((b - a) * k / 1000))) * (b - a) / 2000;
            }
        }
    }
}
