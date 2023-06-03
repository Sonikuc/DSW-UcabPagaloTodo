using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Infrastructure.Database.Configuration
{
    public class ProviderEntityConfiguration : IEntityTypeConfiguration<ProviderEntity>
    {
        public void Configure(EntityTypeBuilder<ProviderEntity> builder)
        {
            // Configura la clave principal de la clase base
            builder.HasBaseType<UserEntity>();

            builder.Property(provider => provider.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            // Configura la relación con ServiceEntity
            builder.HasMany(provider => provider.Service)
                .WithOne(service => service.Provider)
                .HasForeignKey(service => service.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
