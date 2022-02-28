using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
  public ConsoleManager consoleManager;
  public Abalone abalone;
  public GameManager gameManager;
  public int numberOfSelectedTiles;
  public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
  {
    Transform t = parent.transform;
    for (int i = 0; i < t.childCount; i++)
    {
      if (t.GetChild(i).gameObject.tag == tag)
      {
        return t.GetChild(i).gameObject;
      }
    }
    return null;
  }

  public static List<GameObject> selectedTiles;
  // Start is called before the first frame update
  public void Start()
  {
    selectedTiles = new List<GameObject>();
  }
  public static void addClickedTile(GameObject tile)
  {
    GameObject TileBg = FindGameObjectInChildWithTag(tile, "Tile Background");

    if (selectedTiles.Contains(tile))
    {
      TileBg.SetActive(false);
      selectedTiles.Remove(tile);
    }
    else
    {
      TileBg.SetActive(true);
      selectedTiles.Add(tile);
      Debug.Log("Clicked tile : " + tile.name);
    }
    if (selectedTiles.Count > 3)
    {
      GameObject TileZeroBg = FindGameObjectInChildWithTag(selectedTiles[0], "Tile Background");
      selectedTiles.Remove(selectedTiles[0]);
      TileZeroBg.SetActive(false);
    }
    int debugCount = 0;
    foreach (GameObject debugTile in selectedTiles)
    {
      debugCount++;
    }

  }

  public void tryToSelectTile(GameObject tile)
  {
    consoleManager.sendMessageToConsole("Printing all neighbors!");
    Node testNode = abalone.getNode(tile);
    foreach (Node n in testNode.getNeighbors())
    {
      consoleManager.sendMessageToConsole(n.getName());
    }
    abalone.getNode(tile.name);
    BoardColor selectedColor = tile.GetComponent<SingleTileScript>().getTileColor();
    if (selectedColor.ToString().Equals(GameManager.getCurrentTurn().ToString()))
    {
      //Color of selected tile matches turn
      addClickedTile(tile);
    }
  }



  public void moveE()
  {
    //Get selected tiles on the backend
    List<Node> selectedNodes = new List<Node>();
    foreach (GameObject selectedObject in selectedTiles)
    {
      selectedNodes.Add(abalone.getNode(selectedObject));

      //Check if valid configuration for a move ( A Pillar )
      bool isValidMove = abalone.checkIfTilesAreInLine(selectedNodes);
      if (!isValidMove)
      {
        consoleManager.printNotAPillarError();
        return;
      }
      //Check if you're moving strait or Diagonally
      bool horizontalPillar = abalone.areNodesInHorizontalPillar(selectedNodes);

      //Check if the space is empty
      bool spaceIsEmptyEast = false;
      if (horizontalPillar)
      {
        spaceIsEmptyEast = abalone.checkIfEastIsEmptyPillar(selectedNodes);
      }
      else
      {
        spaceIsEmptyEast = abalone.checkIfEastIsEmptySideStep(selectedNodes);
      }
      //If the space is empty and selected tiles in a line, move tiles
      //Else if the space is occupied, check by how many tiles
      //If less, push tiles
      //Check for tiles off the board and destroy them
      //switch turns
    }
  }
  public void moveSE()
  {

  }
  public void moveSW()
  {

  }
  public void moveW()
  {

  }
  public void moveNW()
  {

  }
  public void moveNE()
  {

  }

}