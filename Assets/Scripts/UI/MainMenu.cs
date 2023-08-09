using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string FirstLevel;
    public GameObject OptionsScreen;
    public AudioSource ButtonPressSFX;

    float m_Delay = 0.25f;

    IEnumerator StartDelayTimer()
    {
        yield return new WaitForSeconds(m_Delay);

        SceneManager.LoadScene(FirstLevel);
    }

    public void StartGame()
    {
        StartCoroutine(StartDelayTimer());
    }

    public void OpenOptions()
    {
        OptionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        OptionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void PlayButtonPressSFX()
    {
        if (ButtonPressSFX == null)
            return;

        ButtonPressSFX.PlayOneShot(ButtonPressSFX.clip);
    }
}
