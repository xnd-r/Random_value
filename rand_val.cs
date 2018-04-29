using System;

namespace Random_value
{
    public class rand_val
    {
        double lambda;
        int N; // amount of devices
        int M;
        
        // amount of experiments
        public rand_val(double _l, int _n, int _m)
        {
            lambda = _l;
            N = _n;
            M = _m;
        }

        public double get_device_time(double l, Random rand)
        {
            double y = rand.NextDouble();
            return (-(Math.Log(1-y)) /l);
        }

        public double single_experiment(int n, double l, Random rand)
        {
            double random_value = 0d;
            for (int i = 0; i < n; ++i)
            {
                random_value += get_device_time(l,rand);
            }
            return random_value;
        }

    }
}
