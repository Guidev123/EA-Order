
using MediatR;
using Orders.Application.Commands.Vouchers.Create;
using Orders.Application.Responses;

namespace Orders.API.Endpoints.Vouchers
{
    public class CreateVoucherEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => 
            app.MapPost("/", HandleAsync).Produces<Response<CreateVoucherResponse>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       CreateVoucherCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? TypedResults.Created($"/{command.Code}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
