namespace Application.Services;

using Domain.Abstractions;
using Domain.Items;
using Domain.Vehicles;

// Хранилище всего парка
public interface IFleetRepository
{
    void AddVehicle(Vehicle vehicle);
    void AddItem(Item item);

    IReadOnlyList<Vehicle> GetAllVehicles();
    IReadOnlyList<Item> GetAllItems();
    IReadOnlyList<IInventoriable> GetAllInventoriable();
}