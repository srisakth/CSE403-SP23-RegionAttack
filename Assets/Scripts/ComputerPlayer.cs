using System;
using System.Collections.Generic;

public class ComputerPlayer : Player
{
    private Random random = new Random();
    public ComputerPlayer(int id,Game game) : base(id,game)
	{
		
	}
	public ((int, int), int) findPrimeMove(int prime) {
		for (int i = 0; i < game.getDim() * game.getDim(); i++)
		{
			int x = random.Next(0, game.getDim() / 2);
			int y = random.Next(0, game.getDim());

			if (game.IsValid((x, y), prime)>0)
			{
				return ((x, y), prime);
			}
		}
		return ((-1, -1), 0);
    }
	public List<int> haveMulDiv(int num)
	{
        List<int> posNums = new List<int> { };
        for (int i = 0; i < numberPool.Count; i++)
		{
			if (num % numberPool[i] == 0 || numberPool[i] % num == 0) {
				 posNums.Add(numberPool[i]);
			}
		}
		return posNums;
	}
	public ((int, int), int) findMove() {
        //Try to extend current region
        for (int i = 0; i < game.getDim(); i++)
        {
            for (int j = 0; j < game.getDim(); j++)
            {
				if (game.isPlayersNumber((i, j), false)) {
					List<int> nums = haveMulDiv(game.board[i, j].Item1);
					for (int l = 0; l < nums.Count; l++) {
						int num = nums[l];
                        if (game.IsValid((i + 1, j), num)>0) return ((i + 1, j), num);
                        if (game.IsValid((i - 1, j), num)>0) return ((i - 1, j), num);
                        if (game.IsValid((i, j + 1), num)>0) return ((i, j + 1), num);
                        if (game.IsValid((i + 1, j - 1), num)>0) return ((i, j - 1), num);
                    }
                }
            }
        }

        //Find move prime move
        for (int i = 0; i < numberPool.Count; i++) {
			if (game.isPrime(numberPool[i])) {
                ((int, int), int) move = findPrimeMove(numberPool[i]);
				if (move.Item2 != 0) {
					return move;
				}
			}
		}

		return ((-1,-1),0);
	} 
}

