using System;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarHandlerTests
{
    private GameObject root;
    private MenuBarHandler menu;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("MenuBarHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        menu = root.AddComponent<MenuBarHandler>();

        SetPrivateField("AccountBalance_text", CreateText("AccountBalance"));
        SetPrivateField("CalendarTime_text", CreateText("CalendarTime"));
        SetPrivateField("Time_text", CreateText("Time"));

        SetPrivateField("Pause_btn", CreateButton("Pause"));
        SetPrivateField("Play_btn", CreateButton("Play"));
        SetPrivateField("Forward_btn", CreateButton("Forward"));
        SetPrivateField("FastForward_btn", CreateButton("FastForward"));

        // Now Awake/Start can run safely
        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void AccountBalance_WhenSetPositive_UpdatesText()
    {
        menu.AccountBalance = 500;

        Assert.AreEqual(500, menu.AccountBalance);
    }

    [Test]
    public void AccountBalance_WhenSetNegative_ThrowsException()
    {
        Assert.Throws<Exception>(() =>
        {
            menu.AccountBalance = -1;
        });
    }

    [Test]
    public void CalendarTime_WhenSet_UpdatesValue()
    {
        DateTime date = new DateTime(2026, 4, 16);

        menu.CalendarTime = date;

        Assert.AreEqual(date.Date, menu.CalendarTime.Date);
    }

    [Test]
    public void Time_WhenSet_UpdatesTimeText()
    {
        DateTime time = new DateTime(2026, 4, 16, 14, 30, 15);

        menu.Time = time;

        TextMeshProUGUI timeText = GetPrivateField<TextMeshProUGUI>("Time_text");
        Assert.AreEqual("14:30:15", timeText.text);
    }

    [Test]
    public void SetButtonsActive_WhenFalse_DisablesAllButtons()
    {
        menu.SetButtonsActive(false);

        Assert.IsFalse(GetPrivateField<Button>("Pause_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Play_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("Forward_btn").interactable);
        Assert.IsFalse(GetPrivateField<Button>("FastForward_btn").interactable);
    }

    [Test]
    public void SetButtonsActive_WhenTrue_EnablesAllButtons()
    {
        menu.SetButtonsActive(true);

        Assert.IsTrue(GetPrivateField<Button>("Pause_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Play_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("Forward_btn").interactable);
        Assert.IsTrue(GetPrivateField<Button>("FastForward_btn").interactable);
    }

    [Test]
    public void SelectPlayButton_ChangesPlayButtonColor()
    {
        Button playButton = GetPrivateField<Button>("Play_btn");

        menu.SelectPlayButton();

        Assert.AreEqual(Color.lightGray, playButton.image.color);
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
        typeof(MenuBarHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(menu, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(MenuBarHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(menu);
    }
}