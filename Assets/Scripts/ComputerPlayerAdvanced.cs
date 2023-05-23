using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayerAdvanced : ComputerPlayer
{
    private System.Random random = new System.Random();
    public ComputerPlayerAdvanced(int id, Game game) : base(id, game)
    {

    }
    // The FindPrimeMove method tries to find a position to place the given prime on the board by generating random positions in it's half of the board.
    public ((int, int), int) FindPrimeMove(int prime)
    {
        for (int i = 0; i < game.GetDim() * game.GetDim(); i++)
        {
            int x = random.Next(0, game.GetDim() / 2);
            int y = random.Next(0, game.GetDim());

            if (game.IsValid((x, y), prime, id == 1) > 0)
            {
                return ((x, y), prime);
            }
        }
        return ((-1, -1), 0);
    }
    // The HasMulDiv method returns for a given number if the Computer Player has a number in his number pool, which is either a multiple or a divisor of the given number.
    public List<int> HasMulDiv(int num)
    {
        List<int> posNums = new List<int> { };
        for (int i = 0; i < numberPool.Count; i++)
        {
            if (num % numberPool[i] == 0 || numberPool[i] % num == 0)
            {
                posNums.Add(numberPool[i]);
            }
        }
        return posNums;
    }
    // The FindMove method overrides it's parents classes FindMethod to implement a more complex logic. 
    public new ((int, int), int) FindMove()
    {
        //Try to extend current region
        for (int i = 0; i < game.GetDim(); i++)
        {
            for (int j = 0; j < game.GetDim(); j++)
            {
                if (game.IsPlayersNumber((i, j), false))
                {
                    List<int> nums = HasMulDiv(game.board[i, j].Item1);
                    for (int l = 0; l < nums.Count; l++)
                    {
                        int num = nums[l];
                        if (!game.IsPlayersNumber((i + 1, j), false) && game.IsValid((i + 1, j), num, id == 1) > 0) return ((i + 1, j), num);
                        if (!game.IsPlayersNumber((i - 1, j), false) && game.IsValid((i - 1, j), num, id == 1) > 0) return ((i - 1, j), num);
                        if (!game.IsPlayersNumber((i, j + 1), false) && game.IsValid((i, j + 1), num, id == 1) > 0) return ((i, j + 1), num);
                        if (!game.IsPlayersNumber((i, j - 1), false) && game.IsValid((i + 1, j - 1), num, id == 1) > 0) return ((i, j - 1), num);
                    }
                }
            }
        }
        //Find move prime move
        for (int i = 0; i < numberPool.Count; i++)
        {
            if (game.IsPrime(numberPool[i]))
            {
                ((int, int), int) move = FindPrimeMove(numberPool[i]);
                if (move.Item2 != 0)
                {
                    return move;
                }
            }
        }
        //Try to override number
        for (int i = 0; i < game.GetDim(); i++)
        {
            for (int j = 0; j < game.GetDim(); j++)
            {
                if (game.IsPlayersNumber((i, j), id == 1))
                {
                    for (int n = 0; n < numberPool.Count; n++)
                    {
                        if (game.IsValid((i, j), n, id == 1) > 0) return ((i, j), n);
                    }
                }
            }
        }
        return ((-1, -1), 0);
    }
}