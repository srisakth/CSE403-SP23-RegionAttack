using System;
using System.Collections.Generic;

public class GameOption
{
    public enum Mode
    {
        online,
        local,
        computerBasic,
        computerAdvanced
    }
    public int dim
    {
        get { return _dim; }
        set 
        { 
            if (_dim > 0)
                _dim = value;
            else
                _dim = 6;
        }
    }

    public Mode mode
    {
        get { return _mode; }
        set { _mode = value; }
    }


    bool _startingPlayer;
    private int _dim;
    private Mode _mode;

    public GameOption(int dim, bool startingPlayer, Mode mode)
	{
        this._dim = dim;
        this._mode = mode;
        this._startingPlayer = startingPlayer;
	}
    // The InitGame method returns a new Game objects that is initialized based on the current state of the GameOption.
    public Game InitGame(){
        Game game = new Game(this);
        game.isP1Turn = _startingPlayer;
        return game;
    }
    // The IsOnlineGame method returns if the Game Mode is the online game mode.
    public bool IsOnlineGame(){
        return _mode == Mode.online;
    }
    // The IsLocalGame method returns if the Game Mode is the local game mode.
    public bool IsLocalGame(){
        return _mode == Mode.local;
    }
    // The IsComputerGame method returns if the Game Mode is a computer game mode.
    public bool IsComputerGame(){
        return _mode == Mode.computerBasic || _mode == Mode.computerAdvanced;
    }
    // The GetDimension method returns the dimension of that is selected.
    public int GetDimension() {
        return dim;
    }
}
