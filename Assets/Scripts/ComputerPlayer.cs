using System;
using System.Collections.Generic;

public class ComputerPlayer : Player
{
    private System.Random random = new System.Random();
    public ComputerPlayer(int id,Game game) : base(id,game)
	{
		
	}
	// The FindMove method computes a specific move that the Computer Player wants to play as the next move.
	public ((int, int), int) FindMove() {
		List<((int, int), int)> posMoves = PossibleMoves();
		int randMove = random.Next(0, posMoves.Count);
		return posMoves[randMove];
	} 
}

