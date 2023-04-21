using System;
using static UnityEngine.Rendering.DebugUI;

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

	// Random number generator for hand generation
    private Random RND = new Random();


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
			p1.addNum(RND.Next(1, maxNumber + 1));
			p2.addNum(RND.Next(1, maxNumber + 1));
		}

		// Decide which player is playing first
		isP1Turn = RND.Next(0, 2) == 0;

		if (isP1Turn)
            p1.addNum(RND.Next(1, maxNumber + 1));
		else
			p2.addNum(RND.Next(1, maxNumber + 1));

        Console.WriteLine("Game was created!");
	}

	// Tries to make a move for the current player with the given number.
	// If it is invalid, then returns false.
	// If it is valid, then it updates the internal board and score accordingly
	public bool MakeMove((int,int) position, int number) {
		if (!IsValid(position, number))
		{
			Console.WriteLine("This move is not valid!");
			return false;
		}
		board[position.Item1, position.Item2] = (number, isP1Turn);
		
		// Update the scores
		p1.setScore(ComputeScore(true));
		p2.setScore(ComputeScore(false));

		// Switch turns
		isP1Turn = !isP1Turn;

		return true;
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

