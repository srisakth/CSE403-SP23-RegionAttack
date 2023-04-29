using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTests
{
    // A Test  as an ordinary method
    [Test]
    public void IsValidTests()
    {
        int dim = 6;
        Game game = new Game(dim, false);
        (int, bool)[,] board = new (int, bool)[dim,dim];
        game.setGameBoard(board);
        Assert.AreEqual(0, game.IsValid((-1,-1),0));
        // Use the Assert class to test conditions
    }
}
