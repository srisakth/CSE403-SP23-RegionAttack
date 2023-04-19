using System;
public class SinglePlayerGame : Game
{
	public SinglePlayerGame(int dim) : base(dim)
	{
		this.p2 = new ComputerPlayer(2, this);
	}
}

