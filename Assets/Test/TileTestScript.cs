using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TileTestScript
{
    [Test]
    public void TileConstructorTests()
    {
        Tile t1 = new Tile(15, 150);
        Assert.IsNotNull(t1);
        Assert.AreEqual(15, t1.X);
        Assert.AreEqual(150, t1.Y);

        Tile t2 = new Tile(1, 2, new LumberMill<Wood>());
        Assert.IsNotNull(t2);
        Assert.AreEqual(1, t2.X);
        Assert.AreEqual(2, t2.Y);
        Assert.IsNotNull(t2.Entity);
        Assert.IsInstanceOf<LumberMill<Wood>>(t2.Entity);

        Tile t3 = new Tile(1, 2, 12346);
        Assert.IsNotNull(t3);
        Assert.AreEqual(1, t3.X);
        Assert.AreEqual(2, t3.Y);
        Assert.AreEqual(12346, t3.ObjectId);

    }

    [Test]
    public void TileExceptionTests()
    {
        Assert.DoesNotThrow(() => new Tile(15, 15, new BusStop(false)));

    }
}
