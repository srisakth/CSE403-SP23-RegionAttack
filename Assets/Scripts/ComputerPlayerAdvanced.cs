using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayerAdvanced : ComputerPlayer
{
    private System.Random random = new System.Random();
    public ComputerPlayerAdvanced(int id, Game game) : base(id, game)
    {

    }
    public ((int, int), int) findPrimeMove(int prime)
    {
        for (int i = 0; i < game.getDim() * game.getDim(); i++)
        {
            int x = random.Next(0, game.getDim() / 2);
            int y = random.Next(0, game.getDim());

            if (game.IsValid((x, y), prime, id == 1) > 0)
            {
                return ((x, y), prime);
            }
        }
        return ((-1, -1), 0);
    }
    public List<int> haveMulDiv(int num)
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
    public new ((int, int), int) findMove()
    {
        //Try to extend current region
        for (int i = 0; i < game.getDim(); i++)
        {
            for (int j = 0; j < game.getDim(); j++)
            {
                if (game.isPlayersNumber((i, j), false))
                {
                    List<int> nums = haveMulDiv(game.board[i, j].Item1);
                    for (int l = 0; l < nums.Count; l++)
                    {
                        int num = nums[l];
                        if (!game.isPlayersNumber((i + 1, j), false) && game.IsValid((i + 1, j), num, id == 1) > 0) return ((i + 1, j), num);
                        if (!game.isPlayersNumber((i - 1, j), false) && game.IsValid((i - 1, j), num, id == 1) > 0) return ((i - 1, j), num);
                        if (!game.isPlayersNumber((i, j + 1), false) && game.IsValid((i, j + 1), num, id == 1) > 0) return ((i, j + 1), num);
                        if (!game.isPlayersNumber((i, j - 1), false) && game.IsValid((i + 1, j - 1), num, id == 1) > 0) return ((i, j - 1), num);
                    }
                }
            }
        }
        //Find move prime move
        for (int i = 0; i < numberPool.Count; i++)
        {
            if (game.isPrime(numberPool[i]))
            {
                ((int, int), int) move = findPrimeMove(numberPool[i]);
                if (move.Item2 != 0)
                {
                    return move;
                }
            }
        }
        //Try to override number
        for (int i = 0; i < game.getDim(); i++)
        {
            for (int j = 0; j < game.getDim(); j++)
            {
                if (game.isPlayersNumber((i, j), id == 1))
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