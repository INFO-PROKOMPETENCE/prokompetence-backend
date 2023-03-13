using System.Security.Cryptography;
using System.Text;

namespace Prokompetence.Common.Security;

public static class CryptographyHelper
{
    private static readonly Random Random = new();

    public static byte[] GenerateRandomBytes(int length)
    {
        var result = new byte[length];
        Random.NextBytes(result);
        return result;
    }

    public static byte[] GenerateMd5Hash(string source, byte[] salt)
        => GenerateMd5Hash(Encoding.UTF8.GetBytes(source), salt);

    private static byte[] GenerateMd5Hash(byte[] source, byte[] salt)
    {
        var sourceHash = MD5.HashData(source);
        var sourceWithSalt = sourceHash.Concat(salt).ToArray();
        return MD5.HashData(sourceWithSalt);
    }
}