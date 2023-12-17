using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(500);
            builder.HasOne(s => s.Admin)
                .WithMany(u => u.Rooms)
                .IsRequired();                         
        }
    }
}
