using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static NUnit.Framework.Assert;
public class ProdFacilityTestScript
{
    [Test]
    public void ProdFacilityFactoryUpdateProduceTest()
    {
        var factory = new Factory<Steel>();

        factory.Update(1.0);
        AreEqual(12, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityFactoryUpdateBufferTest()
    {
        var factory = new Factory<Steel>();

        factory.Update(0.5);
        factory.Update(0.5);

        AreEqual(12, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityFactoryUpdateForALotOfSmallStepsTest()
    {
        var factory = new Factory<Steel>();

        for (int i = 0; i < 100; i++)
            factory.Update(0.01);

        AreEqual(12, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityFactoryProdInteractTest()
    {
        var factory = new Factory<Steel>();

        factory.Update(1.0);

        int taken = factory.ProdInteract(5);

        AreEqual(5, taken);
        AreEqual(7, factory.ProducedCount);
    }
}
