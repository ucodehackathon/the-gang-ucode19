using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ManagerPlayers : MonoBehaviour
{
    public static ManagerPlayers Instance { get; private set; }
    public GameObject playerPrefab;

    private Dictionary<int, Player> players;
    private Dictionary<Player, int> playersWithoutLog;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playersWithoutLog = new Dictionary<Player, int>();
        DateTime firstTime = ParseData.rows[0].time;
        HashSet<int> tags = new HashSet<int>();

        foreach (DataSetRow row in ParseData.rows)
        {
            if (row.time > firstTime)
            {
                break;
            }

            tags.Add(row.tag_id);
        }

        SpawnPlayers(tags);
    }

    private void SpawnPlayers(HashSet<int> tags)
    {
        players = new Dictionary<int, Player>();
        foreach (int tag in tags)
        {
            SpawnPlayer(tag);
        }
    }

    private void SpawnPlayer(int tag)
    {
        GameObject inst = Instantiate(playerPrefab);
        inst.name = "Player " + tag;
        Player player = inst.GetComponent<Player>();
        player.SetUp(tag);
        players.Add(tag, player);
    }

    public void UpdatePlayers(int indexUpdate, int nextUpdate)
    {
        if (indexUpdate > ParseData.rows.Count || nextUpdate > ParseData.rows.Count)
        {
            Debug.LogError("Incorrect Index Calculation");
            return;
        }

        HashSet<Player> updatedPlayers = new HashSet<Player>();

        DateTime dateTime = ParseData.rows[indexUpdate].time;
        for(int i = indexUpdate; i < ParseData.rows.Count &&
            dateTime == ParseData.rows[i].time; i++)
        {
            DataSetRow row = ParseData.rows[i];
            DataSetRow? nextRow = NextRow(row.tag_id, nextUpdate);
            //Debug.Log(row.time.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            if (!players.ContainsKey(row.tag_id))
            {
                SpawnPlayer(row.tag_id);
            }

            Player player = players[row.tag_id];
            player.UpdatePlayer(row, nextRow);
            if (/*player.gameObject.activeSelf || */row.speed >= 0)
            {
                player.gameObject.SetActive(true);
                playersWithoutLog[player] = 0;
                updatedPlayers.Add(player);
            }
        }
        UpdateNoLog(updatedPlayers);
    }

    private DataSetRow? NextRow(int tag, int from)
    {
        DateTime time = ParseData.rows[from].time;
        for (int i = from; i < ParseData.rows.Count; i++)
        {
            DataSetRow row = ParseData.rows[i];
            if (row.time > time)
            {
                return null;
            }

            if (row.tag_id == tag)
            {
                return row;
            }
        }

        return null;
    }

    private const int removeFromGameNoLog = 10;

    private void UpdateNoLog(HashSet<Player> updated)
    {
        IEnumerable<Player> withoutLog = players.Select(x => x.Value).Where(x => !updated.Contains(x));
        foreach(Player player in withoutLog)
        {
            playersWithoutLog[player] = playersWithoutLog[player] + 1;
            if (playersWithoutLog[player] >= removeFromGameNoLog)
            {
                player.gameObject.SetActive(false);
            }
        }
    }

    public void StopVelocities()
    {
        foreach(KeyValuePair<int, Player> player in players){
            player.Value.StopMoving();
        }
    }


    public Player lowerIndexPlayer
    {
        get
        {
            return players[players.Select(x => x.Key).Min()];
        }
    }

    public Player NextIndexPlayer(int index)
    {
        List<int> ordered = players.Where(x => x.Value.gameObject.activeSelf).Select(x => x.Key).OrderBy(x => x).ToList();
        int currentSubIndex = ordered.FindIndex(x => x == index);
        return players[ordered[(currentSubIndex + 1) % ordered.Count]];
    }
}
