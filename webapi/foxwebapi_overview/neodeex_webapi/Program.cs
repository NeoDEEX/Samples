using commlib;
using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.ServiceModel.Services.Data;
using NeoDEEX.ServiceModel.WebApi;
using System.Data;

namespace neodeex_webapi;

public class Program
{
    public static void Main(string[] args)
    {
        // 테스트 환경 구성
        Utils.SetupTest<Program>();

        //
        // ASP.NET Web API 초기화 코드
        //
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // MVC 컨트롤러와 FoxWebApi 컨트롤러를 등록.
        builder.Services.AddControllers()
            .AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service 모듈 구성
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        // 다음 코드는 Fox Web API 를 사용하지 않고, 직접 FoxDataService 를 호출하는 예시입니다.
        // FoxDataService를 직접 호출하는 것 보다는 DispatchDataService 와 같은 디스패치 API 를 사용하는 것을 권장합니다.
        app.MapGet("/orders", () =>
        {
            var request = new FoxDataRequest("orders.get_all_orders");
            using var service = new FoxDataService();
            var response = service.ExecuteDataSet(request);
            var orders = new List<Order>();
            foreach(DataRow row in response.DataSet.Tables[0].Rows)
            {
                var order = GetOrderFromDataRow(row);
                orders.Add(order);
            }
            return orders;
        });

        // 다음 코드는 Fox Web API 를 사용하지 않고, 직접 FoxDataService 를 호출하는 예시입니다.
        // FoxDataService를 직접 호출하는 것 보다는 DispatchDataService 와 같은 디스패치 API 를 사용하는 것을 권장합니다.
        app.MapGet("/orders/{id}", (int id) =>
        {
            var request = new FoxDataRequest("orders.get_order_by_id");
            request.Parameters.Add("order_id", id);
            using var service = new FoxDataService();
            var response = service.ExecuteDataSet(request);
            if (response.DataSet.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            var order = GetOrderFromDataRow(response.DataSet.Tables[0].Rows[0]);
            return order;
        });

        // 다음 코드는 Fox Web API 를 사용하지 않고, 직접 FoxBizService 를 호출하는 예시입니다.
        // FoxBizService를 직접 호출하는 것 보다는 DispatchBizService 와 같은 디스패치 API 를 사용하는 것을 권장합니다.
        app.MapPost("/orders/new", (OrderInfo orderDTO) =>
        {
            var request = new FoxBizRequest("OrdersBiz", "InsertOrder");
            request.Parameters.Add("order", orderDTO.order);
            request.Parameters.Add("details", orderDTO.details);
            using var service = new FoxBizService();
            var response = service.Execute(request);
            return response;
        });

        app.Run();
    }

    private static Order GetOrderFromDataRow(DataRow row)
    {
        return new Order()
        {
            Order_Id = Convert.ToInt32(row["Order_Id"]),
            Customer_Id = row["Customer_Id"] == DBNull.Value ? string.Empty : row["Customer_Id"].ToString()!,
            Employee_Id = row["Employee_Id"] == DBNull.Value ? null : row["Employee_Id"].ToString(),
            Order_Date = row["Order_Date"] == DBNull.Value ? null : Convert.ToDateTime(row["Order_Date"]),
            Shipped_Date = row["Shipped_Date"] == DBNull.Value ? null : Convert.ToDateTime(row["Shipped_Date"]),
            Ship_Address = row["Ship_Address"] == DBNull.Value ? null : row["Ship_Address"].ToString()
        };
    }
}

internal class OrderInfo
{
    public Order? order { get; set; }
    public OrderDetail[]? details { get; set; }
}
