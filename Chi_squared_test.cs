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

        public double distribute_chi(double R0, int r)
        {
            //double res = 0.0;
            //for (int k = 1; k < 1000; ++k)
            //{
            //    res += (Math.Pow(2, -(k - 1) / 2) / SF.GammaFunction((k - 1) / 2)) * Math.Pow(R0 * (k - 1) / 1000, ((k - 1) / 2) - 1) * Math.Exp(-R0 * (k - 1) / 1000 / 2)
            //    res += (this.get_density(, k - 1)) +
            //        this.get_density(R0 * k / 1000, k - 1) * (R0 / 2000);
            //}
            //return res;
            return alglib.incompletegamma(R0 * Math.Pow(2, 0.5*r), 0.5 * r);
        }
    }
}
