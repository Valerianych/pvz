using UnityEngine;

public static class OrderNumberGenerator
{
    private static readonly string[] Letters = { "A", "B", "C", "D", "E" };

    public static string Generate()
    {
        string letter = Letters[Random.Range(0, Letters.Length)];
        int number = Random.Range(100, 999);

        return letter + "-" + number;
    }
}