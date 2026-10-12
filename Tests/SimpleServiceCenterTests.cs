namespace Tests;

using Domain.Vehicles;
using Infrastructure.Services;

public class SimpleServiceCenterTests
{
    [Test]
    public void Inspect_WhenBeginnerScoreIs3OrHigher_ShouldAccept()
    {
        var center = new SimpleServiceCenter();
        var scooter = new Scooter("SC-1", "Ok Scooter", 1.0m, 3);

        bool result = center.Inspect(scooter);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Inspect_WhenBeginnerScoreIsLessThan3_ShouldReject()
    {
        var center = new SimpleServiceCenter();
        var scooter = new Scooter("SC-2", "Bad Scooter", 1.0m, 2);

        bool result = center.Inspect(scooter);

        Assert.That(result, Is.False);
    }
}