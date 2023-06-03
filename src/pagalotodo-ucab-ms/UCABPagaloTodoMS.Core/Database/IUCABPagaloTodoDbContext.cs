using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Core.Database
{
    public interface IUCABPagaloTodoDbContext
    {
        DbSet<ValoresEntity> Valores { get; }
        DbSet<UserEntity> UserEntities { get; set; }

		DbSet<ServiceEntity> ServiceEntities { get; set; }

        DbSet<BillEntity> BillEntities { get; set; }

        DbSet<PaymentOptionEntity> PaymentOptionEntities { get; set; }

      
    
        DbContext DbContext
        {
            get;
        }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);
        
    }
}
