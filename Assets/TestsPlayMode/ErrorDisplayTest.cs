using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDisplayTest
{
    private GameObject root;
    private ErrorHandler errDisp;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("ErrorHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        errDisp = root.AddComponent<ErrorHandler>();

        SetPrivateField(errDisp, "Close_btn", CreateButton("Close_btn"));
        SetPrivateField(errDisp, "ErrorTag_txt", CreateText("ErrorTag_text"));
        SetPrivateField(errDisp, "Error_txt", CreateText("Error_text"));

        // Now Awake/Start can run safely
        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void ErrorTag_WhenSet_UpdatesErrorTag()
    {
        string errTag = "Button Error";
        errDisp.ErrorTag = errTag;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("ErrorTag_txt").text, errTag);
    }
    [Test]
    public void ErrorText_WhenSet_UpdatesErrorText()
    {
        string errText = "Button cannot be pressed";
        errDisp.ErrorText = errText;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Error_txt").text, errText);
    }
    [Test]
    public void DisplayError_WhenSet_UpdatesTextAndTagAndSetsGameObjectActive()
    {
        string errTag = "Button Error";
        string errText = "Button cannot be pressed";
        errDisp.DisplayError(errTag, errText);
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("ErrorTag_txt").text, errTag);
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Error_txt").text, errText);
        Assert.IsTrue(errDisp.gameObject.activeSelf);
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

    private void SetPrivateField<TTarget, TValue>(
        TTarget target,
        string fieldName,
        TValue value)
    {
        typeof(TTarget)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(target, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(ErrorHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(errDisp);
    }
}
