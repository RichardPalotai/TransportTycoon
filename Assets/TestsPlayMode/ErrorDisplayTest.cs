using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDisplayTest
{
    private GameObject root;
    private MessageHandler msgDisp;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("ErrorHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        msgDisp = root.AddComponent<MessageHandler>();

        SetPrivateField(msgDisp, "Close_btn", CreateButton("Close_btn"));
        SetPrivateField(msgDisp, "MessageTag_txt", CreateText("MessageTag_text"));
        SetPrivateField(msgDisp, "Message_txt", CreateText("Message_text"));

        // Now Awake/Start can run safely
        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void MessageTag_WhenSet_UpdatesMessageTag()
    {
        string errTag = "Button Error";
        msgDisp.MessageTag = errTag;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("MessageTag_txt").text, errTag);
    }
    [Test]
    public void ErrorText_WhenSet_UpdatesErrorText()
    {
        string errText = "Button cannot be pressed";
        msgDisp.MessageText = errText;
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Message_txt").text, errText);
    }
    [Test]
    public void DisplayError_WhenSet_UpdatesTextAndTagAndSetsGameObjectActive()
    {
        string errTag = "Button Error";
        string errText = "Button cannot be pressed";
        msgDisp.DisplayError(errTag, errText);
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("MessageTag_txt").text, errTag);
        Assert.AreEqual(GetPrivateField<TextMeshProUGUI>("Message_txt").text, errText);
        Assert.IsTrue(msgDisp.gameObject.activeSelf);
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
        return (T)typeof(MessageHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(msgDisp);
    }
}
