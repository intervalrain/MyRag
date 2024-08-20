namespace MyRag.Util.VectorTools;

public class Vector
{
    public static void AssertSameLength(float[] a, float[] b)
    {
        if (a.Length != b.Length)
        {
            throw new ArgumentException("Vectors must be of same length");
        }
    }

    public static float CosineSimilarity(float[] a, float[] b)
    {
        return DotProduct(a, b) / (float)(Math.Sqrt(Magnitude(a) * Math.Sqrt(Magnitude(b))));
    }

    public static float DotProduct(float[] a, float[] b)
    {
        AssertSameLength(a, b);

        float dotProduct = 0.0f;

        Parallel.For(0, a.Length, i => dotProduct += a[i] * b[i]);

        return dotProduct;
    }

    public static float Magnitude(float[] a)
    {
        float magnitude = 0.0f;

        Parallel.For(0, a.Length, i => magnitude += (float)Math.Pow(a[i], 2));

        return magnitude;
    }
}

