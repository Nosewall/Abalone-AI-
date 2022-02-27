using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
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
    TileBg.SetActive(true);
    Debug.Log(selectedTiles);
    selectedTiles.Add(tile);
    Debug.Log("Clicked tile : " + tile.name);
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