namespace Domain.Items;

using Domain.Abstractions;

// Базовый класс вещей (шлемы, док-станции, зарядные шкафы...)
public abstract class Item : IInventoriable
{
    protected Item(string inventoryNumber, string name)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер обязателен");

        InventoryNumber = inventoryNumber;
        Name = name;
    }

    public string InventoryNumber { get; }
    public string Name { get; }

    public abstract string ItemType { get; }
}