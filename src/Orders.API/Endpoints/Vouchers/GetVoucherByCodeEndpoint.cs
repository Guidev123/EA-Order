
using MediatR;
using Orders.Application.Queries.Vouchers.GetByCode;
using Orders.Application.Responses;

namespace Orders.API.Endpoints.Vouchers
{
    public class GetVoucherByCodeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/{code}", HandleAsync).Produces<Response<GetVoucherByCodeResponse>>();

        private static async Task<IResult> HandleAsync(IMediator mediator, string code)
        {
            var result = await mediator.Send(new GetVoucherByCodeQuery(code));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
