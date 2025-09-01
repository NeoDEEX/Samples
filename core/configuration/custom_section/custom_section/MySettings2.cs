using System.Text;

namespace CustomSection;

// 구성 설정 바인딩용 타입
internal class MySettings2
{
    private List<string> _stringList = [];

    public string? StringProp { get; set; }
    public bool? BoolProp { get; set; }
    // 수정을 막기 위해 ReadOnly 리스트로 제공한다.
    // 이제 이 속성 때문에 바인딩을 수행할 수 없기 때문에 커스텀 섹션 핸들러를 작성해야 한다.
    public IReadOnlyList<string> StringList { get => _stringList; }
    public SubType? SubObject { get; set; }

    private static string? GetString<T>(T type)
    {
        if (type == null) return "(null)";
        return type.ToString();
    }

    internal void SetList(List<string> list)
    {
        _stringList = list;
    }

    public override string ToString()
    {
        var sb = new StringBuilder(64);
        sb.Append($"StringProp = ").AppendLine(GetString(this.StringProp));
        sb.Append($"BoolProp = ").AppendLine(GetString(this.BoolProp));
        sb.Append($"StringList = [");
        foreach(var str in this._stringList)
        {
            sb.Append('"').Append(str).Append("\", ");
        }
        if (_stringList.Count > 0)
        {
            sb.Remove(sb.Length - 2, 2);
        }
        sb.AppendLine("]");
        sb.Append($"SubObject = ").Append(GetString(this.SubObject));
        return sb.ToString();
    }
}
