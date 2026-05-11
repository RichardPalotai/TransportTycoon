using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuilderSelectorTest
{
    private GameObject root;
    private BuilderSelectorHandler builderSelector;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject(
            "BuilderSelectorHandler_TestRoot",
            typeof(RectTransform)
        );

        root.SetActive(false);
        root.AddComponent<Outline>();

        builderSelector = root.AddComponent<BuilderSelectorHandler>();

        GameObject price = CreatePriceHierarchy();
        price.transform.SetParent(root.transform, false);

        SetPrivateField("Price", price);
        SetPrivateField(
            "PriceTag_Text",
            price.transform.Find("PriceTag/PriceTag_text")
                .GetComponent<TextMeshProUGUI>()
        );

        SetPrivateField("Mouse_btn", CreateButton("Mouse_btn"));
        SetPrivateField("Road_btn", CreateButton("Road_btn"));
        SetPrivateField("BusStop_btn", CreateButton("BusStop_btn"));
        SetPrivateField("TrafficLight_btn", CreateButton("TrafficLight_btn"));
        SetPrivateField("Bus_btn", CreateButton("Bus_btn"));
        SetPrivateField("Car_btn", CreateButton("Car_btn"));
        SetPrivateField("Truck_btn", CreateButton("Truck_btn"));
        SetPrivateField("Minivan_btn", CreateButton("Minivan_btn"));

        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void IsMouseSelected_WhenTrue_SelectedButtonIsMouse()
    {
        SetPrivateField("SelectedButton", GetPrivateField<Button>("Mouse_btn"));
        Assert.IsTrue(builderSelector.IsMouseSelected);
    }

    [Test]
    public void IsMouseSelected_WhenFalse_SelectedButtonIsNotMouse()
    {
        SetPrivateField("SelectedButton", GetPrivateField<Button>("Road_btn"));
        Assert.IsFalse(builderSelector.IsMouseSelected);
    }

    [Test]
    public void SetDemolishMode_WhenTrue_SetsMouseOutlineRedAndBuildButtonsInactive()
    {
        builderSelector.SetDemolishMode(true);

        Outline dol = GetPrivateField<Button>("SelectedButton").GetComponent<Outline>();
        Assert.AreEqual(dol.effectColor, Color.red);

        Assert.IsFalse(GetPrivateField<Button>("Road_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("BusStop_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("TrafficLight_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Bus_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Car_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Truck_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Minivan_btn").interactable);
    }

    [Test]
    public void SetDemolishMode_WhenFalse_SetsMouseOutlineBlackAndBuildButtonsActive()
    {
        builderSelector.SetDemolishMode(false);

        Outline dol = GetPrivateField<Button>("SelectedButton").GetComponent<Outline>();
        Assert.AreEqual(dol.effectColor, Color.black);

        Assert.IsTrue(GetPrivateField<Button>("Road_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("BusStop_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("TrafficLight_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Bus_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Car_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Truck_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Minivan_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenFalse_DisablesAllButtons()
    {
        builderSelector.SetButtonsActive(false);

        Assert.IsFalse(GetPrivateField<Button>("Mouse_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Road_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("BusStop_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("TrafficLight_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Bus_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Car_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Truck_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Minivan_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenTrue_EnablesAllButtons()
    {
        builderSelector.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Mouse_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Road_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("BusStop_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("TrafficLight_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Bus_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Car_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Truck_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Minivan_btn").interactable);
    }

    [Test]
    public void SetBuildButtonsActive_WhenFalse_DisablesAllButtons()
    {
        builderSelector.SetButtonsActive(false);

        Assert.IsFalse(GetPrivateField<Button>("Road_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("BusStop_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("TrafficLight_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Bus_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Car_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Truck_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Minivan_btn").interactable);
    }

    [Test]
    public void SetBuildButtonsActive_WhenTrue_EnablesAllButtons()
    {
        builderSelector.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Road_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("BusStop_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("TrafficLight_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Bus_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Car_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Truck_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Minivan_btn").interactable);
    }

    private Button CreateButton(string name)
    {
        GameObject obj = new GameObject(
            name,
            typeof(RectTransform)
        );

        obj.transform.SetParent(root.transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(160, 40);

        Image image = obj.AddComponent<Image>();

        Outline outline = obj.AddComponent<Outline>();
        outline.effectColor = Color.black;
        outline.effectDistance = new Vector2(2f, -2f);

        Button button = obj.AddComponent<Button>();
        button.targetGraphic = image;

        return button;
    }

    private GameObject CreatePriceHierarchy()
    {
        GameObject price = new GameObject("Price", typeof(RectTransform));

        GameObject priceTextObj = new GameObject("Price_text", typeof(RectTransform));
        priceTextObj.transform.SetParent(price.transform, false);
        priceTextObj.AddComponent<TextMeshProUGUI>();

        GameObject priceTag = new GameObject("PriceTag", typeof(RectTransform));
        priceTag.transform.SetParent(price.transform, false);

        GameObject priceTagTextObj = new GameObject("PriceTag_text", typeof(RectTransform));
        priceTagTextObj.transform.SetParent(priceTag.transform, false);
        priceTagTextObj.AddComponent<TextMeshProUGUI>();

        GameObject currencyIcon = new GameObject("Currency_icon", typeof(RectTransform));
        currencyIcon.transform.SetParent(priceTag.transform, false);
        currencyIcon.AddComponent<Image>();

        return price;
    }

    private void SetPrivateField<T>(string fieldName, T value)
    {
        typeof(BuilderSelectorHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(builderSelector, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(BuilderSelectorHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(builderSelector);
    }
}