namespace Com.RePower.Ocv.Model.Extensions
{
    public static class DoubleExtensions
    {
        //两个Double数相加

        public static double? Add(this double? v1, double? v2)
        {
            if (v1 == null || v2 == null) return null;

            decimal b1 = new decimal(v1 ?? 0);

            decimal b2 = new decimal(v2 ?? 0);

            return decimal.ToDouble(decimal.Add(b1, b2));
        }

        //两个Double数相减

        public static double? Sub(this double? v1, double? v2)
        {
            if (v1 == null || v2 == null) return null;

            decimal b1 = new decimal(v1 ?? 0);

            decimal b2 = new decimal(v2 ?? 0);

            return decimal.ToDouble(decimal.Subtract(b1, b2));
        }

        // 两个Double数相乘

        public static double? Mul(this double? v1, double? v2)
        {
            if (v1 == null || v2 == null) return null;

            decimal b1 = new decimal(v1 ?? 0);

            decimal b2 = new decimal(v2 ?? 0);

            return decimal.ToDouble(decimal.Multiply(b1, b2));
        }

        //两个Double数相除

        public static double? Div(this double? v1, double? v2)
        {
            if (v1 == null || v2 == null || v2 == 0) return null;

            decimal b1 = new decimal(v1 ?? 0);

            decimal b2 = new decimal(v2 ?? 0);

            return decimal.ToDouble(decimal.Divide(b1, b2));
        }
    }
}