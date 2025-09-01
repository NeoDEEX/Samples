using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSection;

// 구성 설정 바인딩용 타입
internal class MySettings
{

    public string? StringProp { get; set; }
    public bool? BoolProp { get; set; }
    public List<string> StringList { get; set; } = [];
    public SubType? SubObject { get; set; }

    private static string? GetString<T>(T type)
    {
        if (type == null) return "(null)";
        return type.ToString();
    }

    public override string ToString()
    {
        var sb = new StringBuilder(64);
        sb.Append($"StringProp = ").AppendLine(GetString(this.StringProp));
        sb.Append($"BoolProp = ").AppendLine(GetString(this.BoolProp));
        sb.Append($"StringList = [");
        foreach(var str in this.StringList)
        {
            sb.Append('"').Append(str).Append("\", ");
        }
        if (this.StringList.Count > 0)
        {
            sb.Remove(sb.Length - 2, 2);
        }
        sb.AppendLine("]");
        sb.Append($"SubObject = ").Append(GetString(this.SubObject));
        return sb.ToString();
    }
}

// 바인딩용 하위 타입
internal class SubType
{
    public string? Name { get; set;}
    public string? Value { get; set; } = "DefaultValue";

    public override string ToString()
    {
        var sb = new StringBuilder(32);
        sb.Append("{ Name: ")
            .Append(this.Name ?? "(null)")
            .Append(", Value: ")
            .Append(this.Value ?? "(null)")
            .Append(" }");
        return sb.ToString();
    }
}
