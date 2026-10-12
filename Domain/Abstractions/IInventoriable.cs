namespace Domain.Abstractions;

// Всё, что имеет инвентарный номер и стоит на балансе
public interface IInventoriable
{
    string InventoryNumber { get; }
    string Name { get; }
}