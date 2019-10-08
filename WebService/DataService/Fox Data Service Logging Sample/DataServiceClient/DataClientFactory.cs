using System.ServiceModel.Channels;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Data;
using TheOne.ServiceModel.Web;

namespace DataServiceClient
{
    // Data Service에 대한 클라이언트 팩터리 클래스
    // REST API를 기본으로 사용하도록 FoxRestDataClient 객체를 생성하여 반환한다.
    internal class DataClientFactory : FoxDataClientFactory
    {
        public override IFoxDataClient Create(string serviceUri)
        {
            return new FoxRestDataClient(serviceUri);
        }

        public override IFoxDataClient Create(string serviceAddress, Binding binding)
        {
            return new FoxRestDataClient() { ServiceUrl = serviceAddress };
        }

        public override IFoxDataClient Create(string addressName, string serviceUri, string bindingMapName = null)
        {
            return new FoxRestDataClient(addressName, serviceUri);
        }
    }
}