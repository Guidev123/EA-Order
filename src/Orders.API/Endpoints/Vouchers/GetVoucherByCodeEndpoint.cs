
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Queries.Vouchers.GetVoucherByCode;
using Orders.Application.Responses;

namespace Orders.API.Endpoints.Vouchers
{
    public class GetVoucherByCodeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/{code}", HandleAsync).Produces<Response<GetVoucherByCodeResponse>>();

        private static async Task<IResult> HandleAsync([FromServices] IMediator mediator, string code)
        {
            var result = await mediator.Send(new GetVoucherByCodeQuery(code));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
