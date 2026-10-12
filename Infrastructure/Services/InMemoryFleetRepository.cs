namespace Infrastructure.Services;

using Application.Services;
using Domain.Abstractions;
using Domain.Items;
using Domain.Vehicles;

// Простое хранение в памяти (списки)
public class InMemoryFleetRepository : IFleetRepository
{
    private readonly List<Vehicle> _vehicles = new();
    private readonly List<Item> _items = new();

    public void AddVehicle(Vehicle vehicle) => _vehicles.Add(vehicle);
    public void AddItem(Item item) => _items.Add(item);

    public IReadOnlyList<Vehicle> GetAllVehicles() => _vehicles.AsReadOnly();
    public IReadOnlyList<Item> GetAllItems() => _items.AsReadOnly();

    public IReadOnlyList<IInventoriable> GetAllInventoriable()
    {
        var result = new List<IInventoriable>();
        result.AddRange(_vehicles);
        result.AddRange(_items);
        return result.AsReadOnly();
    }
}