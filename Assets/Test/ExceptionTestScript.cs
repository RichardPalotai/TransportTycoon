using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExceptionTestScript
{
    [Test]
    public void ExceptionTagTest()
    {
        FieldOverrideException e = new FieldOverrideException();
        Assert.AreEqual("Field Override Error", e.Tag);
        NotEnoughMoneyException e2 = new NotEnoughMoneyException();
        Assert.AreEqual("Not Enough Money Error", e2.Tag);
        ObjectIdIsNotSetException e3 = new ObjectIdIsNotSetException();
        Assert.AreEqual("Object Id Is Not Set Error", e3.Tag);
        VehicleConditionException e4 = new VehicleConditionException();
        Assert.AreEqual("Vehicle Condition Error", e4.Tag);
    }
}
