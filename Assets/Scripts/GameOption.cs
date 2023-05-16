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
    Mode mode;
    bool startingPlayer;
    public GameOption(int dim, bool startingPlayer, Mode mode)
	{
        this.dim = dim;
        this.mode = mode;
        this.startingPlayer = startingPlayer;
	}
    public Game initGame(){
        Game game = new Game(dim, isComputerGame());
        game.isP1Turn = startingPlayer;
        return game;
    }
    public bool isOnlineGame(){
        return mode == Mode.online;
    }
    public bool isLocalGame(){
        return mode == Mode.local;
    }
    public bool isComputerGame(){
        return mode == Mode.computerBasic || mode == Mode.computerAdvanced;
    }
	
}
