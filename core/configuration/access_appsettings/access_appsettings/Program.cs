using NeoDEEX.Configuration;
using System.Text;

Console.WriteLine("NeoDEEX5 Fox Configuration AppSettings Sample");

SimpleAccess();
ComplexAccess();
BindAccess();
ArrayAccess();
DictionaryAccess();
EnumKeyValue();

static void SimpleAccess()
{
    // 단순 액세스 예제
    Console.WriteLine("\nSimple AppSettings example...");

    string? setting1 = FoxConfigurationManager.AppSettings["setting1"];
    string? setting2 = FoxConfigurationManager.AppSettings["SETTING2"];  // 대소문자를 구별하지 않음
    bool setting3 = FoxConfigurationManager.AppSettings.GetValue<bool>("Setting3");
    int setting4 = FoxConfigurationManager.AppSettings.GetValue<int>("setting4");

    Console.WriteLine($"setting1 = {setting1}");
    Console.WriteLine($"setting2 = {setting2}");
    Console.WriteLine($"setting3 = {setting3}");
    Console.WriteLine($"setting4 = {setting4}");
}

static void ComplexAccess()
{
    // 키 경로를 사용하여 하위 값들 액세스
    Console.WriteLine("\nComplex AppSettings example...");

    string? subkey1 = FoxConfigurationManager.AppSettings["complex:subkey1"];
    bool subkey2 = FoxConfigurationManager.AppSettings.GetValue<bool>("complex:subkey2");
    string? element1 = FoxConfigurationManager.AppSettings["array:0"];
    string? element2 = FoxConfigurationManager.AppSettings["array:1"];

    Console.WriteLine($"subkey1 = {subkey1}");
    Console.WriteLine($"subkey2 = {subkey2}");
    Console.WriteLine($"element1 = {element1}");
    Console.WriteLine($"element2 = {element2}");
}

static void BindAccess()
{
    // 바인딩을 사용하여 닷넷 타입으로 매핑하여 액세스
    Console.WriteLine("\nBinding AppSettings example...");

    var complex = FoxConfigurationManager.AppSettings.Get<ComplexSetting>("complex");

    Console.WriteLine($"subkey1 = {complex?.Subkey1}");
    Console.WriteLine($"subkey2 = {complex?.Subkey2}");
    Console.WriteLine($"stringList = {DumpStringOfArrayElement(complex?.StringList.ToArray())}");
    Console.WriteLine($"subObject : name={complex?.SubObject.Name} value={complex?.SubObject.Value}");
}

static void ArrayAccess()
{
    // 배열 액세스
    Console.WriteLine("\nArray AppSettings example...");

    var array1 = FoxConfigurationManager.AppSettings.Get<string[]>("array");
    Console.WriteLine($"string[] array: {DumpStringOfArrayElement(array1)}");

    var list1 = FoxConfigurationManager.AppSettings.Get<List<string>>("array");
    Console.WriteLine($"List<string> list: {DumpStringOfArrayElement(list1?.ToArray())}");
}

static void DictionaryAccess()
{
    // Dictionary 액세스 예제
    Console.WriteLine("\nDictionary AppSettings example...");

    // 주) Dictionary<string, object> 타입을 명시하더라도 모두 문자열 값으로 바인딩 됨에 주의해야 한다.
    var dic = FoxConfigurationManager.AppSettings.Get<Dictionary<string, object>>("dic");
    if (dic == null)
    {
        return;
    }
    foreach(var pair in dic)
    {
        Console.WriteLine($"{pair.Key}: {pair.Value} ({pair.Value.GetType().Name})");
    }
}

static void EnumKeyValue()
{
    // 키값 열거 예제
    Console.WriteLine("Enumuerating AppSettings example...");

    foreach (var key in FoxConfigurationManager.AppSettings)
    {
        var value = FoxConfigurationManager.AppSettings[key];
        Console.WriteLine($"{key} = {value}");
    }
}

static string DumpStringOfArrayElement(Array? array)
{
    if (array == null)
    {
        return "(null)";
    }
    var sb = new StringBuilder();
    sb.Append("[ ");
    foreach (var item in array)
    {
        sb.Append(item.ToString()).Append(", ");
    }
    if (array.Length > 0)
    {
        sb.Remove(sb.Length - 2, 2);
    }
    sb.Append(" ]");
    return sb.ToString();
}

class ComplexSetting
{
    public string? Subkey1 { get; set; }
    public bool? Subkey2 { get; set; }
    public List<string> StringList { get; set; } = [];
    public NameValuePair SubObject { get; set; } = new();
}

class NameValuePair
{
    public string? Name { get; set; }
    public string? Value { get; set; }
}
