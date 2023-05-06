using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTests
{

    bool[] turns = new bool[]{true, false};
    int dim = 6;
    System.Random random = new System.Random();
    // A Test  as an ordinary method
    
    [Test]
    public void IsValidTestsBasic()
    {
        Game game = new Game(dim, false);
        // Test invalid position
        Assert.AreEqual(0, game.IsValid((-1,-1), 0));
        Assert.AreEqual(0, game.IsValid((dim, 0), 0));
        Assert.AreEqual(0, game.IsValid((0, dim), 0));
        Assert.AreEqual(0, game.IsValid((dim, dim), 0));

        // Test opponent larger number
        for(int i = 0; i < turns.Length; i++){
            game.board[0,0] = (5,turns[i]);
            game.isP1Turn = !turns[i];
            Assert.AreEqual(-1,game.IsValid((0,0),3));
            game.board[1,1] = (12,turns[i]);
            game.isP1Turn = !turns[i];
            Assert.AreEqual(-1,game.IsValid((1,1),7));
        }

        // Test conflict left, up, right, down (multiple ones)
        game.resetGameBoard();
        for(int i = 0; i < turns.Length; i++){
            game.isP1Turn = turns[i];
            game.board[1,0] = (3,turns[i]);
            game.board[3,0] = (4,turns[i]);
            game.board[0,2] = (7,turns[i]);
            game.board[4,3] = (9,turns[i]);
            Assert.AreEqual(-2,game.IsValid((1,2),2));
            Assert.AreEqual(-3,game.IsValid((3,1),6));
            Assert.AreEqual(-4,game.IsValid((0,0),2));
            Assert.AreEqual(-5,game.IsValid((4,2),4));
        }

        // Test mult/div up, down, left, right (multiple ones) 
        game.resetGameBoard();
        for(int i = 0; i < turns.Length; i++){
            game.isP1Turn = turns[i];
            game.board[1,0] = (3,turns[i]);
            game.board[3,0] = (4,turns[i]);
            game.board[0,2] = (7,turns[i]);
            game.board[4,3] = (9,turns[i]);
            Assert.AreEqual(2,game.IsValid((1,2),7));
            Assert.AreEqual(3,game.IsValid((3,1),8));
            Assert.AreEqual(4,game.IsValid((0,0),6));
            Assert.AreEqual(5,game.IsValid((4,2),3)); 
        }

        // Test prime right side
        game.resetGameBoard();
        int[] primes = new int[]{2,3,5,7,11};
        game.isP1Turn = true;
        int boarder = 3; //should be Math.Ceiling(dim/2)
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(boarder,dim);
            Assert.AreEqual(6,game.IsValid((x,y), primes[i]));
        }
        game.isP1Turn = false;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(0,boarder);
            Assert.AreEqual(6,game.IsValid((x,y), primes[i]));
        }

        // Test prime wrong side
        game.resetGameBoard();
        game.isP1Turn = true;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(0,boarder);
            Assert.AreEqual(-6,game.IsValid((x,y), primes[i]));
        }
        game.isP1Turn = false;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(boarder,dim);
            Assert.AreEqual(-6,game.IsValid((x,y), primes[i]));
        }
        // Test not prime and not extension rule
        int[] nprimes = new int[]{4,6,8,9,12};
        for(int j = 0; j < turns.Length; j++){
            for(int i = 0; i < nprimes.Length; i++){
                int x = random.Next(0,dim);
                int y = random.Next(0,dim);
                Assert.AreEqual(-7,game.IsValid((x,y), nprimes[i]));
            }
        }
        
    }
    [Test]
    public void IsValidTestsAdvanced()
    {
        // Extension rule advances (multiple neighbors)
        Game game = new Game(dim, false);
        game.resetGameBoard();
        game.board[2,0] = (1,true);
        game.board[3,0] = (1,false);
        game.board[4,0] = (1,false);
        game.board[1,1] = (2,true);
        game.board[3,1] = (7,false);
        game.board[1,2] = (12,true);
        game.board[2,2] = (4,true);
        game.board[1,3] = (3,true);
        game.board[3,3] = (7,false);
        game.board[1,4] = (1,true);
        game.board[2,4] = (6,true);
        game.board[4,4] = (11,false);

        game.isP1Turn = true;
        Assert.AreEqual(4,game.IsValid((1,0),4));
        Assert.AreEqual(2,game.IsValid((2,1),8));
        Assert.AreEqual(2,game.IsValid((2,3),12));
        game.isP1Turn = false;
        Assert.AreEqual(2,game.IsValid((4,1),7));
        Assert.AreEqual(3,game.IsValid((3,2),1));
        Assert.AreEqual(3,game.IsValid((4,5),11));
    }
    [Test]
    public void CountScoreTestsBasic() {
        Game game = new Game(dim, false);
        
        game.resetGameBoard();
        game.board[1,1] = (7, false);
        game.board[1,2] = (1, false);
        game.board[1,4] = (1, true);
        game.board[2,4] = (4, true);
        game.board[3,4] = (8, true);
        Assert.AreEqual(2,game.ComputeScore(false));
        Assert.AreEqual(3,game.ComputeScore(true));
        game.board[2,1] = (9, false);
        game.board[3,5] = (2, true);
        Assert.AreEqual(3,game.ComputeScore(false));
        Assert.AreEqual(4,game.ComputeScore(true));

        game.resetGameBoard();
        game.board[0,0] = (1,false);
        game.board[5,0] = (1,false);
        game.board[2,2] = (2,false);
        game.board[3,2] = (4,false);
        game.board[0,5] = (1,true);
        game.board[5,5] = (1,true);
        game.board[2,3] = (8,true);
        game.board[3,3] = (8,true);
        Assert.AreEqual(2,game.ComputeScore(false));
        Assert.AreEqual(2,game.ComputeScore(true));
    }
    [Test]
    public void CountScoreTestsAdvanced() {
        Game game = new Game(dim, false);
        (int,int)[] poss1 = new (int,int)[] {(0,0),(1,0),(2,0),(2,1),(2,2),(1,2),(0,2),(0,1)};
        for(int i = 0; i < poss1.Length; i++){
            game.board[poss1[i].Item1,poss1[i].Item2] = (1,false);
        }
        Assert.AreEqual(9,game.ComputeScore(false));
        game.resetGameBoard();
        (int,int)[] poss2 = new (int,int)[] {(0,0),(1,0),(2,0),(3,0),(3,1),(3,2),(2,2),(1,2),(0,2),(0,1)};
        for(int i = 0; i < poss2.Length; i++){
            game.board[poss2[i].Item1,poss2[i].Item2] = (1,false);
        }
        Assert.AreEqual(12,game.ComputeScore(false));
        (int,int)[] poss3 = new (int,int)[] {(0,0),(1,0),(2,0),(3,0),(4,0),(0,1),(0,2),(0,3),(0,4),(1,3),(1,4),(3,1),(4,1),(2,2),(3,2),(2,3)};
        for(int i = 0; i < poss3.Length; i++){
            game.board[poss3[i].Item1,poss3[i].Item2] = (1,false);
        }
        Assert.AreEqual(19,game.ComputeScore(false));
    }
}
