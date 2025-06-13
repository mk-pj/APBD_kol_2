using kol_2.Data;
using kol_2.DTOs;
using kol_2.Middlewares;
using kol_2.Models;
using Microsoft.EntityFrameworkCore;

namespace kol_2.Services;

public class DbService(SeedingDbContext context) : IDbService
{
    private readonly SeedingDbContext _context = context;
    
    public async Task<NurseryDto> GetNurseryAsync(int nurseryId)
    {
        var nursery = await _context.Nurseries
            .Where(n => n.NurseryId == nurseryId)
            .Select(c => new NurseryDto()
            {
                NurseryId = nurseryId,
                Name = c.Name,
                EstablishedDate = c.EstablishedDate,
                Batches = c.SeedlingBatches.Select(ph => new SeedlingBatchesDto()
                {
                    BatchId = ph.BatchId,
                    Quantity = ph.Quantity,
                    SownDate = ph.SownDate,
                    ReadyDate = ph.ReadyDate,
                    Species = new TreeSpeciesDto()
                    {
                        LatinName = ph.TreeSpecies.LatinName,
                        GrowthTimeInYears = ph.TreeSpecies.GrowthTimeLnYears
                    },
                    Responsible = ph.Responsibles.Select(r => new ResponsibleDto()
                    {
                        FirstName = r.Employee.FirstName,
                        LastName = r.Employee.LastName,
                        Role = r.Role
                    }).ToList()
                }).ToList()
            }).FirstOrDefaultAsync();
        
        if (nursery is null)
            throw new NotFoundException($"Nursery {nurseryId} not found");
        
        return nursery;
    }

    public async Task AddNewBatchAsync(NewBatchDto newBatchDto)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
    
        try
        {
            var species = await _context.TreeSpecies
                .FirstOrDefaultAsync(ts => ts.LatinName.Equals(newBatchDto.Species));
                
            if(species is null)
                throw new NotFoundException($"Tree species {newBatchDto.Species} not found");

            var nurseries = await _context.Nurseries.ToListAsync();

            var nursery = nurseries.FirstOrDefault(n => n.Name.Equals(newBatchDto.Nursery));
            
            if(nursery is null)
                throw new NotFoundException($"Nursery {newBatchDto.Nursery} doesn't exist");
    
            var employees = await _context.Employees.ToListAsync();

            var newBatch = new SeedlingBatch()
            {
                NurseryId = nursery.NurseryId,
                SpeciesId = species.SpeciesId,
                Quantity = newBatchDto.Quantity,
                SownDate = DateTime.Now,
                ReadyDate = null
            };

            await _context.SeedlingBatches.AddAsync(newBatch);
   
            await _context.SaveChangesAsync();
            
            foreach (var responsible in newBatchDto.Responsible)
            {
                var employee = employees.FirstOrDefault(e => e.EmployeeId == responsible.EmployeeId);
                
                if(employee is null)
                    throw new NotFoundException($"Employee {responsible.EmployeeId} not found");

                await _context.AddAsync(new Responsible()
                {
                    BatchId = newBatch.BatchId,
                    EmployeeId = responsible.EmployeeId,
                    Role = responsible.Role
                });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}