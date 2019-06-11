using System.ServiceModel.Channels;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Data;
using TheOne.ServiceModel.Web;

namespace SampleClientApp
{
    // REST API 클라이언트를 기본으로 사용하는 FoxDataClientFactory 구현
    class DataClientFactory : FoxDataClientFactory
    {
        public override IFoxDataClient Create(string addressName, string serviceUri, string bindingMapName = null)
        {
            return new FoxRestDataClient(addressName, serviceUri);
        }

        public override IFoxDataClient Create(string serviceAddress, Binding binding)
        {
            // 주소 맵(address map)을 사용하지 않는 서비스의 전체 주소를 설정하기 위해서는 ServiceUrl 속성을 사용한다.
            return new FoxRestDataClient() { ServiceUrl = serviceAddress };
        }

        public override IFoxDataClient Create(string serviceUri)
        {
            return new FoxRestDataClient(serviceUri);
        }
    }
}
