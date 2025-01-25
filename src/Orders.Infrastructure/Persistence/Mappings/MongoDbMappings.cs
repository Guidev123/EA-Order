using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Orders.Core.DomainObjects;
using Orders.Core.Entities;

namespace Orders.Infrastructure.Persistence.Mappings
{
    public static class MongoDbMappings 
    {
        public static void MapEntity()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
            {
                BsonClassMap.RegisterClassMap<Entity>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapIdField(x => x.Id)
                        .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                });
            }
        }
        public static void MapVoucher()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Voucher)))
            {
                BsonClassMap.RegisterClassMap<Voucher>(map =>
                {
                    map.AutoMap();
                    map.MapProperty(c => c.Code).SetElementName("code");
                    map.MapProperty(c => c.Percentual).SetElementName("percentual");
                    map.MapProperty(c => c.DiscountValue).SetElementName("discount_value");
                    map.MapProperty(c => c.Quantity).SetElementName("quantity");
                    map.MapProperty(c => c.DiscountType).SetElementName("discount_type");
                    map.MapProperty(c => c.ExpiresAt).SetElementName("expires_at");
                    map.MapProperty(c => c.CreatedAt).SetElementName("created_at");
                    map.MapProperty(c => c.IsActive).SetElementName("is_active");
                    map.SetIgnoreExtraElements(true);
                });
            }
        }

        public static void MapOrder()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Order)))
            {
                BsonClassMap.RegisterClassMap<Order>(map =>
                {
                    map.AutoMap();
                    map.MapMember(o => o.Code).SetElementName("code");
                    map.MapMember(o => o.CustomerId).SetElementName("customer_id")
                    .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

                    map.MapMember(o => o.VoucherId)
                   .SetElementName("voucher_id")
                   .SetSerializer(new NullableSerializer<Guid>(new GuidSerializer(GuidRepresentation.Standard)))
                   .SetIgnoreIfNull(true);

                    map.MapMember(o => o.VoucherIsUsed).SetElementName("voucher_is_used");
                    map.MapMember(o => o.Discount).SetElementName("discount");
                    map.MapMember(o => o.TotalPrice).SetElementName("total_price");
                    map.MapMember(o => o.CreatedAt).SetElementName("created_at");
                    map.MapMember(o => o.OrderStatus).SetElementName("order_status");
                    map.MapMember(o => o.Voucher).SetElementName("voucher").SetIgnoreIfNull(true);
                    map.MapMember(o => o.Address).SetElementName("address");
                    map.MapMember(o => o.OrderItems)
                        .SetElementName("order_items");

                    map.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(OrderItem)))
            {
                BsonClassMap.RegisterClassMap<OrderItem>(map =>
                {
                    map.AutoMap();
                    map.MapProperty(i => i.OrderId).SetElementName("order_id")
                    .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

                    map.MapProperty(i => i.ProductId).SetElementName("product_id")
                    .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

                    map.MapProperty(i => i.Name).SetElementName("name");
                    map.MapProperty(i => i.Quantity).SetElementName("quantity");
                    map.MapProperty(i => i.Price).SetElementName("price");
                    map.MapProperty(i => i.ImageUrl).SetElementName("image_url");

                    map.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
