namespace Domain.Vehicles;

// Велосипед не потребляет энергию
public sealed class Bicycle : Vehicle
{
    public Bicycle(string inventoryNumber, string name, int beginnerScore)
        : base(inventoryNumber, name, dailyEnergyKwh: 0m, beginnerScore) { }

    public override string VehicleType => "Велосипед";
}