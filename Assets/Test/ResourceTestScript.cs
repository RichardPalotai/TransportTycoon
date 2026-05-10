using NUnit.Framework;
using static NUnit.Framework.Assert;
public class ResourceTestScript
{
    [Test]
    public void IronTestScript()
    {
        AreEqual("Iron", Iron.Instance.NameString);
    }
    [Test]
    public void SteelTestScript()
    {
        AreEqual("Steel", Steel.Instance.NameString);
    }
    [Test]
    public void WoodTestScript()
    {
        AreEqual("Wood", Wood.Instance.NameString);
    }
    [Test]
    public void PaperTestScript()
    {
        AreEqual("Paper", Paper.Instance.NameString);
    }
    [Test]
    public void CheeseTestScript()
    {
        AreEqual("Cheese", Cheese.Instance.NameString);
    }
    [Test]
    public void MilkTestScript()
    {
        AreEqual("Milk", Milk.Instance.NameString);
    }
    [Test]
    public void EggTestScript()
    {
        AreEqual("Egg", Egg.Instance.NameString);
    }
}
