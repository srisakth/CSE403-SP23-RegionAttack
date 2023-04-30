using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class PlaceTileTest
{
    GameManager gameManager;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Set up the expected state of a generic game
        gameManager = new GameManager();
        var gridManager = new GridManager();
        HandManager _p1Hand = new HandManager();
        HandManager _p2Hand = new HandManager();
        PopUp _popup = new PopUp();
        Timer _moveTimer = new Timer();

        GameObject _score1 = new ();
        GameObject _score2 = new ();
        _score1.AddComponent<TMP_Text>();
        _score2.AddComponent<TMP_Text>();

    }


    [UnityTest]
    public IEnumerator PlaceInvalidTile()
    {
        // TODO: call set functions to choose invalid tiles and check appropriate error messages are displayed
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlaceValidTile()
    {
        // TODO: call set functions to choose valid tiles and check that the board is updated
        yield return null;
    }


}
