namespace Infrastructure.Services;

using Application.Services;
using Domain.Vehicles;

// Пока что реализация: принимаем всё, у чего BeginnerScore >= 3
public class SimpleServiceCenter : IServiceCenter
{
    public bool Inspect(Vehicle vehicle)
    {
        // Можно усложнить правило позже
        return vehicle.BeginnerScore >= 3;
    }
}