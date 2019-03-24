using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button playButton, pauseButton;

    public void PlayButton()
    {
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void PauseButton()
    {
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
}
