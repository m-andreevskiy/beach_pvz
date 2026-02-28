using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject checkboxTick;

    void Start()
    {
        checkboxTick.SetActive(PlayerPrefs.GetInt("tutorial") == 1);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ToggleTutorial()
    {
        PlayerPrefs.SetInt("tutorial", checkboxTick.activeSelf ? 0 : 1);

        checkboxTick.SetActive( ! checkboxTick.activeSelf);
    }
}
