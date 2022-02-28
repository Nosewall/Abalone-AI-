using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileScript : MonoBehaviour
{
  public InputScript inputScript;
  public BoardColor tileColor;


  public void OnMouseDown()
  {
    Debug.Log("Clicked tile :" + this.gameObject.name);
    inputScript.tryToSelectTile(this.gameObject);
  }

  public void setTileColor(BoardColor color)
  {
    tileColor = color;
  }

  public BoardColor getTileColor()
  {
    return tileColor;
  }
}
