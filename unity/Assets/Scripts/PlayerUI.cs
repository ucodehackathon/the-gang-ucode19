using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private DoubleCamera doubleCamera;
    [SerializeField] private GameObject showPanel;
    [SerializeField] private Camera pointOfView;
    [SerializeField] private Text dorsal, age, heigth, weigth, distance, speed, energy;
    [SerializeField] private Canvas canvas;
    private Player currentPlayer;

    private void SetPlayer(Player player)
    {
        currentPlayer = player;
    }

    private void Update()
    {
        UpdatePlayerClick();
        UpdateActive();
        UpdateTransformPosition();
        UpdateInfo();
    }

    private void UpdateActive()
    {
        bool active = currentPlayer != null && currentPlayer.gameObject.activeSelf;
        showPanel.SetActive(active);
        if (active)
        {
            doubleCamera.SwitchToPlayer(currentPlayer);
        } else
        {
            doubleCamera.SwitchToSuperior();
        }
    }

    private void UpdateInfo()
    {
        if (currentPlayer != null)
        {
            UpdateFields(currentPlayer.lastRow);
            UpdateFireBase();
        }
    }

    private void UpdateFireBase()
    {
        try{
            FireBasePlayerInfo fireBaseRetrieve = FireBaseRetrieveGame.firebaseInfo[currentPlayer.tagID];
            age.text = fireBaseRetrieve.age.ToString();
            heigth.text = fireBaseRetrieve.heigth.ToString();
            weigth.text = fireBaseRetrieve.weight.ToString();

        }catch (Exception e)
        {
            System.Random random = new System.Random();
            int age = random.Next(20, 36);
            int height = random.Next(168, 185);
            int weight = random.Next(64, 83);
            int shoe = random.Next(39, 46);
            string mainFoot = random.Next(0, 2) == 0 ? "left" : "right";
            FireBasePlayerInfo fireBasePlayer = new FireBasePlayerInfo(currentPlayer.tagID, age, height, weight, shoe, mainFoot);
            FireBaseRetrieveGame.firebaseInfo[currentPlayer.tagID] = fireBasePlayer;
        }

    }

    private void UpdateTransformPosition()
    {
        if (currentPlayer != null)
        {
            this.transform.position = pointOfView.WorldToScreenPoint(currentPlayer.gameObject.transform.position) + new Vector3(0, (this.GetComponent<RectTransform>().rect.height / 2) * canvas.scaleFactor, 0);
        }
    }

    private void UpdatePlayerClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            bool detected = false;
            Ray ray = pointOfView.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 5);
            if (Physics.Raycast(ray, out hit, 200))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag("Player"))
                {
                    SetPlayer(obj.GetComponentInChildren<Player>());
                    detected = true;
                }
            }

            if (!detected)
            {
                SetPlayer(null);
            }
        }
    }

    public void UpdateFields(DataSetRow row)
    {
        dorsal.text = row.tag_id.ToString();
        distance.text = row.total_distance.ToString();
        speed.text = row.speed.ToString();
        energy.text = row.energy.ToString();
    }
}
