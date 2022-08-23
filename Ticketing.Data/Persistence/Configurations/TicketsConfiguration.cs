using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Data.Enums;

namespace Ticketing.Data.Persistence.Configurations
{
    public class TicketsConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(500);

            builder.HasOne(t => t.User)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.UserCreatedId);

            builder.Property(t => t.Priority)
                .HasConversion(t => t.ToString(), p => (PriorityLevel)Enum.Parse(typeof(PriorityLevel), p))
                .HasMaxLength(100);

            builder.HasMany(t => t.UserTickets)
                .WithOne(t => t.Ticket)
                .HasForeignKey(t=>t.TicketId).OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(t=>!t.IsClosed);
        }
    }
}
