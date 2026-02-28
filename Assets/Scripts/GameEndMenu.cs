using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndMenu : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mouseBlockingPanel;
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowLose()
    {
        canvas.SetActive(true);
        mouseBlockingPanel.SetActive(true);
        text.text = "It is a defeat. But life doesn't end";
        text.color = Color.red;
    }

    public void ShowWin()
    {
        canvas.SetActive(true);
        mouseBlockingPanel.SetActive(true);
        text.text = "Good job here!";
        text.color = Color.yellow;
    }
}
