// See https://aka.ms/new-console-template for more information
using KeyProviderLib;
using NeoDEEX.Security;
using SimpleEncryptDecrypt;
using System.Text;

Console.WriteLine("Simple Encrypt/Decrpt Sample App.\n");

BasicCryptoSample();
EasyCryptoSample();
BasicCryptoSampleWithBase64();
EasyCryptoSampleWithBase64();
ObjectEncrytDecryptSample();
StreamEncryptDecryptSample();
CustomKeyProviderSample();

static void BasicCryptoSample()
{
    // 문자열을 암호화하는 예제
    string plainText = "Basic Crypto Sample";
    Console.WriteLine($"Plain text: {plainText}");
    // 문자열을 byte[] 로 전환하고 암호화 한다.
    byte[] plainData = Encoding.UTF8.GetBytes(plainText);
    byte[] cipherData = FoxCryptoHelper.Encrypt(plainData);
    // 암호화된 문자열을 복호화하는 예제
    byte[] decryptedData = FoxCryptoHelper.Decrypt(cipherData);
    // 복호화된 데이터를 다시 문자열로 바꾼다.
    string decryptedText = Encoding.UTF8.GetString(decryptedData);
    Console.WriteLine($"Decrypted text: {decryptedText}\n");
}

static void EasyCryptoSample()
{
    // 문자열을 매개변수로 사용하는 Enrypt 메서드를 사용하면
    // 문자열을 byte[] 로 전환한 후 암호화를 수행해준다.
    string plainText = "Easy Crypto Sample";
    Console.WriteLine($"Plain text: {plainText}");
    byte[] cipherData = FoxCryptoHelper.Encrypt(plainText);

    // DecryptToString 메서드는 복호화후 문자열로 변환하여 반환해 준다.
    string decryptedText = FoxCryptoHelper.DecryptToString(cipherData);
    Console.WriteLine($"Decrypted text: {decryptedText}\n");
}

static void BasicCryptoSampleWithBase64()
{
    string plainText = "Basic Crypto Sample using BASE64";
    Console.WriteLine($"Plain text: {plainText}");
    // 문자열을 암호화하여 Base64 문자열을 구한다.
    string encodedCipherText = GetEncryptedString(plainText);
    // 암호화된 Base64 문자열로부터 복호화를 하여 원본 문자열을 구한다.
    string decryptedText = GetDecryptedString(encodedCipherText);
    Console.WriteLine($"Decrypted text: {decryptedText}\n");
}

static string GetEncryptedString(string plainText)
{
    // 문자열을 암호화된 Base64 문자열로 바꾸기 위해서는
    // 문자열을 byte[] 로 전환 후 암호화 하고 다시 Base64 인코딩을 해야 한다.
    byte[] plainData = Encoding.UTF8.GetBytes(plainText);
    byte[] cipherData = FoxCryptoHelper.Encrypt(plainData);
    string encodedCipherText = Convert.ToBase64String(cipherData);
    return encodedCipherText;
}
    
static string GetDecryptedString(string encodedCipherText)
{ 
    // 암호화된 Base64 문자열을 복호화하기 위해서는
    // Base64 로 디코딩하여 byte[] 을 구하여 복호화를 수행해야 한다.
    // 복호화된 byte[] 로부터 원본 문자열로 디코딩을 추가로 작업한다.
    byte[] cipherData = Convert.FromBase64String(encodedCipherText);
    byte[] decryptedData = FoxCryptoHelper.Decrypt(cipherData);
    string decryptedText = Encoding.UTF8.GetString(decryptedData);
    return decryptedText;
}

static void EasyCryptoSampleWithBase64()
{
    string plainText = "Easy Crypto Sample using BASE64";
    Console.WriteLine($"Plain text: {plainText}");
    // EncryptToBase64 메서드는 암호화 및 Base64 인코딩을 수행해 준다.
    string encodedCipherText = FoxCryptoHelper.EncryptToBase64(plainText);
    // 문자열을 매개변수로 사용하는 DecryptToString 메서드는 Base64 디코딩, 복호화를 수행해 준다.
    string decryptedText = FoxCryptoHelper.DecryptToString(encodedCipherText);
    Console.WriteLine($"Decrypted text: {decryptedText}\n");
}

static void ObjectEncrytDecryptSample()
{
    SimplePoco obj = new SimplePoco() { Id = 1, Name = "Nobody", Password = "!23$" };
    Console.WriteLine($"Origianl object: {obj}");
    // 주어진 객체를 JSON 직열화하고 암호화 하여 반환한다.
    byte[] cipherData = FoxCryptoHelper.EncryptObject(obj);
    // 암호화된 JSON 객체를 복호화하고 역직렬화 하여 반환한다.
    SimplePoco? obj2 = FoxCryptoHelper.DecryptObject<SimplePoco>(cipherData);
    Console.WriteLine($"Decrypted object: {obj}\n");
}

static void StreamEncryptDecryptSample()
{
    // 스트림 암/복호화 메서드를 사용하여 파일을 암호화 혹은 복호화 한다.
    string plainFileName = "TargetFile.txt";
    string encryptedFileName = "EncryptedFile.bin";
    string decryptedFileName = "DecryptedFile.txt";
    Console.WriteLine($"Original file content: {File.ReadAllText(plainFileName)}");
    {
        // 파일 암호화
        Stream inputStream = File.OpenRead(plainFileName);
        Stream outputStream = File.Create(encryptedFileName);
        FoxCryptoHelper.Encrypt(inputStream, outputStream);
    }
    {
        // 파일 복호화
        Stream inputStream = File.OpenRead(encryptedFileName);
        Stream outputStream = File.Create(decryptedFileName);
        FoxCryptoHelper.Decrypt(inputStream, outputStream);
    }
    Console.WriteLine($"Decrypted file content: {File.ReadAllText(decryptedFileName)}\n");
}

static void CustomKeyProviderSample()
{
    // FoxCryptoKeyProvider 클래스로부터 파생하여 암호화 알고리즘과 키를 지정할 수 있다.
    var provider = new MyKeyProvider();
    FoxCryptoHelper.KeyProvider = provider;
    var algorithm = provider.GetSymmetricAlgorithm();
    Console.WriteLine($"Current key provider: {provider.GetType().FullName}");
    Console.WriteLine($"  Key size: {algorithm.KeySize}");

    string cipherText = FoxCryptoHelper.EncryptToBase64("Hello World!");
    Console.WriteLine($"Original Text: {FoxCryptoHelper.DecryptToString(cipherText)}");
    // 디폴트 키 프로바이더로 복원
    FoxCryptoHelper.ResetKeyProvider();
}