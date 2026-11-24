using System.Text.Json;
using Spectre.Console;
using NeoDEEX.ServiceModel.WebApi;
using NeoDEEX.ServiceModel.Biz;

namespace usetypeinfo;

//
// 이 데모 코드는 NeoDEEX 버전 beta.2 이상이 필요하다.
// (추가된 GetJsonSerializerOptions 메서드를 사용)
//
class Program
{
    // 커스텀 타입 직렬화를 위한 예제 클래스
    class CustomType(string name, DateTime birthDate, decimal salary)
    {
        public string Name { get; set; } = name;
        public DateTime BirthDate { get; set; } = birthDate;
        public decimal Salary { get; set; } = salary;
    }

    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Web API: Use Type Info Usage Sample...:[/]");
        AnsiConsole.WriteLine();

        FoxWebApiServerConfig.WriteIndented = true;

        SerializeDictionary(false);
        SerializeDictionary(true);
        SerializeCustomType(false);
        SerializeCustomType(true);
    }

    // Dictionary 를 UseTypeInfo 옵션과 함께 직렬화하는 예제
    static void SerializeDictionary(bool useTypeInfo)
    {
        AnsiConsole.MarkupLine($"[green]Serialize Dictionary with UseTypeInfo = [/][blue]{UseTypeInfoToString(useTypeInfo)}[/]");
        // Dictioanry 객체를 생성하고 초기화한다.
        JsonSerializerOptions options = typeof(Dictionary<string, object>).GetJsonSerializerOptions(useTypeInfo)
            ?? JsonSerializerOptions.Web;
        Dictionary<string, object> dict = new()
        {
            { "key1", 123.45M },
            { "key2", 123.45 },
            { "key3", 123.45F }
        };
        // Dictionary 객체를 직렬화하고 그 결과를 출력한다.
        string json = JsonSerializer.Serialize(dict, options);
        AnsiConsole.MarkupLine("[yellow]Serialized JSON:[/]");
        AnsiConsole.MarkupLine($"[gray]{json}[/]");
        AnsiConsole.WriteLine();
    }

    // 커스텀 타입을 UseTypeInfo 옵션과 함께 직렬화하는 예제
    static void SerializeCustomType(bool useTypeInfo, bool useJsonElement = false)
    {
        AnsiConsole.MarkupLine($"[green]Serialize CustomType with UseTypeInfo = [/][blue]{UseTypeInfoToString(useTypeInfo)}[/]");
        // FoxBizRequest 객체의 매개변수로 커스텀 타입 객체를 설정한다.
        FoxBizRequest request = new("BizLogic", "BizMethod") { SerializeClientInfo = false };
        JsonSerializerOptions options = request.GetType().GetJsonSerializerOptions(useTypeInfo)
            ?? JsonSerializerOptions.Web;
        CustomType? custom = new("Alice", new DateTime(1993, 1, 1), 3.3M);
        request.Parameters["obj"] = custom;
        // FoxBizRequest 객체를 직렬화하고 그 결과를 출력한다.
        string json = JsonSerializer.Serialize(request, options);
        AnsiConsole.MarkupLine("[yellow]Serialized FoxBizRequest JSON:[/]");
        AnsiConsole.MarkupLine($"[gray]{json}[/]");
        // 직렬화된 JSON을 다시 역직렬화하고 커스텀 타입 객체를 확인한다.
        FoxBizRequest? deserializedRequest = JsonSerializer.Deserialize<FoxBizRequest>(json, options);
        AnsiConsole.MarkupLine("[yellow]Deserialized CustomType:[/]");
        if (deserializedRequest != null)
        {
            if (useJsonElement && deserializedRequest.Parameters["obj"] is JsonElement element)
            {
                // UseTypeInfo가 false인 경우, 지원되지 않는 타입(커스텀 타입)에 대해 JsonElement를 통해
                // 역직렬화를 수행할 수도 있다.
                AnsiConsole.MarkupLine($"[cyan]Raw JSON Element: {element}[/]");
                custom =JsonSerializer.Deserialize<CustomType>(element, options);
            }
            else
            {
                custom = deserializedRequest.Parameters["obj"] as CustomType;
            }
            AnsiConsole.MarkupLine($"[gray]Name: {custom?.Name}, BirthDate: {custom?.BirthDate}, Salary: {custom?.Salary}[/]");
        }
        AnsiConsole.WriteLine();
    }

    // UseTypeInfo 옵션 값을 문자열(소문자)로 변환하는 헬퍼 메서드
    static string UseTypeInfoToString(bool useTypeInfo)
    {
        return useTypeInfo.ToString().ToLower();
    }
}
