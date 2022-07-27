# 동적 쿼리 예제

이 예제는 Fox Query 에서 제공하는 매크로 기능을 사용하여 동적으로 쿼리를 작성하는 예제 코드 입니다. Fox Query 매크로에 대한 상세한 내용은 다음 문서를 참고 하십시요.

* [Fox Query 매크로](https://github.com/NeoDEEX/manual/blob/master/data/foxquery/dynamic_query.md)

```xml
<statement id="GetData">
    <text>
        SELECT ProductID, ProductName, CategoryID FROM Products
        $$WHERE()$$
    </text>
    <macros>
        <macro name="WHERE">
        <![CDATA[
            var categoryId = env.Args.CategoryId;
            env.WriteLog($"categoryId = {categoryId}");
            if (DBNull.Value.Equals(categoryId)) {
                return null;
            }
            else {
                env.Params.Add("CategoryId", "Int");
                return "WHERE CategoryId = #CategoryId#";
            }
        ]]>
        </macro>
    </macros>
</statement>
```

---
