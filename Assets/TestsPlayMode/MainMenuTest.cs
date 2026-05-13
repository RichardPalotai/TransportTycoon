using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTest
{
    private GameObject root;
    private MainMenuHandler menu;
    private MessageHandler msgDisp;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("MainMenuHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        menu = root.AddComponent<MainMenuHandler>();
        msgDisp = root.AddComponent<MessageHandler>();

        SetPrivateField(menu, "NewGame_btn", CreateButton("NewGame_btn"));
        SetPrivateField(menu, "LoadGame_btn", CreateButton("LoadGame_btn"));
        SetPrivateField(menu, "GameSlot_dropdown", CreateDropdown("SlotSelector_btn"));
        SetPrivateField(menu, "QuitGame_btn", CreateButton("QuitGame_btn"));

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
    public void IsThereSave_WhenSaveExists_ReturnsTrue()
    {
        SaveFilesToTemp();

        var game = new Game();
        game.NewGame(new DataAccess());
        game.SaveGame();
        Assert.IsTrue(menu.IsThereSave);

        RecoverFilesToSaves();
    }

    [Test]
    public void IsThereSave_WhenNoSave_ReturnsFalse()
    {
        SaveFilesToTemp();

        var game = new Game();
        game.NewGame(new DataAccess());
        Assert.IsFalse(menu.IsThereSave);

        RecoverFilesToSaves();
    }

    [Test]
    public void SaveNames_WhenSaveExists_ReturnsStringList()
    {
        SaveFilesToTemp();

        var game = new Game();
        game.NewGame(new DataAccess());
        game.SaveGame();
        Assert.IsNotEmpty(menu.SaveNames);

        RecoverFilesToSaves();
    }

    [Test]
    public void SaveNames_WhenNoSave_ReturnsEmptyList()
    {
        SaveFilesToTemp();

        var game = new Game();
        game.NewGame(new DataAccess());
        List<string> saves;
        Assert.Throws<Exception>(() => saves = menu.SaveNames);

        RecoverFilesToSaves();
    }

    [Test]
    public void GetSaves_WhenSaveExists_ReturnsNameAndDate()
    {
        SaveFilesToTemp();

        var game = new Game();
        game.NewGame(new DataAccess());
        game.SaveGame();
        Assert.IsNotEmpty(menu.SaveNames);

        RecoverFilesToSaves();
    }

    [Test]
    public void GetSaves_WhenNoSuchSave_ReturnsEmptyList()
    {
        SaveFilesToTemp();

        var game = new Game();
        game.NewGame(new DataAccess());
        (string, DateTime) save;
        Assert.Throws<Exception>(() => save = menu.GetSave("asd"));

        RecoverFilesToSaves();
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

    private TMP_Dropdown CreateDropdown(string name)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(root.transform);

        Image image = obj.AddComponent<Image>();

        TMP_Dropdown dropdown = obj.AddComponent<TMP_Dropdown>();
        dropdown.targetGraphic = image;

        GameObject labelObj = new GameObject("-- Select Game --");
        labelObj.transform.SetParent(obj.transform);
        TextMeshProUGUI label = labelObj.AddComponent<TextMeshProUGUI>();
        dropdown.captionText = label;

        dropdown.options.Clear();
        dropdown.options.Add(new TMP_Dropdown.OptionData("-- Select Game --"));
        dropdown.value = 0;
        dropdown.RefreshShownValue();

        return dropdown;
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

    private void SaveFilesToTemp()
    {
        string savesPath = Path.Combine(Application.persistentDataPath, "saves");
        string tempPath = Path.Combine(Application.persistentDataPath, "temp_saves");
        if (Directory.Exists(savesPath))
        {
            string[] files = Directory.GetFiles(savesPath);
            if (files.Length > 0)
            {
                Directory.CreateDirectory(tempPath);
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string dest = Path.Combine(tempPath, fileName);
                    File.Copy(file, dest, true);
                }

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }
        else
        {
            Directory.CreateDirectory(savesPath);
        }
    }

    private void RecoverFilesToSaves()
    {
        string savesPath = Path.Combine(Application.persistentDataPath, "saves");
        string tempPath = Path.Combine(Application.persistentDataPath, "temp_saves");
        if (Directory.Exists(tempPath))
        {
            string[] files = Directory.GetFiles(savesPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
            files = Directory.GetFiles(tempPath);
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string dest = Path.Combine(savesPath, fileName);
                File.Copy(file, dest, true);
            }
            Directory.Delete(tempPath, recursive: true);
        }
        else
        {
            string[] files = Directory.GetFiles(savesPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
