using System;
using System.Collections.Generic;
/* 
* The Game class is an abstraction of the game state.  
*/
public class Game
{
	int initPoolSize = 4;
	int maxNumber = 12;
	int _dim;

	// Variables (Game Board, Players)
	public Player p1;
	public Player p2;
	public bool isP1Turn;

	// The board stores (1) the number stored in the board and (2) whether that number is P1's number.
	// If the number is 0, then the latter boolean does not matter.
	public (int, bool)[,] board;

	private Random random = new Random();

	public Game(int dim, bool _isOpponentAI)
	{
		p1 = new Player(1,this);
		if (_isOpponentAI)
		{
			p2 = new ComputerPlayer(2,this);
		}
		else {
			p2 = new Player(2,this);
		}

		isP1Turn = true;

		_dim = dim;
		board = new (int, bool)[dim, dim];
		for (int i = 0; i < dim; i++) {
			for (int j = 0; j < dim; j++) {
				board[i, j] = (0, IsP1Side((i, j)));
			}
		}
		genNumberPool(p1);
		genNumberPool(p2);
		// Add the initial hand

		// Decide which player is playing first
		isP1Turn = random.Next(0, 2) == 0;
	}

	// Tries to make a move for the current player with the given number.
	// If it is invalid, then returns 0.
	// If it is valid, then it updates the internal board and score accordingly and return the new number for the current player.

	public int MakeMove((int, int) position, int number) {
		int x = IsValid(position, number, isP1Turn);
        if (x <= 0)
		{
			return x;
		}
		board[position.Item1, position.Item2] = (number, isP1Turn);

		// Remove the used tile and add a new tile
		int num = 0;
		if (isP1Turn)
		{
			p1.removeNum(number);
			num = addNewNumber(p1);
		}
		else
		{
			p2.removeNum(number);
			num = addNewNumber(p2);
		}

		// Update the scores
		p1.setScore(ComputeScore(true));
		p2.setScore(ComputeScore(false));

		// Switch turns
		isP1Turn = !isP1Turn;

		// Regenerate Numbers if no moves are possible
		if (isP1Turn) reGenNumberPool(p1);
		else reGenNumberPool(p2);
		return num;
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

	public bool isPrime(int number)
	{
		int[] sprimes = new int[] { 2, 3, 5, 7, 11 };
		return Array.Exists(sprimes, element => element == number);
	}

	public bool isDivMul((int, int) position, int number)
	{
		if (validPosition(position) && isPlayersNumber(position, isP1Turn))
		{
			int num = board[position.Item1, position.Item2].Item1;
			return number %  num == 0 || num % number == 0;
		}else{
			return false;
		}
		
	}

	private bool isCompatible((int, int) position, int number, bool isP1)
	{
		if (!validPosition(position) || !isPlayersNumber(position, isP1)){
			return true;
		}else if (isDivMul(position, number)) {
			return true;
		}else{
			return false;
		}
	}

	public int IsValid((int, int) position, int number, bool isP1) {
		//Check current value of cell:
		if (!validPosition(position))
		{
			return 0;	//invalid position (outside board)
		}
		//Oponent larger number
		if (!isPlayersNumber(position, isP1) && board[position.Item1, position.Item2].Item1 >= number)
		{
			return -1;	//position contains larger or equal oponent number
		}
        //Check conflict with adjacent numbers
        bool[] comp = new bool[4];
		comp[0] = isCompatible((position.Item1 - 1, position.Item2), number, isP1);
		comp[1] = isCompatible((position.Item1, position.Item2 - 1), number, isP1);
		comp[2] = isCompatible((position.Item1 + 1, position.Item2), number, isP1);
		comp[3] = isCompatible((position.Item1, position.Item2 + 1), number, isP1);
        for (int i = 0; i < 4; i++)
        {
            if (!comp[i])
            {
                return -(i + 2); // the tile corresponding to i+1 contains a conflicting number
            }
        }
        //Check multiple divisor rule
        bool[] divmul = new bool[4];
		divmul[0] = isDivMul((position.Item1 - 1, position.Item2), number);
        divmul[1] = isDivMul((position.Item1, position.Item2 - 1), number);
        divmul[2] = isDivMul((position.Item1 + 1, position.Item2), number);
        divmul[3] = isDivMul((position.Item1, position.Item2 + 1), number);
		for (int i = 0; i < 4; i++) {
			if (divmul[i]) {
				return i + 2;
			}
		}
		//Check prime rule
		if (isPrime(number))
		{
			if (IsP1Side(position) == isP1Turn)
			{
				return 6;   //prime rule correct
			}
			else
			{
				return -6;  //prime wrong side
			}
		}
		else {
			return -7; // not a prime and no extension
		}
	}

	protected internal void fillOutside((int,int) start, bool[,] outside,bool[,] curReg) {
		if(validPosition(start) && !curReg[start.Item1,start.Item2] && !outside[start.Item1,start.Item2]){
			outside[start.Item1,start.Item2] = true;
			fillOutside((start.Item1-1,start.Item2),outside,curReg);
			fillOutside((start.Item1,start.Item2-1),outside,curReg);
			fillOutside((start.Item1+1,start.Item2),outside,curReg);
			fillOutside((start.Item1,start.Item2+1),outside,curReg);
		}
	}
	// Marks all tiles as true, which are contained in the region containing start
    protected internal void markRegion((int, int) start, bool isP1, bool[,] check, bool[,] curReg)
    {
        if (validPosition(start) && !check[start.Item1, start.Item2] && isPlayersNumber((start.Item1, start.Item2), isP1))
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
    private int ComputeRegionSize((int, int) position, bool isP1, bool[,] check)
	{
		int regSize = 0;
        bool[,] curReg = new bool[_dim, _dim];
		markRegion(position, isP1, check, curReg);
		bool[,] outside = new bool[_dim,_dim];
		for(int i = 0; i < _dim; i++){
			fillOutside((i,0),outside,curReg);
			fillOutside((0,i),outside,curReg);
			fillOutside((i,_dim),outside,curReg);
			fillOutside((_dim,i),outside,curReg);
		}
        for (int i = 0; i < _dim; i++){
            for (int j = 0; j < _dim; j++){
				if (!outside[i, j]) regSize++;
            }
        }
        return regSize;
	}
	public void reGenNumberPool(Player p) {
		while (p.PossibleMoves().Count == 0){
            p.numberPool = new List<int>();
            for (int i = 0; i < initPoolSize; i++)
			{
				p.addNum(random.Next(1, maxNumber + 1));
			}
		}
    }
	public void genNumberPool(Player p) {
        for (int i = 0; i < initPoolSize; i++)
        {
            p.addNum(random.Next(1, maxNumber + 1));
        }
		while (!p.getNumberPool().Exists(x => isPrime(x))) {
			p.getNumberPool().RemoveAt(0);
			p.addNum(random.Next(1, maxNumber + 1));
		}
    }
	public int addNewNumber(Player p) {
		int x = 0;
        if (p.PossibleMoves().Count > 0){
			x = random.Next(1, maxNumber + 1);
		}else { 
			x = random.Next(1,maxNumber+1);
			while (PossibleMoves(x,p.id==1).Count == 0)
			{
				x = random.Next(1, maxNumber + 1);
			}
        }
		p.addNum(x);
		return x;
    }
	// Computes score of given player
    public int ComputeScore(bool isP1)
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
	// Returns the possible moves of the given player given a number.
	public List<(int,int)> PossibleMoves(int number, bool isP1) {
        List<(int,int)> moves = new List<(int,int)>();
        for(int i = 0; i < _dim; i++){
			for (int j = 0; j < _dim; j++) {
                if(IsValid((i,j),number,isP1)>0){
                    moves.Add((i,j));
                }
            }
        }
		return moves;
    }

	public int getNumberPoolSize()
	{
		return initPoolSize;
	}

	public int getMaxNumber()
	{
		return maxNumber;
	}

	public int getDim()
	{
		return _dim;
	}

	// Helper functions
	bool validPosition((int, int) position)
	{
		return position.Item1 >= 0 && position.Item1 < _dim && position.Item2 >= 0 && position.Item2 < _dim;
	}

	// Helper function to return whether the grid at that coordinate is player 1's grid
	bool IsP1Side((int, int) position)
	{
		// For now, we can just set the lower half as P1's but we can eventually have different configurations
		return position.Item2 >= _dim / 2;
	}

	// Helper function returns if a certain position contains a number of the given player
	public bool isPlayersNumber((int, int) position, bool isP1)
	{
		if (!validPosition(position)) return false;
		return board[position.Item1, position.Item2].Item1 != 0 && board[position.Item1, position.Item2].Item2 == isP1;
	}
	public void resetGameBoard() {
		this.board = new (int,bool)[_dim,_dim];
	}
}
