using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public string sceneName;
    public Dropdown dropdownSessions;

    public void ChangeScene()
    {
        string date;
        try
        {
            date = FireBaseRetrieve.dates[dropdownSessions.value];
        } catch (Exception e)
        {
            date = "";
        }
        PlayerPrefs.SetString("firebaseSession", date);
        SceneManager.LoadScene(sceneName);
    }
}
