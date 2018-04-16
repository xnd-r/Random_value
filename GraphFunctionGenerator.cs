using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Random_value
{
    /// <summary>
    /// </summary>
    public class GraphFunctionGenerator
    {
        private double xMin = 0d;
        private double xMax = 5d;
        public double Interval
        {
            get { return xMax - xMin; }
        }
        private float f(double x, double l)
        {   
            if (x < 0)
                return 1;
            return (float)(-l * Math.Exp(-l * x));
            //return (float)-(1 - Math.Exp(-l * x));
        }

        private PointF[] listOfPoints;
        public PointF[] ListOfPoints
        {
            get { return listOfPoints; }
            private set { listOfPoints = value; }
        }
        /// <summary>
        /// Creating of point-gen.
        /// </summary>
        /// <param name="numberOfPoints">dim of grid.</param>
        public GraphFunctionGenerator(int numberOfPoints, double l)
        {
            listOfPoints = new PointF[numberOfPoints];
            // X-step
            double StepByX = Interval / numberOfPoints;
            double x;
            for (int i = 0; i < numberOfPoints; i++)
            {
                x = StepByX * i + xMin;
                listOfPoints[i] = new PointF((float)x, f(x, l));
            }
        }
    }

    //public class normal_generation
    //{


    //    static void TestErf()
    //    {
    //        // Select a few input values
    //        double[] x =
    //        {
    //    -3,
    //    -1,
    //    0.0,
    //    0.5,
    //    2.1
    //    };

    //        // Output computed by Mathematica
    //        // y = Erf[x]
    //        double[] y =
    //        {
    //    -0.999977909503,
    //    -0.842700792950,
    //    0.0,
    //    0.520499877813,
    //    0.997020533344
    //    };

    //        double maxError = 0.0;
    //        for (int i = 0; i < x.Length; ++i)
    //        {
    //            double error = Math.Abs(y[i] - Erf(x[i]));
    //            if (error > maxError)
    //                maxError = error;
    //        }

    //        Console.WriteLine("Maximum error: {0}", maxError);
    //    }
    //}
}
