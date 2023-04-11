using Automaton.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Automaton.Domain.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(name: "Order", schema: "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.ProductId).HasColumnName("ProductId");
            builder.Property(x => x.EmailMessageId).HasColumnName("EmailMessageId");
            builder.Property(x => x.CustomerEmailAddress).HasColumnName("CustomerEmailAddress");
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(x => x.Price).HasColumnName("Price");
            builder.Property(x => x.PaymentId).HasColumnName("PaymentId");
            builder.Property(x => x.MessageSent).HasColumnName("MessageSent");
            builder.Property(x => x.OrderSent).HasColumnName("OrderSent");
        }
    }
}