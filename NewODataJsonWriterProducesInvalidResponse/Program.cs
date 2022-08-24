using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Json;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddOData(options =>
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<Customer>("Customers").EntityType.HasKey(x => x.ID);
        var edmModel = modelBuilder.GetEdmModel();
        
        options.AddRouteComponents("default", edmModel, routeServices =>
        {
            routeServices.AddSingleton<ODataBatchHandler, DefaultODataBatchHandler>();
            routeServices.AddSingleton<IStreamBasedJsonWriterFactory>(_ => DefaultStreamBasedJsonWriterFactory.Default);
        });
    });

var app = builder.Build();

app.UseODataRouteDebug().UseODataBatching().UseRouting().UseEndpoints(routeBuilder => routeBuilder.MapControllers());

app.Run();

public class Customer
{
    public int ID { get; set; }
    public string? Name { get; set; }
}

[EnableQuery]
public class CustomersController : ODataController
{
    public IQueryable<Customer> Get()
    {
        return new[]
        {
            new Customer
            {
                ID = 1,
                Name = "John Smith",
            },
        }.AsQueryable();
    }
}