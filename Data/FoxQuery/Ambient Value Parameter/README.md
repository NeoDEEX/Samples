# Ambient Value Parameter Sample

Fox Query의 [환경 값 매개변수(Ambient Value Paramter)](https://github.com/NeoDEEX/manual/tree/master/data/foxquery/ambient.md)에 대한 예제 코드 입니다.

이 예제 코드는 다음과 같은 데이터베이스 테이블을 수정하는데 필요한 환경 값 매개변수 사용 방법을 보여 줍니다.

```sql
create table Product (
   Id                   int             identity,
   ProductName          nvarchar(50)    not null,
   SupplierId           int             not null,
   UnitPrice            decimal(12,2)   null default 0,
   Package              nvarchar(30)    null,
   IsDiscontinued       bit             not null default 0,
   ModifiedBy           nvarchar(32)    null,
   ModifiedAt           datetime        null default sysdatetime()
   constraint PK_PRODUCT primary key (Id)
)
```

환경 값 매개변수를 사용하지 않는 경우, `ModifiedBy` 컬럼 값 업데이트를 위해 현재 사용자의 아이디를 매개변수 등의 방법으로 서버로 전송 해야 합니다. `ModifiedBy` 컬럼은 앱의 핵심 비즈니스 기능이 아닌 감사 및 로깅과 관련된 데이터이지만 **클라이언트** 개발자가 이 컬럼에 관여된 코드를 신경쓰고 코드를 작성해야 한다는 점은 개발/유지/보수에 부정적인 요소가 될 수 있습니다.

환경 값 매개변수는 Fox Query의 `<parameter>` 요소의 `ambient` 속성 값이 `true` 인 경우 `property` 속성이 지시하는 환경 값을 매개변수의 값을 사용하는 기능입니다. 다음 Fox Query 예제에서 `ModifiedBy` 매개변수가 환경 값 매개변수 입니다. 이 Fox Query를 사용하는 경우, `ModifiedBy` 매개변수의 값은 이 쿼리를 수행하는 사용자의 아이디 값으로 자동으로 설정됩니다.

```xml
<statement id="UpdateProduct">
  <text>
    UPDATE Product
    SET ProductName = @ProductName, SupplierId = @SupplierId, UnitPrice = @UnitPrice,
    Package = @Package, ModifiedBy = @ModifiedBy, ModifiedAt = sysdatetime()
    WHERE Id = @Id
  </text>
  <parameters>
    <parameter name="Id" dbType="int"/>
    <parameter name="ProductName" dbType="nvarchar" size="50" />
    <parameter name="SupplierId" dbType="int"/>
    <parameter name="UnitPrice" dbType="decimal" size="12" precision="2" />
    <parameter name="Package" dbType="nvarchar" size="30" />
    <parameter name="ModifiedBy" property="UserInfo.UserId" dbType="nvarchar" size="32" ambient="true"/>
  </parameters>
</statement>
```

환경 값 매개변수는 [Fox Data Service](/webservice/dataservice/README.md)의 `SaveDataTable` 메서드를 사용하여 간단한 데이터 변경 프로그램을 작성할 때 유용하게 사용될 수 있습니다. 이 예제는 이러한 시나리오를 예제로서 보여줍니다. 환경 값 매개변수의 상세한 설명과 사용 시나리오에 대해서는 [관련 문서](https://github.com/NeoDEEX/manual/tree/master/data/foxquery/ambient.md)를 참고 하십시오.

---
