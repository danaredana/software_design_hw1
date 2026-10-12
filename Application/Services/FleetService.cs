namespace Application.Services;

using Domain.Abstractions;
using Domain.Items;
using Domain.Vehicles;

// Главный сервис: приём техники, отчёты и т.д.
public class FleetService
{
    private readonly IFleetRepository _repository;
    private readonly IServiceCenter _serviceCenter;

    public FleetService(IFleetRepository repository, IServiceCenter serviceCenter)
    {
        _repository = repository;
        _serviceCenter = serviceCenter;
    }

    // Добавить транспорт (с техосмотром)
    public bool TryAddVehicle(Vehicle vehicle)
    {
        if (!_serviceCenter.Inspect(vehicle))
            return false; // отклонили на техосмотре

        _repository.AddVehicle(vehicle);
        return true;
    }

    // Добавить вещь (без техосмотра)
    public void AddItem(Item item)
    {
        _repository.AddItem(item);
    }

    // Суммарное суточное потребление энергии
    public decimal GetTotalDailyEnergy()
    {
        decimal total = 0;

        foreach (var v in _repository.GetAllVehicles())
            total += v.DailyEnergyConsumptionKwh;

        foreach (var item in _repository.GetAllItems())
        {
            if (item is IEnergyConsumer consumer)
                total += consumer.DailyEnergyConsumptionKwh;
        }

        return total;
    }

    // Список устройств для новичков (BeginnerScore >= 6)
    public IReadOnlyList<Vehicle> GetBeginnerFriendlyVehicles()
    {
        return _repository.GetAllVehicles()
            .Where(v => v.BeginnerScore >= 6)
            .ToList();
    }

    // Всё, что на балансе (техника + вещи)
    public IReadOnlyList<IInventoriable> GetAllInventory()
    {
        return _repository.GetAllInventoriable();
    }

    public IReadOnlyList<Vehicle> GetAllVehicles() => _repository.GetAllVehicles();
    public IReadOnlyList<Item> GetAllItems() => _repository.GetAllItems();
}