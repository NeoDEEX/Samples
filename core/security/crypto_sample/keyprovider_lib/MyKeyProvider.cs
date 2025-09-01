using NeoDEEX.Security;
using System.Security.Cryptography;

namespace KeyProviderLib;

//
// TripleDES 암호화 알고리즘(및 암호화 키)를 사용하는 커스텀 키 프로바이더 구현
//
public class MyKeyProvider : FoxCryptoKeyProvider
{
    private readonly byte[] _key;
    private readonly byte[] _iv;
    private readonly TripleDES _algorithm;

    public MyKeyProvider()
    {
        byte[] salt = [0, 1, 2, 3, 4, 5, 6, 7];
        Rfc2898DeriveBytes derivedBytes = new(
            "password",
            salt,
            100_000, // 충분히 큰 반복 횟수
            HashAlgorithmName.SHA256
        );
        _key = derivedBytes.GetBytes(24);
        _iv = derivedBytes.GetBytes(8);
        _algorithm = TripleDES.Create();
    }

    public override SymmetricAlgorithm GetSymmetricAlgorithm() => _algorithm;
    public override byte[] GetSymmetricKey() => _key;
    public override byte[] GetSymmetricIV() => _iv;
}
