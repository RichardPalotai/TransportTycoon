using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTestScript
{
    [Test]
    public void NewGameTest()
    {
        var game = new Game();
        game.NewGame();

        Assert.IsNotNull(game.Map);
        Assert.IsNotNull(game.Player);
        Assert.AreEqual(System.DateTime.Today, game.CurrentTime);
        Assert.AreEqual(1.0, game.TimeScale);
    }

    [Test]
    public void PauseGameTest()
    {
        var game = new Game();
        game.NewGame();
        game.PauseGame();

        Assert.IsTrue(game.IsPaused);
    }
    [Test]
    public void ResumeGameTest()
    {
        var game = new Game();
        game.NewGame();
        game.PauseGame();
        game.ResumeGame();

        Assert.IsFalse(game.IsPaused);
        Assert.AreEqual(1.0, game.TimeScale);
    }


    [Test]
    public void GameTimeAdvanceWhenRunningTest()
    {
        var game = new Game();
        game.NewGame();
        var startTime = game.CurrentTime;
        game.UpdateGame(10);

        Assert.Greater(game.CurrentTime, startTime);
        Assert.AreEqual(startTime.AddSeconds(10), game.CurrentTime);
    }

    [Test]
    public void GameTimeAdvanceWhenStoppedTest()
    {
        var game = new Game();
        game.NewGame();
        var startTime = game.CurrentTime;
        game.PauseGame();
        game.UpdateGame(10);

        Assert.AreEqual(game.CurrentTime, startTime);
    }
}
