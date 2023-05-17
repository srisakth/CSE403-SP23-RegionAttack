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
    public Game InitGame(){
        Game game = new Game(this);
        game.isP1Turn = startingPlayer;
        return game;
    }
    public bool IsOnlineGame(){
        return mode == Mode.online;
    }
    public bool IsLocalGame(){
        return mode == Mode.local;
    }
    public bool IsComputerGame(){
        return mode == Mode.computerBasic || mode == Mode.computerAdvanced;
    }
    public int GetDimension() {
        return dim;
    }
}
