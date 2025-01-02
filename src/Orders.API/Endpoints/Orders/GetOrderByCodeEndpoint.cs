
using MediatR;
using Orders.Application.Queries.Orders.GetByCode;
using Orders.Application.Responses;

namespace Orders.API.Endpoints.Orders
{
    public class GetOrderByCodeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{code}", HandleAsync).Produces<Response<GetOrderByCodeResponse>>();

        private static async Task<IResult> HandleAsync(IMediator mediator, string code)
        {
            var result = await mediator.Send(new GetOrderByCodeQuery(code));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
