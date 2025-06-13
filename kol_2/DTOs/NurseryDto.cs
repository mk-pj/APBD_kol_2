using kol_2.Models;

namespace kol_2.DTOs;

public class NurseryDto
{
    public int NurseryId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime EstablishedDate { get; set; }
    public List<SeedlingBatchesDto> Batches { get; set; } = null!;
}

public class SeedlingBatchesDto
{
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }
    public TreeSpeciesDto Species { get; set; } = null!;
    public List<ResponsibleDto> Responsible { get; set; } = null!;
}

public class TreeSpeciesDto
{
    public string LatinName { get; set; } = null!;
    public int GrowthTimeInYears { get; set; }
}

public class ResponsibleDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
}