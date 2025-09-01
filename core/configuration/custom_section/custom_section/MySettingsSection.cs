using Microsoft.Extensions.Configuration;
using NeoDEEX.Configuration;

namespace CustomSection;

internal class MySettings2Section : FoxConfigurationSection
{
    private MySettings2? _settings;

    public MySettings2Section()
        : base("mySettings")
    {
    }

    public MySettings2? Settings
    {
        get
        {
            lock (SyncObject)
            {
                if (base.Initialized == false)
                {
                    base.Initialize();
                }
            }
            return _settings;
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        // OnInitialize 메서드 내에서는 ConfigSection 속성에 접근할 수 없다.
        // 초기화 전이므로 ConfigSection 속성은 무한 루프를 발생한다.
        // 따라서 OnInitialize 메서드 내에서는 RawConfigSection 속성을 사용해야 한다.
        if (base.RawConfigSection != null)
        {
            _settings = base.RawConfigSection.Get<MySettings2>();
            var subSection = base.RawConfigSection.GetSection("stringList");
            var list = subSection.Get<List<string>>();
            if (list != null)
            {
                _settings?.SetList(list);
            }
        }
            else
            {
                _settings = new MySettings2();
            }
    }
}
