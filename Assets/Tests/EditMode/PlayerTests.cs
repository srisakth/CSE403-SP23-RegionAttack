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
        for (int j = 0; j < 10; j++)
        {
            Game game = new Game(6, false);
            bool isP1 = game.isP1Turn;
            List<((int, int), int)> pm1 = game.p1.PossibleMoves();
            for (int i = 0; i < pm1.Count; i++)
            {
                game.IsValid(pm1[i].Item1, pm1[i].Item2, isP1);
            }
            List<((int, int), int)> pm2 = game.p2.PossibleMoves();
            for (int i = 0; i < pm2.Count; i++)
            {
                game.IsValid(pm2[i].Item1, pm2[i].Item2, !isP1);
            }
        }
    }

    [Test]
    public void TestAllMovesPossible()
    {
        Game game = new Game(6, false);

    }
}

/*    int numGames = 25;
            int numMoves = 5;
            for (int i = 0; i < numGames; i++)
            {
                Debug.Log("New Game");
                Game game = new Game(6, false);
                bool sP = game.isP1Turn;
                for (int j = 0; j < numMoves; j++)
                {
                    List<((int, int), int)> p1AllMoves = game.p1.PossibleMoves();
                    Assert.True(p1AllMoves.Count > 0);
                    int v1 = game.IsValid(p1AllMoves[0].Item1, p1AllMoves[0].Item2, sP);
                    bool turn1 = game.isP1Turn;
                    Assert.True(turn1 == sP);
                    int m1 = game.MakeMove(p1AllMoves[0].Item1, p1AllMoves[0].Item2);
                    if (m1 < 0)
                    {
                        Debug.Log(p1AllMoves[0].Item1 + " " + p1AllMoves[0].Item2 + " " + m1 + " " + sP);
                        Debug.Log(game.board[p1AllMoves[0].Item1.Item1, p1AllMoves[0].Item1.Item2 - 1]);
                    }
                    //Debug.Log(v1 + " " + turn1 + " " + m1);
                    Assert.True(m1 > 0);
                    List<((int, int), int)> p2AllMoves = game.p2.PossibleMoves();
                    Assert.True(p2AllMoves.Count > 0);
                    int v2 = game.IsValid(p2AllMoves[0].Item1, p2AllMoves[0].Item2, !sP);
                    bool turn2 = game.isP1Turn;
                    Assert.True(turn2 == !sP);
                    int m2 = game.MakeMove(p2AllMoves[0].Item1, p2AllMoves[0].Item2);
                    if (m2 < 0)
                    {
                        Debug.Log(p2AllMoves[0].Item1 + " " + p2AllMoves[0].Item2 + " " + m2 + " " + !sP);
                        Debug.Log(game.board[p2AllMoves[0].Item1.Item1, p2AllMoves[0].Item1.Item2 - 1]);
                    }
                    //Debug.Log(v2 + " " + turn2 + " " + m2);
                    Assert.True(m2 > 0);
                }
            }*/