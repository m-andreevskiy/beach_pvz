using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayBase : MonoBehaviour
{
    

    public void OnFadeAnimationEnd ()
    {
        Destroy(gameObject);
    }
}
