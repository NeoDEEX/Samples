using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi_app;

public static class JwtHelper
{
    // JWT 토큰 서명에 사용할 키
    // 주) 키는 외부의 안전한 저장소에 기록해야 하며, 이 예제와 같은 비밀키를 사용하는 것보다는
    //     개인키/공개키 방식을 사용하는 것이 안전합니다.
    private static readonly string SigningKey = "ThisIsTheKeyForSimpleTest12345678901234";

    // 발급자는 App을 파악할 수 있는 고유 문자열을 사용합니다.
    public static string Issuer { get; } = "https://jwt.neodeex.net";
    // NeoDEEX 토큰을 기록할 클레임 이름
    public static string FoxAuthStringClaimName { get; } = "fox_auth_string";
    public static SymmetricSecurityKey SecurityKey => new(Encoding.UTF8.GetBytes(SigningKey));

    // JWT 토큰을 생성하여 반환합니다.
    public static string CreateToken(string subject, string? audience = null, string? authString = null, double expires = 120)
    {
        SigningCredentials credentials = new(SecurityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            // UniqueName 을 명시해야 Identity.Name 속성에서 사용자 Id를 가져올 수 있습니다.
            new Claim(JwtRegisteredClaimNames.UniqueName, subject),
        ];
        if (authString != null)
        {
            claims.Add(new Claim(FoxAuthStringClaimName, authString));
        }

        var token = new JwtSecurityToken(
            Issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(expires),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // asp.net jwt bearer 인증을 사용하지 않고 직접 토큰을 검증하는데 사용합니다.
    public static SecurityToken VaildateToken(string token, string? audience = null)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKey,
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = audience != null,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);
        return validatedToken;
    }
}
