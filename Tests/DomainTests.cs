namespace Tests;

using Domain.Items;
using Domain.Vehicles;

public class DomainTests
{
    [Test]
    public void Vehicle_WithoutInventoryNumber_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            new Scooter("", "Test", 1.0m, 5));
    }

    [Test]
    public void Vehicle_WithBeginnerScoreOutOfRange_ShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Scooter("SC-1", "Test", 1.0m, 0));

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Scooter("SC-1", "Test", 1.0m, 11));
    }

    [Test]
    public void Bicycle_ShouldHaveZeroEnergy()
    {
        var bike = new Bicycle("BK-1", "Stels", 8);

        Assert.That(bike.DailyEnergyConsumptionKwh, Is.EqualTo(0m));
    }

    [Test]
    public void Item_WithoutInventoryNumber_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            new Helmet("", "Шлем"));
    }

    [Test]
    public void ChargingStation_ShouldImplementEnergyConsumer()
    {
        var station = new ChargingStation("CS-1", "Шкаф", 3.5m);

        Assert.That(station.DailyEnergyConsumptionKwh, Is.EqualTo(3.5m));
        Assert.That(station.ItemType, Is.EqualTo("Зарядная станция"));
    }
}