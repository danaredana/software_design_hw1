namespace Domain.Abstractions;

// Всё, что потребляет энергию (самокаты, зарядные шкафы и т.д.)
public interface IEnergyConsumer
{
    // Суточный расход кВт * ч
    decimal DailyEnergyConsumptionKwh { get; }
}