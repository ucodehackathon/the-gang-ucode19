//using Newtonsoft.Json;
using SimpleFirebaseUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseRetrieve : MonoBehaviour
{

    public Dropdown dropdown;
    public GameObject okCallBack, failCallBack;

    public static List<string> dates { get; private set; }

    private void Awake()
    {
        Firebase firebase = Firebase.CreateNew(FirebaseInfo.linkConnection, FirebaseInfo.key);
        firebase.OnGetSuccess += GetOKHandler;
        firebase.OnGetFailed += GetFailHandler;
        firebase.Child("sessions", true).GetValue();
    }

    private void GetFailHandler(Firebase sender, FirebaseError err)
    {
        Debug.Log("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
        HandleError();
    }

    private void HandleError()
    {
        okCallBack.SetActive(false);
        failCallBack.SetActive(true);
    }

    private void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        try
        {
            //Debug.Log(snapshot.RawJson);

            Dictionary<string, dynamic> dict = snapshot.Value<Dictionary<string, dynamic>>();
            List<string> keys = snapshot.Keys;


            if (keys != null)
            {
                dates = new List<string>();
                dropdown.ClearOptions();
                List<string> newOptions = new List<string>();
                foreach (string key in keys)
                {
                    newOptions.Add(dict[key]["name"].ToString());
                    dates.Add(dict[key]["date"].ToString());
                }
                dropdown.AddOptions(newOptions);
            } else
            {
                HandleError();
            }
        } catch (Exception e)
        {
            HandleError();
        }
    }
}
