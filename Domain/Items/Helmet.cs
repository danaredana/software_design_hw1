namespace Domain.Items;

public sealed class Helmet : Item
{
    public Helmet(string inventoryNumber, string name)
        : base(inventoryNumber, name) { }

    public override string ItemType => "Шлем";
}