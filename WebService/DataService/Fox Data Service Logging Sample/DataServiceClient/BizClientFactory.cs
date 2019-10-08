using System.ServiceModel.Channels;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Web;

namespace DataServiceClient
{
    // Biz Service에 대한 클라이언트 팩터리 클래스
    // REST API를 기본으로 사용하도록 FoxRestBizlient 객체를 생성하여 반환한다.
    internal class BizClientFactory : FoxBizClientFactory
    {
        public override IFoxBizClient Create(string serviceUri)
        {
            return new FoxRestBizClient(serviceUri);
        }

        public override IFoxBizClient Create(string serviceAddress, Binding binding)
        {
            return new FoxRestBizClient() { ServiceUrl = serviceAddress };
        }

        public override IFoxBizClient Create(string addressName, string serviceUri, string bindingMapName = null)
        {
            return new FoxRestBizClient(addressName, serviceUri);
        }
    }
}