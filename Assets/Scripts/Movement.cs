using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Movement : MonoBehaviour
{
  public BoardManager boardManager;
  public ConsoleManager consoleManager;
  public Abalone abalone;

  public GameManager gameManager;

  //Checks if the tiles are in a valid line for movement
  public bool checkIfTilesAreInLine()
  {
    List<Node> nodeList = new List<Node>();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    nodeList = nodeList.OrderBy(x => x.getX()).ToList();

    //If there is only 1 piece, then the pieces are in a line.
    if (nodeList.Count <= 1)
    {
      return true;
    }
    bool tilesAreInLine = false;

    //Strait West -> East or East -> West
    bool eastWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      eastWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 0
                     && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!eastWestPillar)
      {
        break;
      }
    }

    //SW -> NE pillar
    bool southWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      southWestPillar = nodeList[i].getY() - nodeList[i + 1].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!southWestPillar)
      {
        break;
      }
    }

    nodeList = nodeList.OrderBy(x => x.getY()).ToList();
    //NW -> SE pillar
    bool northWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      northWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 0;
      if (!northWestPillar)
      {
        break;
      }
    }

    tilesAreInLine = eastWestPillar || northWestPillar || southWestPillar;
    return tilesAreInLine;
  }


  //Checks if move is a side step. Returns false if pillar
  public Boolean checkIfSideStep(Direction direction)
  {
    List<Node> nodeList = new List<Node>();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    nodeList = nodeList.OrderBy(x => x.getX()).ToList();

    //If there is only 1 piece, then the pieces are in a line.
    if (nodeList.Count <= 1)
    {
      return true;
    }

    //Strait West -> East or East -> West
    bool eastWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      eastWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 0
                     && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!eastWestPillar)
      {
        break;
      }
    }

    //SW -> NE pillar
    bool southWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      southWestPillar = nodeList[i].getY() - nodeList[i + 1].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!southWestPillar)
      {
        break;
      }
    }

    nodeList = nodeList.OrderBy(x => x.getY()).ToList();
    //NW -> SE pillar
    bool northWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      northWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 0;
      if (!northWestPillar)
      {
        break;
      }
    }

    if (direction == Direction.E || direction == Direction.W)
    {
      return !eastWestPillar;
    }
    else if (direction == Direction.NE || direction == Direction.SW)
    {
      return !southWestPillar;
    }
    else
    {
      return !northWestPillar;
    }

  }

  //Checks if all spaces are empty for a Side Step.
  public Boolean checkIsSpaceEmptySideStep(Direction d)
  {
    List<Node> nodeList = new List<Node>();

    nodeList = nodeList.OrderBy(x => x.getX()).ToList();
    nodeList = nodeList.OrderBy(x => x.getY()).ToList();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    Node head = getHeadOfStackByDirection(d);
    return getNextTileByDirection(d, head).getColor() == BoardColor.EMPTY;
  }

  //Checks if all spaces are empty for a column move.
  public Boolean checkIsSpaceEmptyColumn(Direction d)
  {
    Boolean spaceIsEmpty = false;
    List<Node> nodeList = new List<Node>();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }
    List<Node> nodeListByX = nodeList.OrderBy(x => x.getX()).ToList();
    List<Node> nodeListByY = nodeList.OrderBy(x => x.getY()).ToList();
    //NW
    if (d == Direction.NW)
    {
      spaceIsEmpty = nodeListByY[0].getNWNeighbor() != null
      && nodeListByY[0].getNWNeighbor().getColor() == BoardColor.EMPTY;
    }
    //NE
    if (d == Direction.NE)
    {
      spaceIsEmpty = nodeListByY[0].getNENeighbor() != null
      && nodeListByY[0].getNENeighbor().getColor() == BoardColor.EMPTY;
    }
    //E
    if (d == Direction.E)
    {
      spaceIsEmpty = nodeListByX[nodeList.Count - 1].getENeighbor() != null
      && nodeListByX[nodeList.Count - 1].getENeighbor().getColor() == BoardColor.EMPTY;
    }
    //SE
    if (d == Direction.SE)
    {
      spaceIsEmpty = nodeListByY[nodeList.Count - 1].getSENeighbor() != null
      && nodeListByY[nodeList.Count - 1].getSENeighbor().getColor() == BoardColor.EMPTY;
    }
    //SW
    if (d == Direction.SW)
    {
      spaceIsEmpty = nodeListByY[nodeList.Count - 1].getSWNeighbor() != null
      && nodeListByY[nodeList.Count - 1].getSWNeighbor().getColor() == BoardColor.EMPTY;
    }
    //W
    if (d == Direction.W)
    {
      spaceIsEmpty = nodeListByX[0].getWNeighbor() != null
      && nodeListByX[0].getWNeighbor().getColor() == BoardColor.EMPTY;
    }
    return spaceIsEmpty;
  }

  public Boolean checkIfCanPush(Direction direction, int columnSize)
  {
    Debug.Log("Checking if can push");
    List<Node> nodeList = new List<Node>();
    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }
    int pushPower = nodeList.Count;

    List<Node> nodeListByX = nodeList.OrderBy(x => x.getX()).ToList();
    List<Node> nodeListByY = nodeList.OrderBy(x => x.getY()).ToList();

    Boolean canPush = false;
    BoardColor color = nodeList[0].getColor();
    BoardColor opponentColor = BoardColor.EMPTY;
    if (color == BoardColor.BLACK)
    {
      opponentColor = BoardColor.WHITE;
    }
    else
    {
      opponentColor = BoardColor.BLACK;
    }

    Node headNode = getHeadOfStackByDirection(direction);

    //Check first tile
    if (checkIfNodeIsNullByDirection(direction, headNode))
    {
      consoleManager.sendMessageToConsole("Trying to push empty space. Invalid move");
      return false;
    }
    Node next = getNextTileByDirection(direction, headNode);

    Boolean keepSearching = true;
    int numberToPush = 0;

    while (keepSearching)
    {
      if (next.getColor() == color)
      {
        keepSearching = false;
        canPush = false;
        consoleManager.sendMessageToConsole("Tried to push, but ran into one of your own pieces");
      }
      else if (next.getColor() == opponentColor)
      {
        numberToPush++;
        if (checkIfNodeIsNullByDirection(direction, next))
        {
          keepSearching = false;
          if (pushPower > numberToPush)
          {
            next.setColor(BoardColor.EMPTY);
            consoleManager.sendMessageToConsole("Pushing a piece off the board");
            consoleManager.sendMessageToConsole("Trying to push " + numberToPush);
            push(direction, nodeList);
            canPush = true;
            keepSearching = false;
          }

        }if(keepSearching){
          nodeList.Add(next);
          next = getNextTileByDirection(direction, next);
        }

        
      }
      else if (next.getColor() == BoardColor.EMPTY)
      {
        keepSearching = false;
        consoleManager.sendMessageToConsole("Trying to push " + numberToPush);
        if (numberToPush < pushPower)
        {
          push(direction, nodeList);
          canPush = true;
        }
        else
        {
          consoleManager.sendMessageToConsole("You're trying to push more tiles than you're able to.");
        }
      }
    }

    return canPush;

  }

  public Node getHeadOfStackByDirection(Direction direction, List<GameObject> selectedList = null, List<Node> inputNodeList = null)
  {
    if (selectedList == null)
    {
      selectedList = InputScript.selectedTiles;
    }
    List<Node> nodeList = new List<Node>();
    foreach (GameObject tile in selectedList)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    if (inputNodeList != null)
    {
      nodeList = inputNodeList;
    }

    List<Node> nodeListByX = nodeList.OrderBy(x => x.getX()).ToList();
    List<Node> nodeListByY = nodeList.OrderBy(x => x.getY()).ToList();

    Node toReturn = new Node("Error finding node head", BoardColor.EMPTY, false, 0, 0);

    //NW
    if (direction == Direction.NW)
    {
      toReturn = nodeListByY[0];
    }
    //NE
    if (direction == Direction.NE)
    {
      toReturn = nodeListByX[nodeListByX.Count - 1];
    }
    //E
    if (direction == Direction.E)
    {
      toReturn = nodeListByX[nodeListByX.Count - 1];
    }
    //SE
    if (direction == Direction.SE)
    {
      toReturn = nodeListByY[nodeListByX.Count - 1];
    }
    //SWNE
    if (direction == Direction.SW)
    {
      toReturn = nodeListByY[nodeListByX.Count - 1];
    }
    //W
    if (direction == Direction.W)
    {
      toReturn = nodeListByX[0];
    }
    return toReturn;

  }

  public Node getNextTileByDirection(Direction direction, Node node)
  {
    Node toReturn = null;

    //NW
    if (direction == Direction.NW)
    {
      toReturn = abalone.getNode(node.getX(), node.getY() - 1);
    }
    //NE
    if (direction == Direction.NE)
    {
      toReturn = abalone.getNode(node.getX() + 1, node.getY() - 1);
    }
    //E
    if (direction == Direction.E)
    {
      toReturn = abalone.getNode(node.getX() + 1, node.getY());
    }
    //SE
    if (direction == Direction.SE)
    {
      toReturn = abalone.getNode(node.getX(), node.getY() + 1);
    }
    //SWNE
    if (direction == Direction.SW)
    {
      toReturn = abalone.getNode(node.getX() - 1, node.getY() + 1);
    }
    //W
    if (direction == Direction.W)
    {
      toReturn = abalone.getNode(node.getX() - 1, node.getY());
    }
    return toReturn;

  }

  public Boolean checkIfNodeIsNullByDirection(Direction direction, Node node)
  {
    Boolean isTileNull = false;
    try
    {

      //NW
      if (direction == Direction.NW)
      {
        isTileNull = abalone.getNode(node.getX(), node.getY() - 1) == null;
      }
      //NE
      if (direction == Direction.NE)
      {
        isTileNull = abalone.getNode(node.getX() + 1, node.getY() - 1) == null;
      }
      //E
      if (direction == Direction.E)
      {
        isTileNull = abalone.getNode(node.getX() + 1, node.getY()) == null;
      }
      //SE
      if (direction == Direction.SE)
      {
        isTileNull = abalone.getNode(node.getX(), node.getY() + 1) == null;
      }
      //SW
      if (direction == Direction.SW)
      {
        isTileNull = abalone.getNode(node.getX() - 1, node.getY() + 1) == null;
      }
      //W
      if (direction == Direction.W)
      {
        isTileNull = abalone.getNode(node.getX() - 1, node.getY()) == null;
      }
    }
    catch (IndexOutOfRangeException)
    {
      return true;
    }
    return isTileNull;
  }

  public void validateMovement(Direction direction)
  {
    //Check to make sure tiles are selected
    if (InputScript.selectedTiles[0] == null)
    {
      consoleManager.sendMessageToConsole("Tried to  move with no tiles selected. Invalid move.");
      return;
    }
    //Check to make sure Tiles selected are actually in a moveable position
    consoleManager.sendMessageToConsole("Attempting to move " + direction);
    if (!checkIfTilesAreInLine())
    {
      consoleManager.sendMessageToConsole("Tiles selected are not in a column. Invalid move.");
      return;
    }

    //Check to see if the tiles are Sidestepping, or moving in a column
    if (checkIfSideStep(direction))
    {
      if (checkIsSpaceEmptySideStep(direction))
      {
        consoleManager.sendMessageToConsole("SideStep to an empty tile. Moving tile");
        move(direction);
        abalone.updateUIBoard();
        InputScript.deselectAllTiles();

      }
      else
      {
        consoleManager.sendMessageToConsole("Tried to side step into a non-empty position. Invalid move.");
        return;
      }
    }
    else
    {
      if (checkIsSpaceEmptyColumn(direction))
      {
        move(direction);
        abalone.updateUIBoard();
        InputScript.deselectAllTiles();
      }
      else
      {
        if (!checkIfCanPush(direction, InputScript.selectedTiles.Count))
        {
          return;
        }
      }
    }
    gameManager.cycleTurn();

  }

  //Once movement is validated, this can be called to move tiles
  public void move(Direction direction, List<Node> nodeList = null)
  {
    if (nodeList == null)
    {
      nodeList = new List<Node>();
      foreach (GameObject tile in InputScript.selectedTiles)
      {
        nodeList.Add(abalone.getNode(tile));
      }
    }

    List<Node> neighbors = new List<Node>();
    BoardColor color = nodeList[0].getColor();

    //NW
    if (direction == Direction.NW)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getNWNeighbor());
      }
    }
    //NE
    if (direction == Direction.NE)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getNENeighbor());
      }
    }
    //E
    if (direction == Direction.E)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getENeighbor());
      }
    }
    //SE
    if (direction == Direction.SE)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getSENeighbor());
      }
    }
    //SW
    if (direction == Direction.SW)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getSWNeighbor());
      }
    }
    //W
    if (direction == Direction.W)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getWNeighbor());
      }
    }

    //Neighbors assigned. Now Swap empties with Color.
    foreach (Node node in nodeList)
    {
      node.setColor(BoardColor.EMPTY);
    }
    foreach (Node node in neighbors)
    {
      node.setColor(color);
    }

  }

  public void moveColumn(Direction direction, List<Node> nodeList)
  {
    Node head = getHeadOfStackByDirection(direction, null, nodeList);

    while (nodeList.Count > 0)
    {
      head = getHeadOfStackByDirection(direction, null, nodeList);
      Node next = getNextTileByDirection(direction, head);
      next.setColor(head.getColor());
      head.setColor(BoardColor.EMPTY);
      nodeList.Remove(head);
    }
  }

  public void push(Direction direction, List<Node> nodeList)
  {
    List<GameObject> gameObjectList = new List<GameObject>();

    foreach (Node node in nodeList)
    {
      GameObject boardTile = boardManager.getTile(node.getName());
      gameObjectList.Add(boardTile);
    }
    moveColumn(direction, nodeList);
    abalone.updateUIBoard();
    InputScript.deselectAllTiles();
  }

}


public enum Direction
{
  NE,
  E,
  SE,
  SW,
  W,
  NW

}
