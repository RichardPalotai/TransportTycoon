using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static NUnit.Framework.Assert;
public class BusStopTestScript
{
    [Test]
    public void BusStopTestScriptSimplePasses()
    {
        BusStop stop = new(false);
        IsInstanceOf<IPassengerInteractable>(stop);
        IsInstanceOf<Facility>(stop);
        for (int i = 0; i < 10; ++i)
        {
            That(stop.PassInteract(), Is.InRange(-10, 10));
        }
    }

}
