using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileScript : MonoBehaviour
{
  public TileColor tileColor;


  public void OnMouseDown()
  {
    Debug.Log("Clicked tile :" + this.gameObject.name);
    InputScript.tryToSelectTile(this.gameObject);
  }

  public void setTileColor(TileColor color)
  {
    tileColor = color;
  }

  public TileColor getTileColor()
  {
    return tileColor;
  }
}

public enum TileColor
{
  BLACK,
  WHITE,
  EMPTY,

}
