using Automaton.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Automaton.Domain.Mappings
{
    public class SummaryMap : IEntityTypeConfiguration<Summary>
    {
        public void Configure(EntityTypeBuilder<Summary> builder)
        {
            builder.ToTable(name: "Summary", schema: "dbo");
            builder.HasKey(x => x.AllPaidOrders);
            builder.Property(x => x.AllPaidOrders).HasColumnName("AllPaidOrders");
            builder.Property(x => x.ThisMonthPaidOrders).HasColumnName("ThisMonthPaidOrders");
            builder.Property(x => x.LastMonthPaidOrders).HasColumnName("LastMonthPaidOrders");
            builder.Property(x => x.Amount).HasColumnName("Amount");
        }
    }
}
