using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class testCameraScript : MonoBehaviour
{


    public void ButtonPressed(GameObject button)
    {
        print("button pos = " + button.transform.position);
        print("button screen-to-world pos = " + Camera.main.ScreenToWorldPoint(button.transform.position));
    }
    void OnGUI()
    {
        if (Input.GetMouseButtonDown(1)){
            Vector3 mousePos = Input.mousePosition;
            print("mouse pos = " + mousePos);
            print("screen-to-world pos = " + Camera.main.ScreenToWorldPoint(mousePos));
            print("screen-to-viewport pos = " + Camera.main.ScreenToViewportPoint(mousePos));
            print("free space\n");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
