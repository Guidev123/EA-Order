﻿using MediatR;
using Orders.Application.Commands.Orders.Create;
using Orders.Application.Responses;
using Orders.Application.Services;

namespace Orders.API.Endpoints.Orders
{
    public class CreateOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync).Produces<Response<CreateOrderResponse>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       CreateOrderCommand command)
        {
            var result = await mediator.Send(command);

            return result.IsSuccess && result.Data is not null
                ? TypedResults.Created($"/{result.Data.OrderCode}", result)
                : result.StatusCode is 404 ? TypedResults.NotFound(result)
                : TypedResults.BadRequest(result);
        }
    }
}
