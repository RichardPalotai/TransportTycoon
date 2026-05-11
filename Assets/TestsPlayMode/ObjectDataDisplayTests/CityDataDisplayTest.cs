using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityDataDisplayTest
{
    private GameObject root;
    private CityDataHandler cityData;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("CityDataHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        cityData = root.AddComponent<CityDataHandler>();

        SetPrivateField("ID_Text", CreateText("ID_Text"));
        SetPrivateField("Satisfaction_Text", CreateText("Satisfaction_Text"));
        SetPrivateField("Needs_Text", CreateText("Needs_Text"));

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
        cityData.ID = 10;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("ID_Text").text, "10");
    }

    [Test]
    public void Satisfaction_WhenValueInBounds_UpdatesValue()
    {
        cityData.Satisfaction = 80;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Satisfaction_Text").text, "80%");
    }

    [Test]
    public void Satisfaction_WhenValueOutOfBounds_ThrowException()
    {
        Assert.Throws<Exception>(() => cityData.Satisfaction = -10);
        Assert.Throws<Exception>(() => cityData.Satisfaction = 110);
    }

    [Test]
    public void Needs_UpdatesStringList()
    {
        List<string> ls = new List<string> { "Milk", "Steel", "Wood" };
        cityData.Needs = new List<string>(ls);
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Needs_Text").text, string.Join(",", ls));
    }

    [Test]
    public void SetButtonsActive_WhenTrue_CloseIsActive()
    {
        cityData.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Close_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenFalse_CloseIsInactive()
    {
        cityData.SetButtonsActive(false);

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
        typeof(CityDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(cityData, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(CityDataHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(cityData);
    }
}