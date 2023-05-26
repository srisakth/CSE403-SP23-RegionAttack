using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TMPro;

public class HandManagerTest
{
    HandManager handManager;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        GameObject gm = new GameObject();
        gm.AddComponent<GameManager>();
        GameManager gameManager = gm.GetComponent<GameManager>();

        var tilePrefab = new GameObject();
        tilePrefab.AddComponent<Tile>();
        tilePrefab.AddComponent<Button>();
        tilePrefab.AddComponent<TextMeshProUGUI>();
        Tile tile = tilePrefab.GetComponent<Tile>();
        tile._button = tilePrefab.GetComponent<Button>();
        tile._text = tilePrefab.GetComponent<TextMeshProUGUI>();

        var container = new GameObject();
        container.AddComponent<GridLayoutGroup>(); 
        container.AddComponent<HandManager>();
        handManager = container.GetComponent<HandManager>();
        handManager._parent = container.GetComponent<GridLayoutGroup>();

        gameManager._p1Hand = handManager;

        handManager.Initialize(tile, true, gameManager);
    }


    [UnityTest]
    public IEnumerator AddRemoveNumberTest()
    {
        // 0 tiles
        Assert.NotNull(handManager);
        Assert.NotNull(handManager._hand);
        Assert.AreEqual(0, handManager._hand.Count);
        Assert.AreEqual(0, handManager.transform.childCount);


        // 1 tile
        handManager.AddNumber(3);
        yield return null;  // Just for safety
        Assert.AreEqual(1, handManager._hand.Count);
        Assert.AreEqual(1, handManager.transform.childCount);
        Assert.AreEqual(3, handManager._hand[0]._num);
        Assert.IsTrue(handManager._hand[0]._isP1);


        // 2 tiles
        handManager.AddNumber(1);
        yield return null;  // Just for safety
        Assert.AreEqual(2, handManager._hand.Count);
        Assert.AreEqual(2, handManager.transform.childCount);
        Assert.AreEqual(3, handManager._hand[0]._num);
        Assert.AreEqual(1, handManager._hand[1]._num);
        Assert.IsTrue(handManager._hand[0]._isP1);


        // Remove the first one
        handManager.RemoveNumber(handManager._hand[0]);
        yield return null;  // Just for safety
        Assert.AreEqual(1, handManager.transform.childCount);
        Assert.AreEqual(1, handManager._hand.Count);
        Assert.AreEqual(1, handManager._hand[0]._num);


        // Remove the other one
        handManager.RemoveNumber(handManager._hand[0]);
        yield return null;  // Just for safety
        Assert.AreEqual(0, handManager._hand.Count);
        Assert.AreEqual(0, handManager.transform.childCount);
    }

    [UnityTest]
    public IEnumerator DisplayClearHandTest()
    {
        // TODO: call set functions to choose valid tiles and check that the board is updated
        yield return null;
    }

}
