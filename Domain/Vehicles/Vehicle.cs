namespace Domain.Vehicles;

using Domain.Abstractions;

// Базовый класс транспорта
public abstract class Vehicle : IInventoriable, IEnergyConsumer, IBeginnerFriendly
{
    protected Vehicle(string inventoryNumber, string name,
        decimal dailyEnergyKwh, int beginnerScore)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер обязателен");
        if (beginnerScore is < 1 or > 10)
            throw new ArgumentOutOfRangeException(nameof(beginnerScore));

        InventoryNumber = inventoryNumber;
        Name = name;
        DailyEnergyConsumptionKwh = dailyEnergyKwh;
        BeginnerScore = beginnerScore;
    }

    public string InventoryNumber { get; }
    public string Name { get; }
    public decimal DailyEnergyConsumptionKwh { get; }
    public int BeginnerScore { get; }

    public abstract string VehicleType { get; }
}