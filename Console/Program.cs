using Application.Services;
using Domain.Items;
using Domain.Vehicles;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddInfrastructure();
var serviceProvider = services.BuildServiceProvider();

var fleetService = serviceProvider.GetRequiredService<FleetService>();

Console.WriteLine("ВышКат: учёт парка\n");

bool exit = false;
while (!exit)
{
    PrintMenu();
    string? choice = Console.ReadLine()?.Trim();

    switch (choice)
    {
        case "1":
            AddVehicle(fleetService);
            break;
        case "2":
            AddItem(fleetService);
            break;
        case "3":
            ShowTotalEnergy(fleetService);
            break;
        case "4":
            ShowBeginnerVehicles(fleetService);
            break;
        case "5":
            ShowAllInventory(fleetService);
            break;
        case "0":
            exit = true;
            Console.WriteLine("Выход.");
            break;
        default:
            Console.WriteLine("Неизвестная команда. Попробуйте ещё раз.\n");
            break;
    }
}

// вспомогательные методы 

static void PrintMenu()
{
    Console.WriteLine("Меню:");
    Console.WriteLine("  1. Добавить транспорт");
    Console.WriteLine("  2. Добавить вещь");
    Console.WriteLine("  3. Показать суммарную энергию");
    Console.WriteLine("  4. Показать технику для новичков");
    Console.WriteLine("  5. Показать весь инвентарь");
    Console.WriteLine("  0. Выход");
    Console.Write("Выбор: ");
}

static void AddVehicle(FleetService fleetService)
{
    Console.WriteLine("\nТип транспорта:");
    Console.WriteLine("  1. Самокат");
    Console.WriteLine("  2. Велосипед");
    Console.WriteLine("  3. Электровелосипед");
    Console.Write("Выбор: ");
    string? type = Console.ReadLine()?.Trim();

    Console.Write("Инвентарный номер: ");
    string? inv = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(inv))
    {
        Console.WriteLine("Инвентарный номер обязателен.\n");
        return;
    }

    Console.Write("Название: ");
    string? name = Console.ReadLine()?.Trim() ?? "Без имени";

    Console.Write("Оценка для новичка (1-10): ");
    if (!int.TryParse(Console.ReadLine(), out int score) || score < 1 || score > 10)
    {
        Console.WriteLine("Оценка должна быть от 1 до 10.\n");
        return;
    }

    Vehicle? vehicle = null;

    switch (type)
    {
        case "1":
            Console.Write("Суточный расход энергии (кВт·ч): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal energyScooter))
            {
                Console.WriteLine("Неверный формат числа.\n");
                return;
            }
            vehicle = new Scooter(inv, name, energyScooter, score);
            break;

        case "2":
            vehicle = new Bicycle(inv, name, score);
            break;

        case "3":
            Console.Write("Суточный расход энергии (кВт·ч): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal energyEBike))
            {
                Console.WriteLine("Неверный формат числа.\n");
                return;
            }
            vehicle = new ElectricBicycle(inv, name, energyEBike, score);
            break;

        default:
            Console.WriteLine("Неизвестный тип.\n");
            return;
    }

    bool accepted = fleetService.TryAddVehicle(vehicle);
    Console.WriteLine(accepted
        ? $"→ {vehicle.VehicleType} [{vehicle.InventoryNumber}] ПРИНЯТ\n"
        : $"→ {vehicle.VehicleType} [{vehicle.InventoryNumber}] ОТКЛОНЁН техосмотром\n");
}

static void AddItem(FleetService fleetService)
{
    Console.WriteLine("\nТип вещи:");
    Console.WriteLine("  1. Шлем");
    Console.WriteLine("  2. Зарядная станция");
    Console.Write("Выбор: ");
    string? type = Console.ReadLine()?.Trim();

    Console.Write("Инвентарный номер: ");
    string? inv = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(inv))
    {
        Console.WriteLine("Инвентарный номер обязателен.\n");
        return;
    }

    Console.Write("Название: ");
    string? name = Console.ReadLine()?.Trim() ?? "Без имени";

    switch (type)
    {
        case "1":
            fleetService.AddItem(new Helmet(inv, name));
            Console.WriteLine($"→ Шлем [{inv}] добавлен\n");
            break;

        case "2":
            Console.Write("Суточный расход энергии (кВт·ч): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal energy))
            {
                Console.WriteLine("Неверный формат числа.\n");
                return;
            }
            fleetService.AddItem(new ChargingStation(inv, name, energy));
            Console.WriteLine($"→ Зарядная станция [{inv}] добавлена\n");
            break;

        default:
            Console.WriteLine("Неизвестный тип.\n");
            break;
    }
}

static void ShowTotalEnergy(FleetService fleetService)
{
    decimal total = fleetService.GetTotalDailyEnergy();
    Console.WriteLine($"\nСуммарное суточное потребление: {total} кВт·ч\n");
}

static void ShowBeginnerVehicles(FleetService fleetService)
{
    var list = fleetService.GetBeginnerFriendlyVehicles();
    Console.WriteLine("\nТехника для новичков (оценка ≥ 6):");
    if (list.Count == 0)
    {
        Console.WriteLine("  (пусто)");
    }
    else
    {
        foreach (var v in list)
            Console.WriteLine($"  - {v.VehicleType}: {v.Name} [{v.InventoryNumber}] (оценка {v.BeginnerScore})");
    }
    Console.WriteLine();
}

static void ShowAllInventory(FleetService fleetService)
{
    var list = fleetService.GetAllInventory();
    Console.WriteLine("\nТехника и вещи в парке:");
    if (list.Count == 0)
    {
        Console.WriteLine("  (пусто)");
    }
    else
    {
        foreach (var item in list)
            Console.WriteLine($"  - {item.Name} [{item.InventoryNumber}]");
    }
    Console.WriteLine();
}