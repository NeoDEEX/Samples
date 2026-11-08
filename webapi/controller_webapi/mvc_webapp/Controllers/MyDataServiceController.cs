using Microsoft.AspNetCore.Mvc;
using NeoDEEX.Diagnostics;
using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.Services.Data;
using NeoDEEX.ServiceModel.WebApi;
using NeoDEEX.ServiceModel.WebApi.Controllers;
using NeoDEEX.ServiceModel.WebApi.Filters;
using System.Text;

namespace mvc_webapp.Controllers;

[Route("/api/data")]
[FoxAuthorize(false)]
[FoxServiceResult(UseTypeInfo = false)]
public class MyDataServiceController : FoxDataServiceControllerBase
{
    private readonly IFoxLog Log;

    public MyDataServiceController()
    {
        this.Log = FoxLogManager.GetLogger<MyDataServiceController>();
        this.Log.Verbose("MyDataServiceController is activated.");
    }

    [HttpGet("products/{id}")]
    [FoxServiceResult(UseTypeInfo = true)]
    public FoxDataResponse Proudcts(string id)
    {
        this.Log.VerboseFormat("Proudcts action invoked.");
        FoxDataRequest request = new("products.get");
        request.Parameters.Add("product_id", id);
        using (this.SetUserInfo())
        {
            IFoxDataService service = this.CreateDataService();
            return service.ExecuteDataSet(request);
        }
    }

    [HttpPost("upload")]
    public FoxDataResponse Upload([FoxFromBody] FoxDataRequest request, IFormFile file)
    {
        this.Log.Verbose("Upload action invoked.");

        using Stream stream = file.OpenReadStream();
        StreamReader reader = new(stream, Encoding.UTF8);
        string content = reader.ReadToEnd();
        this.Log.VerboseFormat("File content: {0}", content);
        
        using (this.SetUserInfo())
        {
            IFoxDataService service = this.CreateDataService();
            return service.ExecuteDataSet(request);
        }
    }
}
