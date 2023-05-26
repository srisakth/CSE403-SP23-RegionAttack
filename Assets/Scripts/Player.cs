using System;
using System.Collections.Generic;

public class Player
{
    protected internal int id;
    int curScore;
    protected internal Game game;
    public List<int> numberPool;

    public Player(int id, Game game)
    {
        this.id = id;
        this.game = game;
        curScore = 0;
        numberPool = new List<int>();
    }
    // The HasNum method returns if the Player has the given number in it's number pool.
    public bool HasNum(int number)
    {
        return numberPool.Contains(number);
    }
    // The AddNum method adds the given number to the Players number pool.
    public void AddNum(int number)
    {
        numberPool.Add(number);
        numberPool.Sort();
    }
    // The RemoveNum method removes the given number from the Players number pool.
    public void RemoveNum(int number)
    {
        numberPool.Remove(number);
    }
    // The ReplaceNum method replaces the number in the number pool with index idx with the given number.
    public void ReplaceNum(int idx, int number)
    {
        if (idx >= numberPool.Count)
            Console.Error.WriteLine("Invalid Index");
        else
            numberPool[idx] = number;
    }
    // The GetId method returns the unique player id (1 or 2).
    public int GetId()
    {
        return id;
    }
    // The GetScore method returns the current score of the player.
    public int GetScore()
    {
        return curScore;
    }
    // The SetScore method sets the current score to the given value.
    public void SetScore(int score)
    {
        curScore = score;
    }
    // The PossibleMoves method returns a list of all possible moves that the player can currently play.
    public List<((int, int), int)> PossibleMoves()
    {
        List<((int, int), int)> allMoves = new List<((int, int), int)>();
        for (int i = 0; i < numberPool.Count; i++)
        {
            int num = numberPool[i];
            for (int l = 0; l < game.GetDim(); l++)
            {
                for (int k = 0; k < game.GetDim(); k++)
                {
                    if (game.IsValid((l, k), num, this.id == 1) > 0)
                    {
                        allMoves.Add(((l, k), num));
                    }
                }
            }
        }

        return allMoves;
    }
    // The GetNumberPool method returns the current number pool of the player.
    public List<int> GetNumberPool()
    {
        return numberPool;
    }

}

