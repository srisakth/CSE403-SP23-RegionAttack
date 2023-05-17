using System;
using System.Collections.Generic;

public class ComputerPlayer : Player
{
    private System.Random random = new System.Random();
    public ComputerPlayer(int id,Game game) : base(id,game)
	{
		
	}
	public ((int, int), int) findMove() {
		List<((int, int), int)> posMoves = PossibleMoves();
		int randMove = random.Next(0, posMoves.Count);
		return posMoves[randMove];
	} 
}

