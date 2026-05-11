using System;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightDataDisplayTest
{
    private GameObject root;
    private TrafficLightDataHandler trafficlightData;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("TrafficLightDataHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        trafficlightData = root.AddComponent<TrafficLightDataHandler>();

        SetPrivateField("ID_Text", CreateText("ID_Text"));
        SetPrivateField("Worth_Text", CreateText("Worth_Text"));
        SetPrivateField("GreenLight_Text", CreateText("GreenLight_Text"));

        SetPrivateField("Plus_btn", CreateButton("Plus_btn"));
        SetPrivateField("Minus_btn", CreateButton("Minus_btn"));
        SetPrivateField("Sell_btn", CreateButton("Sell_btn"));
        SetPrivateField("Close_btn", CreateButton("Close_btn"));

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
        trafficlightData.ID = 10;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("ID_Text").text, "10");
    }

    [Test]
    public void Worth_WhenNotNegative_UpdatesValue()
    {
        trafficlightData.Worth = 0;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Worth_Text").text, "0");
        trafficlightData.Worth = 100;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Worth_Text").text, "100");
    }

    [Test]
    public void Worth_WhenNegative_ThrowsException()
    {
        Assert.Throws<Exception>(() => trafficlightData.Worth = -10);
    }

    [Test]
    public void GreenLight_WhenNotPositive_ThrowsException()
    {
        Assert.Throws<Exception>(() => trafficlightData.GreenLight = 0);
        Assert.Throws<Exception>(() => trafficlightData.GreenLight = -10);
    }

    [Test]
    public void GreenLight_WhenOverSixty_ThrowsException()
    {
        Assert.Throws<Exception>(() => trafficlightData.GreenLight = 61);
    }

    [Test]
    public void GreenLight_WhenInBounds_UpdatesValue()
    {
        trafficlightData.GreenLight = 30;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("GreenLight_Text").text, "30");
    }

    [Test]
    public void SetButtonsActive_WhenTrue_ButtnsAreActive()
    {
        trafficlightData.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Close_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Plus_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Minus_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Sell_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenFalse_ButtonsAreInactive()
    {
        trafficlightData.SetButtonsActive(false);

        Assert.IsFalse(GetPrivateField<Button>("Close_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Plus_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Minus_btn").interactable);
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
        typeof(TrafficLightDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(trafficlightData, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(TrafficLightDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(trafficlightData);
    }
}