using System;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleDataDisplayTest
{
    private GameObject root;
    private VehicleDataHandler vehicleData;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("VehicleDataHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        vehicleData = root.AddComponent<VehicleDataHandler>();

        SetPrivateField("ID_Text", CreateText("ID_Text"));
        SetPrivateField("Resource_Text", CreateText("Resource_Text"));
        SetPrivateField("Capacity_Text", CreateText("Capacity_Text"));
        SetPrivateField("Condition_Text", CreateText("Condition_Text"));
        SetPrivateField("RepairCost_Text", CreateText("RepairCost_Text"));
        SetPrivateField("Worth_Text", CreateText("Worth_Text"));

        SetPrivateField("Close_btn", CreateButton("Close_btn"));
        SetPrivateField("SetRoute_btn", CreateButton("SetRoute_btn"));
        SetPrivateField("Repair_btn", CreateButton("Repair_btn"));
        SetPrivateField("Sell_btn", CreateButton("Sell_btn"));

        // Now Awake/Start can run safely
        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void ID_UpdatesValue()
    {
        vehicleData.ID = 10;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("ID_Text").text, "10");
    }

    [Test]
    public void Resource_UpdatesValue()
    {
        string res = "Milk";
        vehicleData.Resource = res;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Resource_Text").text, res);
    }

    [Test]
    public void Capacity_WhenPositive_UpdatesValue()
    {
        vehicleData.Capacity = 10;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Capacity_Text").text, "10");
    }

    [Test]
    public void Capacity_WhenNotPositive_ThrowsException()
    {
        Assert.Throws<Exception>(() => vehicleData.Capacity = 0);
        Assert.Throws<Exception>(() => vehicleData.Capacity = -10);
    }

    [Test]
    public void Condition_WhenValueInBounds_UpdatesValue()
    {
        vehicleData.Condition = 80;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Condition_Text").text, "80%");
    }

    [Test]
    public void Condition_WhenValueOutOfBounds_ThrowException()
    {
        Assert.Throws<Exception>(() => vehicleData.Condition = -10);
        Assert.Throws<Exception>(() => vehicleData.Condition = 110);
    }

    [Test]
    public void RepairCost_WhenNotNegative_UpdatesValue()
    {
        vehicleData.RepairCost = 0;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("RepairCost_Text").text, "0");
        vehicleData.RepairCost = 100;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("RepairCost_Text").text, "100");
    }

    [Test]
    public void RepairCost_WhenNegative_ThrowsException()
    {
        Assert.Throws<Exception>(() => vehicleData.RepairCost = -10);
    }

    [Test]
    public void Worth_WhenNotNegative_UpdatesValue()
    {
        vehicleData.Worth = 0;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Worth_Text").text, "0");
        vehicleData.Worth = 100;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Worth_Text").text, "100");
    }

    [Test]
    public void Worth_WhenNegative_ThrowsException()
    {
        Assert.Throws<Exception>(() => vehicleData.Worth = -10);
    }

    [Test]
    public void SetButtonsActive_WhenTrue_ButtnsAreActive()
    {
        vehicleData.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Close_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("SetRoute_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Repair_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Sell_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenFalse_ButtonsAreInactive()
    {
        vehicleData.SetButtonsActive(false);

        Assert.IsFalse(GetPrivateField<Button>("Close_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("SetRoute_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Repair_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Sell_btn").interactable);
    }

    private TextMeshProUGUI CreateText(string name)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(root.transform);
        return obj.AddComponent<TextMeshProUGUI>();
    }

    private Button CreateButton(string name)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(root.transform);

        Image image = obj.AddComponent<Image>();
        Button button = obj.AddComponent<Button>();
        button.targetGraphic = image;

        return button;
    }

    private void SetPrivateField<T>(string fieldName, T value)
    {
        typeof(VehicleDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(vehicleData, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(VehicleDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(vehicleData);
    }
}