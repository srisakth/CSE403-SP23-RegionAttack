using System;

/* 
 * The Game class is an abstraction of the game state.  
 */
public class Game
{
	int initPoolSize = 4;
	int maxNumber = 12;

	// Variables (Game Board, Players)
	protected internal Player p1;
	protected internal Player p2;
	protected internal bool isP1Turn;

	// The board stores (1) the number stored in the board and (2) whether that number is P1's number.
	// If the number is 0, then the latter boolean does not matter.
    protected internal (int, bool)[,] board;

	private Random random = new Random();

    public Game(int dim, bool _isOpponentAI)
	{
        p1 = new Player(1);
		p2 = new Player(2);

		// Initialize the board
		board = new(int, bool)[dim,dim];
		for (int i = 0; i < dim; i++) {
			for (int j = 0; j < dim; j++) {
				board[i, j] = (0, true);
			}
		}

		// Add the initial hand
		for (int i = 0; i < initPoolSize; i++)
		{
			p1.addNum(random.Next(1, maxNumber + 1));
			p2.addNum(random.Next(1, maxNumber + 1));
		}

		// Decide which player is playing first
		isP1Turn = random.Next(0, 2) == 0;

		if (isP1Turn)
            p1.addNum(random.Next(1, maxNumber + 1));
		else
			p2.addNum(random.Next(1, maxNumber + 1));
    }

	// Tries to make a move for the current player with the given number.
	// If it is invalid, then returns 0.
	// If it is valid, then it updates the internal board and score accordingly and return the new number for the next player.

	public int MakeMove((int,int) position, int number) {
		if (!IsValid(position, number))
		{
			return 0;
		}
		board[position.Item1, position.Item2] = (number, isP1Turn);

		int newNum = random.Next(1, maxNumber + 1);
		// Remove the used tile and add a new tile
		if (isP1Turn)
		{
			p1.removeNum(number);
			p2.addNum(random.Next(1, maxNumber + 1));
		}
		else
		{
			p2.removeNum(number);
			p1.addNum(random.Next(1, maxNumber + 1));
		}
		
		// Update the scores
		p1.setScore(ComputeScore(true));
		p2.setScore(ComputeScore(false));

		// Switch turns
		isP1Turn = !isP1Turn;

		return newNum;
	}
	public String TerminateGame() {
		if (p1.getScore() > p2.getScore())
		{
			return "P1 won!";
		}
		else if (p2.getScore() > p1.getScore())
		{
			return "P2 won!";
		}
		else {
			return "Tie!";
		}
	}
	protected internal bool IsValid((int, int) position, int number) {
		return true;
    }
    protected internal int ComputeScore(bool isP1) {
		return 0;
	}

	public int getNumberPoolSize() {
		return initPoolSize;
	}
	public int getMaxNumber() {
		return maxNumber;
	}
}

