namespace Domain.Items;

using Domain.Abstractions;

// Зарядный шкаф / док-станция - и инвентаризируется, и потребляет энергию
public sealed class ChargingStation : Item, IEnergyConsumer
{
    public ChargingStation(string inventoryNumber, string name,
        decimal dailyEnergyKwh)
        : base(inventoryNumber, name)
    {
        DailyEnergyConsumptionKwh = dailyEnergyKwh;
    }

    public override string ItemType => "Зарядная станция";
    public decimal DailyEnergyConsumptionKwh { get; }
}