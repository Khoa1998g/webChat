using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using WebApplication1.Data.Entities;
using Message = WebApplication1.Data.Entities.Message;

namespace WebApplication1.Data.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.Property(s => s.Content).IsRequired().HasMaxLength(500);
            builder.HasOne(s => s.ToRoom)
                .WithMany(m => m.Messages)
                .HasForeignKey(m => m.ToRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
