# Collection Parameter Sample

Fox Biz Service의 비즈 메서드는 여러 개의 `IDictionary<string, object>` 매개변수를 갖을 수 있습니다. 이와 관련된 예제 코드 입니다.

## Fox Biz Method 매개변수 매핑

클라이언트는 Fox Biz Service를 호출할 때 `FoxBizRequest` 객체를 사용하여 호출하고자 하는 비즈 클래스와 비즈 메서드, 그리고 매개변수를 전달하게 됩니다. 이 때 `FozBizRequest` 객체의 `Parameters` 컬렉션에 포함된 매개변수들이 비즈 메서드의 매개변수에 매핑되어 값이 전달되게 됩니다. 이 매핑 규칙은 다음과 같습니다.

* 비즈 메서드의 매개변수가 `FoxBizRequest` 타입의 매개변수 1개만 사용하는 경우, 클라이언트가 전송하는 `FoxBizRequest` 객체가 그대로 비즈 메서드에 전달됩니다.

    ```cs
    // FoxBizRequest 객체가 그대로 req 매개변수로 전달됨
    [FoxBizMethod]
    public string SimpleTestMethod1(FoxBizRequest request)
    {
        ......
    }
    ```

* 비즈 메서드의 매개변수가 `IDictionary<string, object>` 혹은 `FoxServiceParameterCollection` 타입의 매개변수 1개만을 사용하는 경우 클라이언트가 전송하는 `FoxBizRequest` 객체의 `Parameters` 컬렉션이 비즈 메서드로 전달됩니다.

    ```cs
    // FoxBizRequest.Parameters 컬렉션이 dic 매개변수로 전달됨
    [FoxBizMethod]
    public void SimpleTestMethod3(IDictionary<string, object> dic)
    {
        ......
    }
    ```

* 이 이외의 경우, 비즈 메서드의 매개변수는 클라이언트가 전송하는 `FoxBizRequest` 객체의 `Parameters` 컬렉션에서 매개변수와 동일한 이름을 갖는 매개변수가 전달됩니다.

    ```cs
    // FoxBizRequest.Parameters 컬렉션에서 s, dic 키를 찾아서 매개변수 매핑
    [FoxBizMethod]
    public void ComplexTestMethod1(string s, IDictionary<string, object> dic)
    {
        ......
    }
    ```

    > 주) 4.5.4 버전 이전에는 이 상황에서 매개변수 타입으로 `IDictionary<string, object>` 타입이 사용될 수 없습니다. (Serialization 예외 발생)

---
