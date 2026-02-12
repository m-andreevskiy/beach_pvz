using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private Animator outlineAnimator;


    public void LackHighlight()
	{
		outlineAnimator.Play("LackHighlight");
	}

}
