using System;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacilityDataDisplayTest
{
    private GameObject root;
    private FacilityDataHandler facilityData;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("FacilityDataHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        facilityData = root.AddComponent<FacilityDataHandler>();

        SetPrivateField("ID_Text", CreateText("ID_Text"));
        SetPrivateField("Traffic_Text", CreateText("Traffic_Text"));
        SetPrivateField("Consume_Text", CreateText("Consume_Text"));
        SetPrivateField("Product_Text", CreateText("Product_Text"));

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
        facilityData.ID = 10;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("ID_Text").text, "10");
    }

    [Test]
    public void Traffic_WhenValueInBounds_UpdatesValue()
    {
        facilityData.Traffic = 80;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Traffic_Text").text, "80%");
    }

    [Test]
    public void Traffic_WhenValueOutOfBounds_ThrowException()
    {
        Assert.Throws<Exception>(() => facilityData.Traffic = -10);
        Assert.Throws<Exception>(() => facilityData.Traffic = 110);
    }

    [Test]
    public void Consume_UpdatesValue()
    {
        string res = "Iron";
        facilityData.Consume = res;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Consume_Text").text, res);
    }

    [Test]
    public void Product_UpdatesValue()
    {
        string res = "Iron";
        facilityData.Product = res;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Product_Text").text, res);
    }

    [Test]
    public void SetButtonsActive_WhenTrue_CloseIsActive()
    {
        facilityData.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Close_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenFalse_CloseIsInactive()
    {
        facilityData.SetButtonsActive(false);

        Assert.IsFalse(GetPrivateField<Button>("Close_btn").interactable);
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
        typeof(FacilityDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(facilityData, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(FacilityDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(facilityData);
    }
}