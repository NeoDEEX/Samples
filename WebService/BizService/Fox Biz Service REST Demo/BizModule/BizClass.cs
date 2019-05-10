using TheOne.ServiceModel.Biz;
using TheOne.Transactions;

namespace BizModule
{
    // 예제 비즈 클래스
    // ClassId로 `데모비즈`를 사용합니다. ClassId를 명시하지 않은 경우,
    // 네임스페이스를 포함하는 클래스의 전체 이름(BizModule.BizClass)이
    // ClassId로 사용됩니다.
    [FoxBizClass("데모비즈")]
    public class BizClass : FoxBizBase
    {
        // 예제 비즈 메서드
        // MethodId를 명시하지 않았기 때문에 메서드 이름이 MethodId로 사용됩니다.
        [FoxBizMethod]
        public string Echo(string input)
        {
            return $"Hello Fox Biz Service: {input}";
        }
    }
}
