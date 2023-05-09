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
	
    public bool hasNum(int number)
    {
        return numberPool.Contains(number);
    }
    public void addNum(int number)
    {
        numberPool.Add(number);
        numberPool.Sort();
    }

    public void removeNum(int number) {
        numberPool.Remove(number);
    }

    public void replaceNum(int idx, int number) {
        if (idx >= numberPool.Count)
            Console.Error.WriteLine("Invalid Index");
        else
            numberPool[idx] = number;
    }
    public int getId()
    {
        return id;
    }
    public int getScore()
    {
        return curScore;
    }
    public void setScore(int score)
    {
        curScore = score;
    }
    public List<((int, int), int)> PossibleMoves() {
        List<((int, int), int)> allMoves = new List<((int, int), int)>();
        for (int i = 0; i < numberPool.Count; i++)
        {
            int num = numberPool[i];
            for (int l = 0; l < game.getDim(); l++) {
                for (int k = 0; k < game.getDim(); k++)
                {
                    if (game.IsValid((l, k), num, this.id == 1)>0) {
                        allMoves.Add(((l,k),num));
                    }
                }
            }
        }
        
        return allMoves;
    }
    public List<int> getNumberPool() {
        return numberPool;
    }
   
}

