namespace FLG.Cs.FLGMath {
    public class NormalDistribution {
        private float mu;       // mean
        private float sigma;    // standard deviation

        public NormalDistribution(float mean, float stdDev)
        {
            mu = mean;
            sigma = stdDev;
        }

        public float areaBetween(float a, float b)
        {
            return CDF(b) - CDF(a);
        }

        public float[] areasBetween(int buckets)
        {
            float min = mu - 3 * sigma;
            float max = mu + 3 * sigma;
            float total = max - min;
            float width = total / buckets;

            float[] result = new float[buckets];
            float a = 0;
            float b = width;
            float sum = 0;
            for (int i = 0; i < buckets; ++i)
            {
                float r = areaBetween(a, b) * 100f;
                result[i] = r;
                sum += r;
                a += width;
                b += width;
            }

            for (int i = 0; i < buckets; ++i)
            {
                result[i] = result[i] / sum;
            }

            return result;
        }

        private float PDF(float x) // Probability Density Function (normal distribution)
        {
            return (1f / (sigma * MathF.Sqrt(2 * MathF.PI))) * MathF.Exp(((x - mu) * (x - mu)) / (2 * sigma * sigma));
        }

        private float CDF(float x) // Cumulative Density Function (integral-ish of PDF)
        {
            return 0.5f * (1f + Erf((x - mu) / (sigma * MathF.Sqrt(2))));
        }

        private float Erf(float x) // Error function approximation
        {
            // Constants for approximation
            float a1 = 0.254829592f;
            float a2 = -0.284496736f;
            float a3 = 1.421413741f;
            float a4 = -1.453152027f;
            float a5 = 1.061405429f;
            float p = 0.3275911f;

            // Save the sign of x
            int sign = (x < 0f) ? -1 : 1;
            x = MathF.Abs(x);

            // A&S formula 7.1.26
            float t = 1f / (1f + p * x);
            float y = 1f - ((((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t) * MathF.Exp(-x * x);

            return sign * y;
        }
    }
}
