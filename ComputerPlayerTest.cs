using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ComputerPlayerTests
{
    [Test]
    public void ComPlayerTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        Game game = new Game(6,true);
        ComputerPlayer p = new ComputerPlayer(2,game);
        game.resetGameBoard();
        // Check if moves are valid
        for (int i = 0; i < 100; i++) {
            ((int, int), int) m = p.findMove();
            if (m.Item2 != -1) {
                game.isP1Turn = false;
                Assert.True(game.IsValid(m.Item1,m.Item2)>0);
            }
            
        }
        
    }
}