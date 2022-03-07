using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBackgroundScript : MonoBehaviour
{
  double R = 0;
  double G = 0;
  double B = 0;

  double L = 400;
  float speed = 0.10F;
  GameObject thisObject;
  // Start is called before the first frame update
  void Start()
  {
    thisObject = this.gameObject;
  }

  // Update is called once per frame
  void Update()
  {
    L = L + (speed);
    if (L > 700)
    {
      L = 400;
    }
    update_color(L);
    update_border_color();
  }

  void update_color(double l) // RGB <0,1> <- lambda l <400,700> [nm]
  {

    double t;
    if ((l >= 400.0) && (l < 410.0)) { t = (l - 400.0) / (410.0 - 400.0); R = +(0.33 * t) - (0.20 * t * t); }
    else if ((l >= 410.0) && (l < 475.0)) { t = (l - 410.0) / (475.0 - 410.0); R = 0.14 - (0.13 * t * t); }
    else if ((l >= 545.0) && (l < 595.0)) { t = (l - 545.0) / (595.0 - 545.0); R = +(1.98 * t) - (t * t); }
    else if ((l >= 595.0) && (l < 650.0)) { t = (l - 595.0) / (650.0 - 595.0); R = 0.98 + (0.06 * t) - (0.40 * t * t); }
    else if ((l >= 650.0) && (l < 700.0)) { t = (l - 650.0) / (700.0 - 650.0); R = 0.65 - (0.84 * t) + (0.20 * t * t); }
    if ((l >= 415.0) && (l < 475.0)) { t = (l - 415.0) / (475.0 - 415.0); G = +(0.80 * t * t); }
    else if ((l >= 475.0) && (l < 590.0)) { t = (l - 475.0) / (590.0 - 475.0); G = 0.8 + (0.76 * t) - (0.80 * t * t); }
    else if ((l >= 585.0) && (l < 639.0)) { t = (l - 585.0) / (639.0 - 585.0); G = 0.84 - (0.84 * t); }
    if ((l >= 400.0) && (l < 475.0)) { t = (l - 400.0) / (475.0 - 400.0); B = +(2.20 * t) - (1.50 * t * t); }
    else if ((l >= 475.0) && (l < 560.0)) { t = (l - 475.0) / (560.0 - 475.0); B = 0.7 - (t) + (0.30 * t * t); }
  }

  void update_border_color()
  {
    Color newColor = thisObject.GetComponent<SpriteRenderer>().color;

    newColor.r = (float)R;
    newColor.b = (float)B;
    newColor.g = (float)G;

    thisObject.GetComponent<SpriteRenderer>().color = newColor;
  }
}
