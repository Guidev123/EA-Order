
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Queries.Orders.GetAll;
using Orders.Application.Responses;
using Orders.Application.Services;

namespace Orders.API.Endpoints.Orders
{
    public class GetAllOrdersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync).Produces<PagedResponse<List<GetAllOrdersResponse>>>();

        private static async Task<IResult> HandleAsync([FromServices] IMediator mediator,
                                                       [FromServices] IUserService user,
                                                        int pageNumber = 1,
                                                        int pageSize = 5)
        {
            var userId = await user.GetUserIdAsync() ?? Guid.Empty;
            var result = await mediator.Send(new GetAllOrdersQuery(pageNumber, pageSize, userId));
            return result.IsSuccess
                ?  Results.Ok(result) 
                : Results.NotFound(result);
        }
    }
}
