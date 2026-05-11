using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplayTest
{
    private GameObject root;
    private GameOverHandler gameoverDisp;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("GameOverHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        gameoverDisp = root.AddComponent<GameOverHandler>();

        // Now Awake/Start can run safely
        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void ToggleGameOverScreen_SwitchesGameObectActivity()
    {
        Assert.IsFalse(gameoverDisp.gameObject.activeSelf);
        gameoverDisp.ToggleGameOverScreen();
        Assert.IsTrue(gameoverDisp.gameObject.activeSelf);
        gameoverDisp.ToggleGameOverScreen();
        Assert.IsFalse(gameoverDisp.gameObject.activeSelf);
    }
}
