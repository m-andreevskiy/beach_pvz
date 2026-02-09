using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResourceChangeModal : MonoBehaviour
{
    private new Collider2D collider;
	private bool isFirstFrame = true;

	void Start()
	{
		collider = GetComponent<Collider2D>();
	}


	void OnEnable()
	{
		isFirstFrame = true;
	}

    void Update()
    {


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
			if (isFirstFrame)
			{
				isFirstFrame = false;
				return;
			}
		
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (collider.OverlapPoint(mousePosition))
			{
				// Debug.Log("click inside");
			}
			else
			{
				this.gameObject.SetActive(false);
			}

        }

    }
}
