using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace kol_2.Models;

[Table("Seedling_Batch")]
public class SeedlingBatch
{
    [Key]
    public int BatchId { get; set; }
    
    [ForeignKey(nameof(Nursery))]
    public int NurseryId { get; set; }
    
    [ForeignKey(nameof(TreeSpecies))]
    public int SpeciesId { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime SownDate { get; set; }
    
    public DateTime? ReadyDate { get; set; }
    
    public Nursery Nursery { get; set; } = null!;
    public TreeSpecies TreeSpecies { get; set; } = null!;
    
    public ICollection<Responsible> Responsibles { get; set; } = null!;
}