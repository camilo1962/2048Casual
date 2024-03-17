using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccion : MonoBehaviour
{

    Vector3 targetRot;
    Vector3 currentAngle;

    int currentSelection;

    int totalCharacter;
    
    public void Start()
    {
        currentSelection = 6;
    }

    public void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float screenHalf = Screen.width / 2;
            Vector3 movement = Vector2.zero;

            if (touch.position.x > screenHalf)
            {
                movement = Vector3.right;
            }
            else
            {
                movement = Vector3.left;
            }

           
        }


        if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.D) && currentSelection < totalCharacter)
        {
        
            currentAngle = transform.eulerAngles;
            targetRot = targetRot + new Vector3(0, 30, 0);
            currentSelection++;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.A) && currentSelection > 1)
        {
        
            currentAngle = transform.eulerAngles;
            targetRot = targetRot - new Vector3(0, 30, 0);
            currentSelection--;
        }
      


        currentAngle = new Vector3(0, Mathf.LerpAngle(currentAngle.y, targetRot.y,2.0f*Time.deltaTime),0);
        transform.eulerAngles = currentAngle;



    }


}
