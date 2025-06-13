namespace kol_2.DTOs;

public class NewBatchDto
{
    public int Quantity { get; set; }
    public string Species { get; set; } = null!;
    public string Nursery { get; set; } = null!;
    public List<AddedResponsibleDto> Responsible { get; set; } = null!;
}

public class AddedResponsibleDto
{
    public int EmployeeId { get; set; }
    public string Role { get; set; } = null!;
}
