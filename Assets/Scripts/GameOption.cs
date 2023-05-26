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
    int dim;
    public Mode mode;
    bool startingPlayer;
    public GameOption(int dim, bool startingPlayer, Mode mode)
	{
        this.dim = dim;
        this.mode = mode;
        this.startingPlayer = startingPlayer;
	}
    // The InitGame method returns a new Game objects that is initialized based on the current state of the GameOption.
    public Game InitGame(){
        Game game = new Game(this);
        game.isP1Turn = startingPlayer;
        return game;
    }
    // The IsOnlineGame method returns if the Game Mode is the online game mode.
    public bool IsOnlineGame(){
        return mode == Mode.online;
    }
    // The IsLocalGame method returns if the Game Mode is the local game mode.
    public bool IsLocalGame(){
        return mode == Mode.local;
    }
    // The IsComputerGame method returns if the Game Mode is a computer game mode.
    public bool IsComputerGame(){
        return mode == Mode.computerBasic || mode == Mode.computerAdvanced;
    }
    // The GetDimension method returns the dimension of that is selected.
    public int GetDimension() {
        return dim;
    }
}
