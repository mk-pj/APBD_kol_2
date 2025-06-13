using kol_2.DTOs;

namespace kol_2.Services;

public interface IDbService
{
    Task<NurseryDto> GetNurseryAsync(int nurseryId);
    Task AddNewBatchAsync(NewBatchDto newBatchDto);
}