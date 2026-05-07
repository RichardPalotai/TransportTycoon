using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        var mock = new Mock<IPassengerInteractable>();
        mock.Setup(x => x.PassInteractFixedSeed(0)).Returns(-100);
        Assert.AreEqual(-100, mock.Object.PassInteractFixedSeed(0));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
