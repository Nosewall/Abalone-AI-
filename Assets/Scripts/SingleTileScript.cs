using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileScript : MonoBehaviour
{
  public void OnMouseDown()
  {
    Debug.Log("Clicked tile :" + this.gameObject.name);
    InputScript.addClickedTile(this.gameObject);
  }
}
