using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Rotador : MonoBehaviour
{

    public TMP_Text textObject;
    public  Renderer rend;
    public string nombre;
    public Color highlightColor;
    public Color defaultColor;
    public bool HasDesc;
    public int nivelNumero;
    public TMP_Text nombreTitulo;

    float rotation = 100f;

   

    void OnMouseOver()
    {
   
        rend = GetComponent<Renderer> ();
        rend.material.color = highlightColor;
        if (HasDesc)
        {
            nombreTitulo.text = nombre;      
            textObject.text = PlayerPrefs.GetInt("RecordText" + nivelNumero).ToString() + " puntos";
        }
        else
        {
            textObject.text = " ";
            nombreTitulo.text = " ";
        }
        float XaxisRotation = Input.GetAxis("Mouse X") * rotation;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotation;

        transform.Rotate(Vector3.down, XaxisRotation);
        transform.Rotate(Vector3.right, YaxisRotation);
    }
   
       void OnMouseExit()
       {
           rend = GetComponent< Renderer > ();
           rend.material.color = defaultColor;
           if (HasDesc)
           {
               textObject.text = " ";
               nombreTitulo.text = " ";
           }
        return;
       }
}
