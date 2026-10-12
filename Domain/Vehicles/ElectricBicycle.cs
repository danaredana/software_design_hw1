namespace Domain.Vehicles;

public sealed class ElectricBicycle : Vehicle
{
    public ElectricBicycle(string inventoryNumber, string name,
        decimal dailyEnergyKwh, int beginnerScore)
        : base(inventoryNumber, name, dailyEnergyKwh, beginnerScore) { }

    public override string VehicleType => "Электровелосипед";
}