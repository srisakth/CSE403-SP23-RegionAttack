using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{

    [Test]
    public void TestMovePossible()
    {
        int numGames = 10;
        int numMoves = 50;
        for (int j = 0; j < numGames; j++)
        {
            Game game = new Game(6, false);
            game.isP1Turn = true;
            for (int l = 0; l < numMoves; l++)
            {
                List<((int, int), int)> pm1 = game.p1.PossibleMoves();
                for (int i = 0; i < pm1.Count; i++)
                {
                    Assert.True(game.IsValid(pm1[i].Item1, pm1[i].Item2, true) > 0);
                }
                Assert.True(pm1.Count > 0);
                Assert.True(game.MakeMove(pm1[0].Item1, pm1[0].Item2) > 0);

                List<((int, int), int)> pm2 = game.p2.PossibleMoves();
                Assert.True(pm2.Count > 0);
                for (int i = 0; i < pm2.Count; i++)
                {
                    Assert.True(game.IsValid(pm2[i].Item1, pm2[i].Item2, false) > 0);
                }
                Assert.True(game.MakeMove(pm2[0].Item1, pm2[0].Item2) > 0);
            }
        }
    }
}
