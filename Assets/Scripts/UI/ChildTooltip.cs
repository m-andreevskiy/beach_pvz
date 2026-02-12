using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildTooltip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sandSprite;
    [SerializeField] private Sprite claySprite;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SetResource(string resourceName)
    {
        switch (resourceName)
        {
            case "sand":
				spriteRenderer.sprite = sandSprite;
                break;

            case "clay":
				spriteRenderer.sprite = claySprite;
                break;

            default:
				Debug.Log("No such resource name in ChildTooltip.setResource: " + resourceName);
                break;
        }
    }
}
