using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private CastleBase castleScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = castleScript.GetTooltipInfo();
    }
}
