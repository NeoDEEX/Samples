namespace webapi_app;

//
// 사용자 인증 정보를 요청하는데 사용하는 클래스입니다.
//
public class RequestUserCredential
{
    public string? UserId { get; set; }
    public string? Password { get; set; }
}
