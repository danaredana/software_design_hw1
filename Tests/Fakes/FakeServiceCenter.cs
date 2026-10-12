namespace Tests.Fakes;

using Application.Services;
using Domain.Vehicles;

// Простая поддельная реализация техосмотра.
// Нужна только для тестов
public class FakeServiceCenter : IServiceCenter
{
    private readonly bool _acceptAll;

    public FakeServiceCenter(bool acceptAll = true)
    {
        _acceptAll = acceptAll;
    }

    // Сюда складываем все машины, которые приходили на осмотр
    public List<Vehicle> InspectedVehicles { get; } = new();

    public bool Inspect(Vehicle vehicle)
    {
        InspectedVehicles.Add(vehicle);
        return _acceptAll;
    }
}