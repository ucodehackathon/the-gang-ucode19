using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class DoubleCamera : MonoBehaviour
{
    [SerializeField] private Camera superiorCamera;
    [SerializeField] private Camera characterCamera;

    private bool camerasEnabled = false;

    private enum State { Superior, Character }
    private State state;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JY_Y"))
        {
            camerasEnabled = !camerasEnabled;
            superiorCamera.enabled = camerasEnabled && state == State.Superior;
            characterCamera.enabled = camerasEnabled && state == State.Character;
        }
    }

    public void SwitchToSuperior()
    {
        state = State.Superior;
        superiorCamera.enabled = camerasEnabled;
        characterCamera.enabled = false;
    }

    public void SwitchToPlayer(Player player)
    {
        state = State.Character;
        superiorCamera.enabled = false;
        characterCamera.enabled = camerasEnabled;
        AutoCam autoCam = characterCamera.GetComponent<AutoCam>();
        autoCam.enabled = true;
        autoCam.m_Target = player.seenCam;

    }
}
