using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
  public Movement movement;
  public ConsoleManager consoleManager;
  public Abalone abalone;
  public GameManager gameManager;
  public int numberOfSelectedTiles;
  public static List<GameObject> selectedTiles;
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

  public static void deselectAllTiles()
  {
    foreach (GameObject tile in selectedTiles)
    {
      GameObject TileBg = FindGameObjectInChildWithTag(tile, "Tile Background");
      TileBg.SetActive(false);
    }

    selectedTiles = new List<GameObject>();
  }

  public void tryToSelectTile(GameObject tile)
  {
    Node node = abalone.getNode(tile.name);
    consoleManager.sendMessageToConsole("N " + node.getName() + " X " + node.getX() + " Y " + node.getY());
    BoardColor selectedColor = tile.GetComponent<SingleTileScript>().getTileColor();
    if (selectedColor.ToString().Equals(GameManager.getCurrentTurn().ToString()))
    {
      //Color of selected tile matches turn
      addClickedTile(tile);
    }
  }
  public void moveE()
  {
    movement.validateMovement(Direction.E);
  }

  public void moveSE()
  {
    movement.validateMovement(Direction.SE);
  }
  public void moveSW()
  {
    movement.validateMovement(Direction.SW);
  }
  public void moveW()
  {
    movement.validateMovement(Direction.W);
  }
  public void moveNW()
  {
    movement.validateMovement(Direction.NW);
  }
  public void moveNE()
  {
    movement.validateMovement(Direction.NE);
  }

}