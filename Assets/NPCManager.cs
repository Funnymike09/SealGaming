using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public GameObject NPC;
    public bool stopSpawning = false;
    public Transform[] SpawnPoints;

    public int requiredMoneyPerSpawn = 300;
    public int maxNPCs = 15;

    private float cumulativeEarned;
    private float previousMoney;
    private List<GameObject> activeNPCs = new List<GameObject>();

    private void Awake()
    {
       if(instance != null && instance != this)
        {
            Destroy(this);
        }
       else
        {
            instance = this;
        }

        previousMoney = EconomyManager.instance.currentMoney;
    }

    private void Update()
    {
        if (stopSpawning || activeNPCs.Count >= maxNPCs) return;
        float currentMoney = EconomyManager.instance.currentMoney;
        float earned = currentMoney - previousMoney;

        if (earned > 0) cumulativeEarned += earned;
        previousMoney = currentMoney;

        while (cumulativeEarned >= requiredMoneyPerSpawn)
        {
            cumulativeEarned -= requiredMoneyPerSpawn;
            SpawnNPC();
            if (activeNPCs.Count >= maxNPCs) break;

        }
    }

    void SpawnNPC()
    {
        if (SpawnPoints.Length == 0 || NPC == null) return;

        Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        GameObject newNPC = Instantiate(NPC, spawnPoint.position, spawnPoint.rotation);
        activeNPCs.Add(newNPC);
    }

    public void NPCDestroyed(GameObject npc)
    {
        if(activeNPCs.Contains(npc))
        {
            activeNPCs.Remove(npc);
        }
    }


}
