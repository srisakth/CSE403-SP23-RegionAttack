using System;
using System.Collections.Generic;
/* 
* The Game class is an abstraction of the game state.  
*/
public class Game
{
	int _initPoolSize = 4;
	int _maxNumber = 12;
	int _dim;
	GameOption gameOption = null;
	// Variables (Game Board, Players)
	public Player p1;
	public Player p2;
	public bool isP1Turn;

	// The board stores (1) the number stored in the board and (2) whether that number is P1's number.
	// If the number is 0, then the latter boolean does not matter.
	public (int, bool)[,] board;

	private Random random = new Random();
    public Game(int dim, bool computerOpponent)
    {
        p1 = new Player(1, this);
        if (!computerOpponent){
            p2 = new Player(2, this);

        }else{
            p2 = new ComputerPlayer(2, this);
        }
        _dim = dim;
        board = new (int, bool)[dim, dim];
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                board[i, j] = (0, IsP1Side((i, j)));
            }
        }
        // Add the initial hand
        GenNumberPool(p1);
        GenNumberPool(p2);
        
        // Decide which player is playing first
        isP1Turn = random.Next(0, 2) == 0;
    }
    public Game(GameOption gameOption)
	{
		this.gameOption = gameOption;
		p1 = new Player(1,this);
		if (gameOption.mode == GameOption.Mode.local || gameOption.mode == GameOption.Mode.online){
			p2 = new Player(2, this);
		}else if (gameOption.mode == GameOption.Mode.computerBasic){
			p2 = new ComputerPlayer(2, this);
		}else if (gameOption.mode == GameOption.Mode.computerAdvanced) {
			p2 = new ComputerPlayerAdvanced(2, this);
		}
		_dim = gameOption.dim;
		board = new (int, bool)[_dim, _dim];
		for (int i = 0; i < _dim; i++) {
			for (int j = 0; j < _dim; j++) {
				board[i, j] = (0, IsP1Side((i, j)));
			}
		}
        // Add the initial hand
        GenNumberPool(p1);
		GenNumberPool(p2);
		
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
			p1.RemoveNum(number);
			num = AddNewNumber(p1);
		}
		else
		{
			p2.RemoveNum(number);
			num = AddNewNumber(p2);
		}

		// Update the scores
		p1.SetScore(ComputeScore(true));
		p2.SetScore(ComputeScore(false));

		// Switch turns
		isP1Turn = !isP1Turn;

		// Regenerate Numbers if no moves are possible
		if (isP1Turn) RegenNumberPool(p1);
		else RegenNumberPool(p2);
		return num;
	}
	// The TerminateGame method returns the winner of the Game based on the current scores of the players.
	public String TerminateGame() {
		if (p1.GetScore() > p2.GetScore())
		{
			return "P1 won!";
		}
		else if (p2.GetScore() > p1.GetScore())
		{
			return "P2 won!";
		}
		else {
			return "Tie!";
		}
	}
	// The IsPrime method returns if the given number is a prime.
	public bool IsPrime(int number)
	{
		int[] sprimes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};
		return Array.Exists(sprimes, element => element == number);
	}
	// The IsDivMul method returns if the given position contains a number of the given player that is a multiple or a divisor of the given number.
	public bool IsDivMul((int, int) position, int number, bool isP1)
	{
		if (ValidPosition(position) && IsPlayersNumber(position, isP1))
		{
			int num = board[position.Item1, position.Item2].Item1;
			return number %  num == 0 || num % number == 0;
		}else{
			return false;
		}
		
	}
	// The IsCompatible method returns if the given position conflicts with the given number of the given player.
	private bool IsCompatible((int, int) position, int number, bool isP1)
	{
		if (!ValidPosition(position) || !IsPlayersNumber(position, isP1)){
			return true;
		}else if (IsDivMul(position, number,isP1)) {
			return true;
		}else{
			return false;
		}
	}
	// The IsValid method computes the if the given move (position,number) is a valid move for the given player according to the game rules.
	public int IsValid((int, int) position, int number, bool isP1) {
		//Check current value of cell:
		if (!ValidPosition(position))
		{
			return 0;	//invalid position (outside board)
		}
		//Oponent larger number
		if (!IsPlayersNumber(position, isP1) && board[position.Item1, position.Item2].Item1 >= number)
		{
			return -1;	//position contains larger or equal oponent number
		}
        //Check conflict with adjacent numbers
        bool[] comp = new bool[4];
		comp[0] = IsCompatible((position.Item1 - 1, position.Item2), number, isP1);
		comp[1] = IsCompatible((position.Item1, position.Item2 - 1), number, isP1);
		comp[2] = IsCompatible((position.Item1 + 1, position.Item2), number, isP1);
		comp[3] = IsCompatible((position.Item1, position.Item2 + 1), number, isP1);
        for (int i = 0; i < 4; i++)
        {
            if (!comp[i])
            {
                return -(i + 2); // the tile corresponding to i+1 contains a conflicting number
            }
        }
        //Check multiple divisor rule
        bool[] divmul = new bool[4];
		divmul[0] = IsDivMul((position.Item1 - 1, position.Item2), number, isP1);
        divmul[1] = IsDivMul((position.Item1, position.Item2 - 1), number, isP1);
        divmul[2] = IsDivMul((position.Item1 + 1, position.Item2), number, isP1);
        divmul[3] = IsDivMul((position.Item1, position.Item2 + 1), number, isP1);
		for (int i = 0; i < 4; i++) {
			if (divmul[i]) {
				return i + 2;
			}
		}
		//Check prime rule
		if (IsPrime(number))
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
	// The FillOutside method is a recursive helper method of the ComputeRegionSize method that marks every position that is not contained inside of the region.
	protected internal void FillOutside((int,int) start, bool[,] outside,bool[,] curReg) {
		if(ValidPosition(start) && !curReg[start.Item1,start.Item2] && !outside[start.Item1,start.Item2]){
			outside[start.Item1,start.Item2] = true;
			FillOutside((start.Item1-1,start.Item2),outside,curReg);
			FillOutside((start.Item1,start.Item2-1),outside,curReg);
			FillOutside((start.Item1+1,start.Item2),outside,curReg);
			FillOutside((start.Item1,start.Item2+1),outside,curReg);
		}
	}
	// The MarkRegion method marks all positions in the region, which is defined by the start position.
    protected internal void MarkRegion((int, int) start, bool isP1, bool[,] check, bool[,] curReg)
    {
        if (ValidPosition(start) && !check[start.Item1, start.Item2] && IsPlayersNumber((start.Item1, start.Item2), isP1))
        {
            check[start.Item1, start.Item2] = true;
            curReg[start.Item1, start.Item2] = true;
            MarkRegion((start.Item1 - 1, start.Item2), isP1, check, curReg);
            MarkRegion((start.Item1, start.Item2 - 1), isP1, check, curReg);
            MarkRegion((start.Item1 + 1, start.Item2), isP1, check, curReg);
            MarkRegion((start.Item1, start.Item2 + 1), isP1, check, curReg);
        }
    }

    // The ComputeRegionSize method computes the size of the region containing the position of given player.
    private int ComputeRegionSize((int, int) position, bool isP1, bool[,] check)
	{
		int regSize = 0;
        bool[,] curReg = new bool[_dim, _dim];
		MarkRegion(position, isP1, check, curReg);
		bool[,] outside = new bool[_dim,_dim];
		for(int i = 0; i < _dim; i++){
			FillOutside((i,0),outside,curReg);
			FillOutside((0,i),outside,curReg);
			FillOutside((i,_dim),outside,curReg);
			FillOutside((_dim,i),outside,curReg);
		}
        for (int i = 0; i < _dim; i++){
            for (int j = 0; j < _dim; j++){
				if (!outside[i, j]) regSize++;
            }
        }
        return regSize;
	}
	// The RegenNumberPool method recomputes the number pool of a player until moves are possible again.
	public void RegenNumberPool(Player p) {
		while (p.PossibleMoves().Count == 0){
            p.numberPool = new List<int>();
            for (int i = 0; i < _initPoolSize; i++)
			{
				p.AddNum(random.Next(1, _maxNumber + 1));
			}
		}
    }
	// The GenNumberPool method generates the number pool for a number and makes sure that a prime is available a the beginning of the game.
	public void GenNumberPool(Player p) {
        for (int i = 0; i < _initPoolSize; i++)
        {
            p.AddNum(random.Next(1, _maxNumber + 1));
        }
		while (!p.GetNumberPool().Exists(x => IsPrime(x))) {
			p.GetNumberPool().RemoveAt(0);
			p.AddNum(random.Next(1, _maxNumber + 1));
		}
    }
	// The AddNewNumber method adds a new number to the players number pool, such that they player is able to make a move.
	public int AddNewNumber(Player p) {
		int x = 0;
        if (p.PossibleMoves().Count > 0){
			x = random.Next(1, _maxNumber + 1);
		}else { 
			x = random.Next(1, _maxNumber+1);
			while (PossibleMoves(x,p.id==1).Count == 0)
			{
				x = random.Next(1, _maxNumber + 1);
			}
        }
		p.AddNum(x);
		return x;
    }
	// The ComputeScore method computes the current score of the given player.
    public int ComputeScore(bool isP1)
    {
		int maxReg = 0;
		bool[,] check = new bool[_dim,_dim];
		for (int i = 0; i < _dim; i++) {
			for (int j = 0; j < _dim; j++) {
				if (!check[i, j] && IsPlayersNumber((i,j), isP1)) {
					maxReg = Math.Max(maxReg, ComputeRegionSize((i,j), isP1, check));
				}
				check[i, j] = true;
			}
		}
        return maxReg;
    }
	// The PossibleMoves method returns all positions, where the given player can place the given number.
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
	// The GetNumberPoolSize method retuns the size of the players number pools.
	public int GetNumberPoolSize()
	{
		return _initPoolSize;
	}
    // The GetMaxNumber method retuns the largest number in the players number pools.
    public int GetMaxNumber()
	{
		return _maxNumber;
	}
	// The GetDim method returns the dimension of the game board.
	public int GetDim()
	{
		return _dim;
	}
	// The Valid Position method returns if the given position lies inside of the game board.
	bool ValidPosition((int, int) position)
	{
		return position.Item1 >= 0 && position.Item1 < _dim && position.Item2 >= 0 && position.Item2 < _dim;
	}

	// The IsP1Side method returns if the given position lies inside player 1's side of the game board.
	bool IsP1Side((int, int) position)
	{
		return position.Item2 >= _dim / 2;
	}

	// The IsPlayersNumber method returns if the given position contains a number of the given player.
	public bool IsPlayersNumber((int, int) position, bool isP1)
	{
		if (!ValidPosition(position)) return false;
		return board[position.Item1, position.Item2].Item1 != 0 && board[position.Item1, position.Item2].Item2 == isP1;
	}
	// The ResetGameBoard method resets the game board to the initial state.
	public void ResetGameBoard() {
		this.board = new (int,bool)[_dim,_dim];
	}
}
