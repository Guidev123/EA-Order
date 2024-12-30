using Orders.API.Endpoints.Orders;
using Orders.API.Endpoints.Vouchers;

namespace Orders.API.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("api/v1/orders")
                .WithTags("Orders")
                .MapEndpoint<CreateOrderEndpoint>();

            endpoints.MapGroup("api/v1/vouchers")
                .WithTags("Vouchers")
                .RequireAuthorization()
                .MapEndpoint<CreateVoucherEndpoint>()
                .MapEndpoint<GetVoucherByCodeEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
