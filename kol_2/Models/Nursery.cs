using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace kol_2.Models;

[Table("Nursery")]
public class Nursery
{

    [Key]
    public int NurseryId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    public DateTime EstablishedDate { get; set; }
    
    public ICollection<SeedlingBatch> SeedlingBatches { get; set; } = null!;
}