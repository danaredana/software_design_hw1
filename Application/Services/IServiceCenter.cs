namespace Application.Services;

using Domain.Vehicles;

// Решает, принимать ли новое устройство в парк
public interface IServiceCenter
{
    // true = приняли, false = отклонили
    bool Inspect(Vehicle vehicle);
}