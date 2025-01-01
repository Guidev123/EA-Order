
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Queries.Orders.GetAll;
using Orders.Application.Responses;

namespace Orders.API.Endpoints.Orders
{
    public class GetAllOrdersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync).Produces<PagedResponse<List<GetAllOrdersResponse>>>();

        private static async Task<IResult> HandleAsync([FromServices] IMediator mediator,
                                                        int pageNumber = 1,
                                                        int pageSize = 5)
        {
            var result = await mediator.Send(new GetAllOrdersQuery(pageNumber, pageSize));
            return result.IsSuccess
                ?  Results.Ok(result) 
                : Results.NotFound(result);
        }
    }
}
