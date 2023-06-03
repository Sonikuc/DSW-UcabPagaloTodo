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
    public class ServiceEntityConfiguration : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceEntity> builder)
        {
            builder.HasKey(service => service.Id);

            // Configura la relación con ProviderEntity
            builder.HasOne(service => service.Provider)
                .WithMany(provider => provider.Service)
                .HasForeignKey(service => service.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
