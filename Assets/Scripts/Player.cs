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
    public List<int> getNumberPool() {
        return numberPool;
    }
}

