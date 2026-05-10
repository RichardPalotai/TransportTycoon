using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTestScript
{
    [Test]
    public void NewGameTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());

        Assert.IsNotNull(game.Map);
        Assert.IsNotNull(game.Player);
        Assert.AreEqual(System.DateTime.Today, game.CurrentTime);
        Assert.AreEqual(1.0, game.TimeScale);
    }

    [Test]
    public void PauseGameTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        game.PauseGame();

        Assert.IsTrue(game.IsPaused);
    }
    [Test]
    public void ResumeGameTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        game.PauseGame();
        game.ResumeGame();

        Assert.IsFalse(game.IsPaused);
        Assert.AreEqual(1.0, game.TimeScale);
    }


    [Test]
    public void GameTimeAdvanceWhenRunningTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        var startTime = game.CurrentTime;
        game.UpdateGame(10);

        Assert.Greater(game.CurrentTime, startTime);
        Assert.AreEqual(startTime.AddSeconds(10), game.CurrentTime);
    }

    [Test]
    public void GameTimeAdvanceWhenStoppedTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        var startTime = game.CurrentTime;
        game.PauseGame();
        game.UpdateGame(10);

        Assert.AreEqual(game.CurrentTime, startTime);
    }
    [Test]
    public void GameSaveTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        int count = Game.GetSaves(new DataAccess()).Count;
        game.SaveGame();
        Assert.IsNotNull(Game.GetSaves(new DataAccess()));
        Assert.AreEqual(count, Game.GetSaves(new DataAccess()).Count);
    }
    [Test]
    public void GameGetSavesTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        game.SaveGame();
        Assert.IsNotNull(Game.GetSaves(new DataAccess()));
        Assert.IsNotEmpty(Game.GetSaves(new DataAccess()));
    }
    [Test]
    public void GameSettersTest()
    {
        var game = new Game();
        game.NewGame(new DataAccess());
        game.SaveGame();

        Assert.DoesNotThrow(() => game.Map = new Map());
        Assert.NotNull(game.Saves);
        Assert.Throws<FileNotFoundException>(() => Game.LoadGame(new DataAccess(), "asdf"));

        Car c = new(5, game.Map);
        GameEntity.ResetId();
        Car c2 = new(5, game.Map);
        Assert.AreEqual(1, c2.ID);
    }
}
