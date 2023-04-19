using System;
using static UnityEngine.Rendering.DebugUI;

public class Player
{
	protected internal int id;
    protected internal Game game;
    int curScore;
	int[] numberPool;
	public Player(int id, Game game)
	{
		this.id = id;
		this.game = game;
		curScore = 0;
		CreateNumberPool();
	}
	
	private void CreateNumberPool() {
        Random rnd = new Random();
        numberPool = new int[game.getNumberPoolSize()];
		for (int i = 0; i < game.getNumberPoolSize(); i++) {
			numberPool[i] = rnd.Next(1, game.getMaxNumber() + 1);
		}
	}
    public bool hasNum(int number)
    {
        int pos = Array.IndexOf(numberPool, number);
        return pos >= 0;
    }
    public void replaceNum(int number) {
        Random rnd = new Random();
        int pos = Array.IndexOf(numberPool, number);
        if (pos < 0) Console.WriteLine("Error: number is not contained in number pool!");
        numberPool[pos] = rnd.Next(1, game.getMaxNumber() + 1);
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
}

