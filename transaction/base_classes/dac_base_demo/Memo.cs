namespace dac_base_demo;

//
// 메모 엔티티 정의
//
public class Memo
{
    public int Id { get; set; }
    public string Title { get; set; } = "제목 없음";
    public string? Content { get; set; }
}
