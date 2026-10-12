namespace Domain.Vehicles;

public sealed class Scooter : Vehicle
{
    public Scooter(string inventoryNumber, string name,
        decimal dailyEnergyKwh, int beginnerScore)
        : base(inventoryNumber, name, dailyEnergyKwh, beginnerScore) { }

    public override string VehicleType => "Самокат";
}