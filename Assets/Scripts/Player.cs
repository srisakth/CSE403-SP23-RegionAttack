using System;
public class Player
{
	readonly int id;
	int curScore;
	readonly Game game;
	public Player(int pid, Game g)
	{
		id = pid;
		game = g;
		curScore = 0;
	}
	public int getId() {
		return id;
	}
	public int getScore() {
		return curScore;
	}
	public void setScore(int score) {
		curScore = score;
	}
}

