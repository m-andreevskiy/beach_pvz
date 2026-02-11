using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOption : MonoBehaviour
{
    [SerializeField] private GameObject resourceModal;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject highlightChosen;
    [SerializeField] private ChildBase childScript;
    [SerializeField] private string resourceName;


    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    
    void OnMouseUp()
    {
        highlight.SetActive(false);
        highlightChosen.SetActive(true);
        highlightChosen.transform.position = transform.position;
        resourceModal.SetActive(false);

        childScript.SetResourceProduction(resourceName);
    }

}
