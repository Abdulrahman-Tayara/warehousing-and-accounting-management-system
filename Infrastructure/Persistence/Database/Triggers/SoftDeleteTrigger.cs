using EntityFrameworkCore.Triggered;
using Infrastructure.Persistence.Database.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Database.Triggers;

public class SoftDeleteTrigger : IBeforeSaveTrigger<ISoftDeletable>
{
    private readonly ApplicationDbContext _applicationContext;

    public SoftDeleteTrigger(ApplicationDbContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task BeforeSave(ITriggerContext<ISoftDeletable> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Deleted)
        {
            var entry = _applicationContext.Entry(context.Entity);
            context.Entity.DeletedAt = DateTime.Now;
            context.Entity.IsDeleted = true;
            entry.State = EntityState.Modified;
        }

        await Task.CompletedTask;
    }
}