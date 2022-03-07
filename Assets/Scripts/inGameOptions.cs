using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameOptions : MonoBehaviour
{
  public GameObject menuObject;
  public GameObject gameEmptyObject;
  public void backToMenu()
  {
    gameEmptyObject.SetActive(false);
    menuObject.SetActive(true);
  }
}
