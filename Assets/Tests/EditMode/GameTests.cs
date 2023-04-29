using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTests
{
    // A Test  as an ordinary method
    Random random = new Random();
    [Test]
    public void IsValidTestsBasic()
    {
        int dim = 6;
        Game game = new Game(dim, false);
        bool[] turns = new bool[]{true, false};

        // Test invalid position
        Assert.AreEqual(0, game.IsValid((-1,-1), 0));
        Assert.AreEqual(0, game.IsValid((dim, 0), 0));
        Assert.AreEqual(0, game.IsValid((0, dim), 0));
        Assert.AreEqual(0, game.IsValid((dim, dim), 0));

        // Test opponent larger number
        for(int i = 0; i < turns.Length; i++){
            game.board[0,0] = (5,turns[i]);
            game.isP1Turn = !turns[i];
            Assert.AreEqual(-1,game.isValid((0,0),3));
            game.board[1,1] = (12,turns[i]);
            game.isP1Turn = !turns[i];
            Assert.AreEqual(-1,game.isValid((1,1),7));
        }

        // Test conflict left, up, right, down (multiple ones)
        game.resetGameBoard();
        for(int i = 0; i < turns.Length; i++){
            game.isP1Turn = turns[i];
            game.board[0,1] = (3,turns[i]);
            game.board[0,3] = (4,turns[i]);
            game.board[2,0] = (7,turns[i]);
            game.board[4,3] = (9,turns[i]);
            Assert.AreEqual(-1,game.isValid((2,1),2));
            Assert.AreEqual(-2,game.isValid((3,1),6));
            Assert.AreEqual(-3,game.isValid((0,0),2));
            Assert.AreEqual(-4,game.isValid((4,2),4));
        }

        // Test mult/div up, down, left, right (multiple ones) 
        game.resetGameBoard();
        for(int i = 0; i < turns.Length; i++){
            game.isP1Turn = turns[i];
            game.board[0,1] = (3,turns[i]);
            game.board[0,3] = (4,turns[i]);
            game.board[2,0] = (7,turns[i]);
            game.board[4,3] = (9,turns[i]);
            Assert.AreEqual(1,game.isValid((2,1),7));
            Assert.AreEqual(2,game.isValid((3,1),8));
            Assert.AreEqual(3,game.isValid((0,0),6));
            Assert.AreEqual(4,game.isValid((4,2),3));
        }

        // Test prime right side
        game.resetGameBoard();
        int[] primes = new int[]{2,3,5,7,11};
        game.isP1Turn = true;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(Math.Ceiling(dim/2),dim);
            Assert.isEqual(5,game.IsValid((x,y), primes[i]));
        }
        game.isP1Turn = false;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(0,Math.Ceiling(dim/2));
            Assert.isEqual(5,game.IsValid((x,y), primes[i]));
        }

        // Test prime wrong side
        game.resetGameBoard();
        game.isP1Turn = true;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(0,Math.Ceiling(dim/2));
            Assert.isEqual(5,game.IsValid((x,y), primes[i]));
        }
        game.isP1Turn = false;
        for(int i = 0; i < primes.Length; i++){
            int x = random.Next(0,dim);
            int y = random.Next(Math.Ceiling(dim/2),dim);
            Assert.isEqual(-5,game.IsValid((x,y), primes[i]));
        }
        // Test not prime and not extension rule
        int[] nprimes = new int[]{4,6,8,9,12};
        for(int j = 0; j < turns.Length; j++){
            for(int i = 0; i < nprimes.Length; i++){
                int x = random.Next(0,dim);
                int y = random.Next(0,dim);
                Assert.isEqual(-6,game.IsValid((x,y), nprimes[i]));
            }
        }
        
    }
    [Test]
    public void IsValidTestsAdvanced()
    {
        
    }
    [Test]
    public void CountScoreTests() {

    }
}
