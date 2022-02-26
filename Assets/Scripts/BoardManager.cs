using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
  public ConsoleManager consoleManager;

  public static List<GameObject> BoardTiles = new List<GameObject>();
  // Start is called before the first frame update


  // Update is called once per frame
  void Update()
  {

  }

  public void referenceAllBoardTiles()
  {
    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Board Piece");
    foreach (GameObject tile in tiles)
    {
      BoardTiles.Add(tile);
    }

    setBoardToBelgianLayout();
  }

  public GameObject getTile(string tileName)
  {
    GameObject tileToReturn = new GameObject();
    tileToReturn.name = "Tried to get tile but returned NULL";
    foreach (GameObject tile in BoardTiles)
    {
      if (tile.name.Equals(tileName))
      {
        tileToReturn = tile;
      }
    }
    return tileToReturn;
  }

  public void changeTileColor(GameObject tileObject, BoardColor color)
  {
    consoleManager.sendMessageToConsole("Changing tile color of " + tileObject.name + " tile");
    Color tileColor = tileObject.GetComponent<SpriteRenderer>().color;
    switch (color)
    {
      case BoardColor.BLACK:
        tileColor = Color.black;
        break;

      case BoardColor.WHITE:
        tileColor = Color.white;
        break;

      case BoardColor.EMPTY:
        tileColor = Color.white;
        tileColor.a = .4f;
        break;
    }
    tileObject.GetComponent<SpriteRenderer>().color = tileColor;
  }

  public void setBoardToDefaultLayout()
  {
    List<GameObject> whiteStartingTiles = getAllTilesThatContain('I');
    List<GameObject> row2 = getAllTilesThatContain('H');
    whiteStartingTiles.AddRange(row2);
    whiteStartingTiles.Add(getTile("G5"));
    whiteStartingTiles.Add(getTile("G6"));
    whiteStartingTiles.Add(getTile("G7"));
    foreach (GameObject tile in whiteStartingTiles)
    {
      changeTileColor(tile, BoardColor.WHITE);
    }


    List<GameObject> blackStartingTiles = getAllTilesThatContain('A');
    List<GameObject> blackRow2 = getAllTilesThatContain('B');
    blackStartingTiles.AddRange(blackRow2);
    blackStartingTiles.Add(getTile("C4"));
    blackStartingTiles.Add(getTile("C3"));
    blackStartingTiles.Add(getTile("C5"));
    foreach (GameObject tile in blackStartingTiles)
    {
      changeTileColor(tile, BoardColor.BLACK);
    }


    List<GameObject> emptyStartingTiles = getAllTilesThatContain('D');
    List<GameObject> emptyRow2 = getAllTilesThatContain('E');
    List<GameObject> emptyRow3 = getAllTilesThatContain('F');
    emptyStartingTiles.AddRange(emptyRow2);
    emptyStartingTiles.AddRange(emptyRow3);
    emptyStartingTiles.Add(getTile("C1"));
    emptyStartingTiles.Add(getTile("C2"));
    emptyStartingTiles.Add(getTile("C6"));
    emptyStartingTiles.Add(getTile("C7"));
    emptyStartingTiles.Add(getTile("G3"));
    emptyStartingTiles.Add(getTile("G4"));
    emptyStartingTiles.Add(getTile("G8"));
    emptyStartingTiles.Add(getTile("G9"));

    foreach (GameObject tile in emptyStartingTiles)
    {
      changeTileColor(tile, BoardColor.EMPTY);
    }
  }


  public void setBoardToBelgianLayout()
  {
    List<GameObject> startingWhiteTiles = new List<GameObject>();
    List<GameObject> startingBlackTiles = new List<GameObject>();


    startingWhiteTiles.Add(getTile("I5"));
    startingWhiteTiles.Add(getTile("I6"));
    startingWhiteTiles.Add(getTile("H4"));
    startingWhiteTiles.Add(getTile("H5"));
    startingWhiteTiles.Add(getTile("H6"));
    startingWhiteTiles.Add(getTile("G4"));
    startingWhiteTiles.Add(getTile("G5"));
    startingWhiteTiles.Add(getTile("A4"));
    startingWhiteTiles.Add(getTile("A5"));
    startingWhiteTiles.Add(getTile("B4"));
    startingWhiteTiles.Add(getTile("B5"));
    startingWhiteTiles.Add(getTile("B6"));
    startingWhiteTiles.Add(getTile("C5"));
    startingWhiteTiles.Add(getTile("C6"));

    startingBlackTiles.Add(getTile("A1"));
    startingBlackTiles.Add(getTile("A2"));
    startingBlackTiles.Add(getTile("B1"));
    startingBlackTiles.Add(getTile("B2"));
    startingBlackTiles.Add(getTile("B3"));
    startingBlackTiles.Add(getTile("C2"));
    startingBlackTiles.Add(getTile("C3"));
    startingBlackTiles.Add(getTile("I8"));
    startingBlackTiles.Add(getTile("I9"));
    startingBlackTiles.Add(getTile("H7"));
    startingBlackTiles.Add(getTile("H8"));
    startingBlackTiles.Add(getTile("H9"));
    startingBlackTiles.Add(getTile("G7"));
    startingBlackTiles.Add(getTile("G8"));

    foreach (GameObject tile in BoardTiles)
    {
      changeTileColor(tile, BoardColor.EMPTY);
    }
    foreach (GameObject tile in startingWhiteTiles)
    {
      changeTileColor(tile, BoardColor.WHITE);
    }
    foreach (GameObject tile in startingBlackTiles)
    {
      changeTileColor(tile, BoardColor.BLACK);
    }
  }

  public void setBoardToGermanLayout()
  {
    List<GameObject> startingWhiteTiles = new List<GameObject>();
    List<GameObject> startingBlackTiles = new List<GameObject>();

    startingWhiteTiles.Add(getTile("H4"));
    startingWhiteTiles.Add(getTile("H5"));
    startingWhiteTiles.Add(getTile("G3"));
    startingWhiteTiles.Add(getTile("G4"));
    startingWhiteTiles.Add(getTile("G5"));
    startingWhiteTiles.Add(getTile("F3"));
    startingWhiteTiles.Add(getTile("F4"));
    startingWhiteTiles.Add(getTile("D6"));
    startingWhiteTiles.Add(getTile("D7"));
    startingWhiteTiles.Add(getTile("C5"));
    startingWhiteTiles.Add(getTile("C6"));
    startingWhiteTiles.Add(getTile("C7"));
    startingWhiteTiles.Add(getTile("B5"));
    startingWhiteTiles.Add(getTile("B6"));

    startingBlackTiles.Add(getTile("B1"));
    startingBlackTiles.Add(getTile("B2"));
    startingBlackTiles.Add(getTile("C1"));
    startingBlackTiles.Add(getTile("C2"));
    startingBlackTiles.Add(getTile("C3"));
    startingBlackTiles.Add(getTile("D2"));
    startingBlackTiles.Add(getTile("D3"));
    startingBlackTiles.Add(getTile("H8"));
    startingBlackTiles.Add(getTile("H9"));
    startingBlackTiles.Add(getTile("G7"));
    startingBlackTiles.Add(getTile("G8"));
    startingBlackTiles.Add(getTile("G9"));
    startingBlackTiles.Add(getTile("F7"));
    startingBlackTiles.Add(getTile("F8"));

    foreach (GameObject tile in BoardTiles)
    {
      changeTileColor(tile, BoardColor.EMPTY);
    }
    foreach (GameObject tile in startingWhiteTiles)
    {
      changeTileColor(tile, BoardColor.WHITE);
    }
    foreach (GameObject tile in startingBlackTiles)
    {
      changeTileColor(tile, BoardColor.BLACK);
    }
  }

  public List<GameObject> getAllTilesThatContain(char letter)
  {

    List<GameObject> tilesToReturn = new List<GameObject>();

    foreach (GameObject tile in BoardTiles)
    {
      char firstLetter = tile.name[0];
      if (firstLetter.Equals(letter))
      {
        tilesToReturn.Add(tile);
      }
    }
    return tilesToReturn;
  }
}



public enum BoardColor
{
  BLACK,
  WHITE,
  EMPTY
}
