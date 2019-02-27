using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    //public GameObject gobject;
    //RectTransform rt;
    public Image image;

    private void Start()

    {
        //rt = GetComponent<RectTransform>();
    }

    private void Update()
    {

        CooldownEffect();



    }
    void CooldownEffect()
    {
        var colorTemp = image.color;

        if (Input.GetKey(KeyCode.Escape))
        {
            colorTemp.a -= 0.1f;
            image.color = colorTemp;


            //bool transpa = false;

            //if (transpa == false)
            //{
            //    colorTemp.a -= 0.1f;
            //    image.color = colorTemp;

            //    if (colorTemp.a <= 0)
            //    {
            //        transpa = true;
            //    }
            //} else
            //{
            //    colorTemp.a += 0.1f;
            //    image.color = colorTemp;
            //    if (colorTemp.a >= 1)
            //    {
            //        transpa = false;
            //    }
            //}

        }
        else if (Input.GetKey(KeyCode.Delete))
        {
            colorTemp.a += 0.1f;
            image.color = colorTemp;
        }

        ////Vector2 rt2 = transform.localScale;
        ////rt2.y *= -1;
        ////transform.localScale = rt2;

        ////rt.transform.SendMessage("Me cago en tus muertos");

        //if (objeto.y < 0 && objeto.y >= 100)
        //{
        //    objeto.y -= 1;
        //    transform.localScale = objeto;
        //}

    }

}
