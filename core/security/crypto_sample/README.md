# 암호화 복호화 예제

NeoDEEX 가 제공하는 암호화/복호화 유틸리티 클래스인 `FoxCrytpoHelper` 클래스에 대한 예제 코드 입니다.

`Program.cs` 파일 에는 다양한 상황에서 사용할 수 있는 `FoxCryptoHelper` 클래스의 메서드들의 예제 코드를 담고 있습니다. `FoxCryptoHelper` 클래스가 제공하는 메서드들은 간단한 호출로 암호화된 결과를 손쉽게 얻을 수 있도록 구성되어 있습니다.

예를 들어 문자열을 암호화 하고 이를 네트워크로 전송하기 위해 Base64 인코딩을 해야 한다면 `EncryptToBase64` 메서드를 호출하면 됩니다. 암호화되고 Base64 로 인코딩된 데이터를 수신한 상대편은 원본 문자열로 복호화하기 위해서 단순히 `DecryptToString` 메서드를 호출하면 됩니다.

```cs
string plainText = "Easy Crypto Sample using BASE64";
Console.WriteLine($"Plain text: {plainText}");
string encodedCipherText = FoxCryptoHelper.EncryptToBase64(plainText);

string decryptedText = FoxCryptoHelper.DecryptToString(encodedCipherText);
Console.WriteLine($"Decrypted text: {decryptedText}\n");
```

암호화/복호화에 대한 상세한 내용은 [암호화 및 복호화 문서](https://neodeex.github.io/doc/core/security/crypto/)를 참고 하십시요.

---
