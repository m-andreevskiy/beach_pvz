using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

  [SerializeField] private GameObject pauseMenu; 

  private bool isPaused = false;
  private float timeScale = 1; 
  private GameManager gameManager;


  public bool IsPaused()
  {
    return isPaused;
  }

  // Start is called before the first frame update
  void Start()
  {
    isPaused = false;
    pauseMenu.SetActive(false);
    gameManager = GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {
    if (gameManager.gameGoesOn && Input.GetKeyDown(KeyCode.Escape))
    {
      if (isPaused)
      {
        Unpause();
      }
      else
      {
        Pause();
      }
    }
  }


  public void Pause()
  {
    if (gameManager != null)
    {
      gameManager.draggingObject = null;
      gameManager.currentContainer = null;
    }
    else
    {
      print("No gameManager attached to gameObject of PauseMenu");
    }

    pauseMenu.SetActive(true);

    timeScale = Time.timeScale;
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void Unpause()
  {
    pauseMenu.SetActive(false);
    Time.timeScale = timeScale;
    isPaused = false;
  }

  public void Exit()
  {
    Application.Quit();
  }

}
