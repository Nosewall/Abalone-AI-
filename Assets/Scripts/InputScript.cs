using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
  public GameManager gameManager;
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
    Debug.Log("Currently Secelcted tiles:");
    Debug.Log("Count : " + selectedTiles.Count);
    foreach (GameObject debugTile in selectedTiles)
    {
      Debug.Log(debugTile.name + " -- Tile in " + debugCount + "Position :" + selectedTiles[debugCount].name);
      debugCount++;
    }

  }

  public static void tryToSelectTile(GameObject tile)
  {
    Debug.Log("Current Turn! : " + GameManager.getCurrentTurn().ToString());
    Debug.Log("Clicked tile! : " + tile.GetComponent<SingleTileScript>().getTileColor());
    TileColor selectedColor = tile.GetComponent<SingleTileScript>().getTileColor();
    if (selectedColor.ToString().Equals(GameManager.getCurrentTurn().ToString()))
    {
      //Color of selected tile matches turn
      addClickedTile(tile);
    }
  }



  public void moveE()
  {

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