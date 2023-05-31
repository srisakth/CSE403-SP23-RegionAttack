using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ComputerPlayerTests
{
    [Test]
    public void ComPlayerTestsSimplePasses()
    {
        for (int numGams = 0; numGams < 10; numGams++)
        {
            GameOption gameOption = new GameOption(6, true, GameOption.Mode.computerBasic);
            Game game = new Game(gameOption);
            game.ResetGameBoard();
            ComputerPlayer cp = (ComputerPlayer)game.p2;
            // Check if moves are valid
            game.isP1Turn = false;
            for (int i = 0; i < 100; i++)
            {
                ((int, int), int) m = cp.FindMove();
                if (m.Item2 != 0)
                {
                    Assert.True(game.IsValid(m.Item1, m.Item2, game.isP1Turn) > 0);
                }

            }
        }
    }
    [Test]
    public void AdvancedComPlayerTests()
    {
        for (int numGams = 0; numGams < 10; numGams++)
        {
            GameOption gameOption = new GameOption(6,true,GameOption.Mode.computerAdvanced);
            Game game = new Game(gameOption);
            game.ResetGameBoard();
            ComputerPlayerAdvanced cp = (ComputerPlayerAdvanced)game.p2;
            // Check if moves are valid
            game.isP1Turn = false;
            for (int i = 0; i < 100; i++)
            {
                ((int, int), int) m = cp.FindMove();
                if (m.Item2 != 0)
                {
                    Assert.True(game.IsValid(m.Item1, m.Item2, game.isP1Turn) > 0);
                }

            }
        }
    }
}