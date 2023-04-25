using System;

/* 
 * The Game class is an abstraction of the game state.  
 */
public class Game
{
	int initPoolSize = 4;
	int maxNumber = 12;
	int _dim;

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
		if (_isOpponentAI)
		{
			p2 = new ComputerPlayer(2);
		}
		else {
			p2 = new Player(2);
		}

		isP1Turn = true;

		_dim = dim;
		board = new (int, bool)[dim, dim];
		for (int i = 0; i < dim; i++) {
			for (int j = 0; j < dim; j++) {
				board[i, j] = (0, IsP1Side((i, j)));
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
	// If it is valid, then it updates the internal board and score accordingly and return the new number for the current player.

	public int MakeMove((int, int) position, int number) {
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
			p1.addNum(newNum);
		}
		else
		{
			p2.removeNum(number);
			p2.addNum(newNum);
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

	public bool isPrime(int number) {
		int[] sprimes = new int[] { 2, 3, 5, 7, 11 };
		return Array.Exists(sprimes, element => element == number);
	}
	public bool isDivMul((int, int) position, int number)
	{
		if (validPosition(position) && isPlayersNumber(position, isP1Turn))
		{
			return number % board[position.Item1, position.Item2].Item1 == 0 || number % board[position.Item1, position.Item2].Item1 == 0;
		}
		return false;
	}
	private bool isCompatible((int, int) position, int number) {
		if (!validPosition(position) || !isPlayersNumber(position,isP1Turn)) return true;
		else if (isDivMul(position, number)) return true;
		return false;
	}
	protected internal bool IsValid((int, int) position, int number) {
		//Check current value of cell: 
		//Oponent larger number
		if (board[position.Item1, position.Item2].Item1 >= number && !isPlayersNumber(position, isP1Turn))
		{
			return false;
		}
		//Check conflict with 4 adjacent numbers
		bool compt1 = isCompatible((position.Item1 - 1, position.Item2), number);
		bool compt2 = isCompatible((position.Item1, position.Item2 - 1), number);
		bool compt3 = isCompatible((position.Item1 + 1, position.Item2), number);
		bool compt4 = isCompatible((position.Item1, position.Item2 + 1), number);
		if (!(compt1 && compt2 && compt3 && compt4)) return false;
		//Check prime rule
		bool divmul1 = isDivMul((position.Item1 - 1, position.Item2), number);
		bool divmul2 = isDivMul((position.Item1, position.Item2 - 1), number);
		bool divmul3 = isDivMul((position.Item1 + 1, position.Item2), number);
		bool divmul4 = isDivMul((position.Item1, position.Item2 + 1), number);
		if (divmul1 || divmul2 || divmul3 || divmul4) return true;
        if (isPrime(number) && IsP1Side(position) != isP1Turn) return true;
        return false;
	}
	// Helper Function for fillRegion
	protected internal void fillInternalPos(bool[,] curReg, (int, int) botRightCorner)
	{
		int l = 1;
		int u = 1;
		while (botRightCorner.Item1 - l >= 0 && curReg[botRightCorner.Item1- l, botRightCorner.Item2]) l++;
        while (botRightCorner.Item2 - u >= 0 && curReg[botRightCorner.Item1, botRightCorner.Item2- u]) u++;
		int[] posl = new int[l];
		int[] posu = new int[u];
		for (int i = 0; i < l; i++) {
			while (posl[i] <= botRightCorner.Item2 && curReg[botRightCorner.Item1 - i, botRightCorner.Item2 - posl[i]]) posl[i]++;
		}
        for (int i = 0; i < u; i++)
        {
            while (posu[i] <= botRightCorner.Item1 && curReg[botRightCorner.Item1 - posu[i], botRightCorner.Item2 - i]) posu[i]++;
        }
		for (int i = 1; i < l; i++) {
			for (int j = 1; j < u; j++) {
				if (posl[i] >= j && posu[j] >= i) {
					for (int k = 1; k <= i; k++) {
						for (int m = 1; m <= j; m++) {
							curReg[botRightCorner.Item1 - k, botRightCorner.Item2 - m] = true;
						}
					}
				}
			}
		}
    }
	// Marks all enclosed tiles
	protected internal void fillRegion(bool[,] curReg) {
        for (int i = _dim-1; i >= 0; i--){
            for (int j = _dim-1; j >= 0; j--){
				if (curReg[i, j]) {
					fillInternalPos(curReg, (i, j));
				}
            }
        }
    }
	// Marks all tiles as true, which are contained in the region containing start
    protected internal void markRegion((int, int) start, bool isP1, bool[,] check, bool[,] curReg)
    {
        if (!validPosition(start)) return;
        if (!check[start.Item1, start.Item2] && isPlayersNumber((start.Item1, start.Item2), isP1))
        {
            check[start.Item1, start.Item2] = true;
            curReg[start.Item1, start.Item2] = true;
            markRegion((start.Item1 - 1, start.Item2), isP1, check, curReg);
            markRegion((start.Item1, start.Item2 - 1), isP1, check, curReg);
            markRegion((start.Item1 + 1, start.Item2), isP1, check, curReg);
            markRegion((start.Item1, start.Item2 + 1), isP1, check, curReg);
        }
    }
    // Computes size of Region containing position of given Player
    protected internal int ComputeRegionSize((int, int) position, bool isP1, bool[,] check)
	{
		int regSize = 0;
        bool[,] curReg = new bool[_dim, _dim];
		markRegion(position, isP1, check, curReg);
		fillRegion(curReg);
        for (int i = 0; i < _dim; i++){
            for (int j = 0; j < _dim; j++){
				if (curReg[i, j]) regSize++;
            }
        }
        return regSize;
	}
	// Computes score of given player
    protected internal int ComputeScore(bool isP1)
    {
		int maxReg = 0;
		bool[,] check = new bool[_dim,_dim];
		for (int i = 0; i < _dim; i++) {
			for (int j = 0; j < _dim; j++) {
				if (!check[i, j] && isPlayersNumber((i,j), isP1)) {
					maxReg = Math.Max(maxReg, ComputeRegionSize((i,j), isP1, check));
				}
				check[i, j] = true;
			}
		}
        return maxReg;
    }


	public int getNumberPoolSize() {
		return initPoolSize;
	}
	public int getMaxNumber() {
		return maxNumber;
	}

	// Helper functions
	bool validPosition((int, int) position) {
		return position.Item1 >= 0 && position.Item1 < _dim && position.Item2 >= 0 && position.Item2 < _dim;
	}
	// Helper function to return whether the grid at that coordinate is player 1's grid
	bool IsP1Side((int, int) position)
	{
		// For now, we can just set the lower half as P1's but we can eventually have different configurations
		return position.Item2 < _dim / 2;
	}
	bool isPlayersNumber((int, int) position, bool isP1) {
		return board[position.Item1, position.Item2].Item1 != 0 && board[position.Item1, position.Item2].Item2 == isP1;
	}
}
