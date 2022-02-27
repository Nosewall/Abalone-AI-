using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{

  public static List<GameObject> selectedTiles;
  // Start is called before the first frame update
  public void Start()
  {
    selectedTiles = new List<GameObject>();
  }
  public static void addClickedTile(GameObject tile)
  {
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
