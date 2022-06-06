using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JournalRepository : RepositoryCrud<Journal, JournalDb>, IJournalRepository
{
    public JournalRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<SaveAction<Task<(Journal Debit, Journal Credit)>>> CreateJournals(
        Journal debit,
        Journal credit
    )
    {
        var debitModel = MapEntityToModel(debit);
        var creditModel = MapEntityToModel(credit);

        var resultCredit = await DbSet.AddAsync(creditModel);
        var resultDebit = await DbSet.AddAsync(debitModel);

        return async () =>
        {
            await SaveChanges();

            return (MapModelToEntity(resultCredit.Entity), MapModelToEntity(resultDebit.Entity));
        };
    }

    protected override IQueryable<JournalDb> GetIncludedDbSet()
    {
        return base.GetIncludedDbSet()
            .Include(journal => journal.SourceAccount)
            .Include(journal => journal.Account)
            .Include(journal => journal.Currency);
    }
}