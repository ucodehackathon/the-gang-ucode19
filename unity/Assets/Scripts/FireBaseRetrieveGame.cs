using SimpleFirebaseUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FireBasePlayerInfo
{
    public int tag;
    public int age;
    public int heigth;
    public int weight;
    public int shoeSize;
    public string foot;

    public FireBasePlayerInfo(int tag, int age, int heigth, int weight, int shoeSize, string foot)
    {
        this.tag = tag;
        this.age = age;
        this.heigth = heigth;
        this.weight = weight;
        this.shoeSize = shoeSize;
        this.foot = foot;
    }
}

public class FireBaseRetrieveGame : MonoBehaviour
{
    public static Dictionary<int, FireBasePlayerInfo> firebaseInfo;

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

    private void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        try
        {
            string date = PlayerPrefs.GetString("firebaseSession", "");

            Dictionary<string, dynamic> dict = snapshot.Value<Dictionary<string, dynamic>>();
            List<string> keys = snapshot.Keys;


            if (keys != null)
            {
                foreach (string key in keys)
                {
                    firebaseInfo = new Dictionary<int, FireBasePlayerInfo>();
                    if (dict[key]["date"].ToString() == date)
                    {
                        foreach (dynamic player in dict[key]["players"])
                        {
                            int tag = (int)player["number"];
                            FireBasePlayerInfo playerInfo = new FireBasePlayerInfo(tag, (int)player["age"], (int)player["height"], (int)player["weight"], (int)player["shoeSize"], (string) player["mainFoot"]);
                            firebaseInfo[tag] = playerInfo;

                        }

                        Debug.Log(firebaseInfo.Count);
                        return;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        HandleError();
    }

    private void HandleError()
    {
        firebaseInfo = new Dictionary<int, FireBasePlayerInfo>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
