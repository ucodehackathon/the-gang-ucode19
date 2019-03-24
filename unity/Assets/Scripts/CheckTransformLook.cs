using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class CheckTransformLook : MonoBehaviour
{
    private AutoCam lookCam;

    private Player currentPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        lookCam = GetComponent<AutoCam>();
    }

    private void OnEnable()
    {        
        currentPlayer = ManagerPlayers.Instance.lowerIndexPlayer;
        lookCam.m_Target = currentPlayer.followingCam;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JY_B"))
        {
            currentPlayer = ManagerPlayers.Instance.NextIndexPlayer(currentPlayer.tagID);
            lookCam.m_Target = currentPlayer.followingCam;
        }
    }
}
