namespace Tests;

using Application.Services;
using Domain.Items;
using Domain.Vehicles;
using Infrastructure.Services;
using Tests.Fakes;

public class FleetServiceTests
{
    // Собираем сервис с нужным техосмотром
    private static FleetService CreateService(IServiceCenter serviceCenter)
    {
        var repository = new InMemoryFleetRepository();
        return new FleetService(repository, serviceCenter);
    }

    [Test]
    public void TryAddVehicle_WhenServiceCenterAccepts_ShouldAddVehicle()
    {
        // Arrange
        var fakeCenter = new FakeServiceCenter(acceptAll: true);
        var service = CreateService(fakeCenter);
        var scooter = new Scooter("SC-TEST-01", "Test Scooter", 1.0m, 7);

        // Act
        bool accepted = service.TryAddVehicle(scooter);

        // Assert
        Assert.That(accepted, Is.True);
        Assert.That(service.GetAllVehicles(), Has.Count.EqualTo(1));
        Assert.That(fakeCenter.InspectedVehicles, Has.Count.EqualTo(1));
    }

    [Test]
    public void TryAddVehicle_WhenServiceCenterRejects_ShouldNotAddVehicle()
    {
        // Arrange
        var fakeCenter = new FakeServiceCenter(acceptAll: false);
        var service = CreateService(fakeCenter);
        var scooter = new Scooter("SC-TEST-02", "Bad Scooter", 1.0m, 2);

        // Act
        bool accepted = service.TryAddVehicle(scooter);

        // Assert
        Assert.That(accepted, Is.False);
        Assert.That(service.GetAllVehicles(), Is.Empty);
        Assert.That(fakeCenter.InspectedVehicles, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetBeginnerFriendlyVehicles_ShouldReturnOnlyScoreGreaterOrEqual6()
    {
        // Arrange
        var fakeCenter = new FakeServiceCenter(acceptAll: true);
        var service = CreateService(fakeCenter);

        service.TryAddVehicle(new Scooter("SC-1", "Easy", 1.0m, 8));   // подходит
        service.TryAddVehicle(new Scooter("SC-2", "Hard", 1.0m, 4));   // не подходит
        service.TryAddVehicle(new Bicycle("BK-1", "Bike", 9));          // подходит

        // Act
        var beginners = service.GetBeginnerFriendlyVehicles();

        // Assert
        Assert.That(beginners, Has.Count.EqualTo(2));
        Assert.That(beginners.All(v => v.BeginnerScore >= 6), Is.True);
    }
    
    [Test]
    public void GetTotalDailyEnergy_ShouldSumVehiclesAndEnergyItems()
    {
        // Arrange
        var fakeCenter = new FakeServiceCenter(acceptAll: true);
        var service = CreateService(fakeCenter);

        service.TryAddVehicle(new Scooter("SC-1", "Scooter", 1.5m, 7));
        service.TryAddVehicle(new Bicycle("BK-1", "Bike", 8));          // 0 кВт·ч
        service.AddItem(new ChargingStation("CS-1", "Зарядка", 4.0m));
        service.AddItem(new Helmet("HL-1", "Шлем"));                    // не потребляет

        // Act
        decimal total = service.GetTotalDailyEnergy();

        // Assert
        Assert.That(total, Is.EqualTo(5.5m)); // 1.5 + 0 + 4.0
    }

    [Test]
    public void GetAllInventory_ShouldReturnVehiclesAndItems()
    {
        // Arrange
        var fakeCenter = new FakeServiceCenter(acceptAll: true);
        var service = CreateService(fakeCenter);

        service.TryAddVehicle(new Scooter("SC-1", "Scooter", 1.0m, 7));
        service.AddItem(new Helmet("HL-1", "Шлем"));

        // Act
        var inventory = service.GetAllInventory();

        // Assert
        Assert.That(inventory, Has.Count.EqualTo(2));
        Assert.That(inventory.Any(i => i.InventoryNumber == "SC-1"), Is.True);
        Assert.That(inventory.Any(i => i.InventoryNumber == "HL-1"), Is.True);
    }
}