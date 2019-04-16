# Progress without FoxForm (IFoxSupportProgress)

`FoxForm`에서 파생되지 않은 폼(Form)에서 NeoDEEX의 프로그래스를 표시하는 방법을 보여주는 예제 입니다.

## IFoxSupportProgress 인터페이스

NeoDEEX의 기본 프로그래스는 `FoxAsyncProxy`(혹은 `FoxTaskProxy`) 클래스나 `FoxProgressScope` 클래스를 사용할 때 표시됩니다. 이 때, 이들 클래스는 `IFoxSupportProgress` 인터페이스를 구현하는 폼(혹은 컨트롤)을 컨트롤 트리 상에서 찾게 됩니다. `FoxForm` 클래스는 `IFoxSupportProgress` 인터페이스를 이미 구현하고 있기 때문에 자동으로 프로그래스가 나타납니다.

따라서 `FoxForm`에서 파생되지 않은 폼에서 NeoDEEX의 프로그래스를 표시하기 위해서는 폼(혹은 그 폼의 베이스 클래스)에서 `IFoxSupportProgress` 인터페이스를 구현하면 됩니다.

```cs
public partial class SimpleShowProgressForm : Form, IFoxSupportProgress
{
    public SimpleShowProgressForm()
    {
        InitializeComponent();
    }

    private FoxProgressDialog _progressDialog;

    // IFoxSupportProgress 인터페이스 구현
    public IFoxProgressDialog GetProgressDialog()
    {
        if (_progressDialog == null)
        {
            _progressDialog = new FoxProgressDialog();
            this.Controls.Add(_progressDialog);
        }
        return _progressDialog;
    }

    ... 생략 ...
}
```

`IFoxSupportProgress` 인터페이스는 `GetProgressDialog` 메서드 하나만을 가지고 있으며 이 메서드는 `IFoxProgressDialog` 인터페이스를 구현하는 객체를 반환합니다. `IFoxProgressDialog` 인터페이스는 프로그래스의 표시 여부를 제어하며 중복 표시되는 것을 막기 위한 메서드들을 포함합니다. 이 인터페이스의 구현은 이 예제의 범위에 포함되지 않습니다. 대신 이 인터페이스를 구현하는 `FoxProgressDialog` 클래스를 사용할 수 있습니다.

`FoxProgressDialog` 클래스는 `UserControl`을 사용하여 프로그래스를 표시하는 NeoDEEX의 기본 프로그래스 구현입니다. `FoxProgressDialog` 객체를 생성하고 표시하고자 하는 폼(컨트롤)에 추가한 후, `IFoxSupportProgress` 인터페이스의 `GetProgressDialog` 메서드에서 반환하도록 합니다.

`IFoxSupportProgress` 인터페이스의 구체적인 예제는 SimpleShowProgressForm.cs 파일을 참고하십시오.
