using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Health : NetworkBehaviour
{
    public Vector3[] SpawnPoints;
    public const int maxHealth = 100;
    private Text result;
    [SyncVar]
    public int health = maxHealth;

    void Awake()
    {
        
        NetworkStartPosition[] startPos = GameObject.Find("SpawnPoints").GetComponentsInChildren<NetworkStartPosition>();
        SpawnPoints = new Vector3[startPos.Length];
        for (int i = 0; i < startPos.Length; i++)
        {
            SpawnPoints[i] = startPos[i].transform.position;
        }
        result = GameObject.Find("ResultUI").GetComponentInChildren<Text>();
        if (result == null)
            Debug.Log("text == null");
    }
    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            health = 0;
            RpcDied();
        }
    }

    IEnumerator waitAndReset(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //result.text = "";
        // transform.position = SpawnPoints[Random.Range(0, 100)%SpawnPoints.Length];
        //health = 100;
        Application.LoadLevel(0);

    }
    [ClientRpc]
    void RpcDied()
    {
        if (isLocalPlayer)
            result.text = "You Lose!";
        else
            result.text = "You Won!";
        StartCoroutine(waitAndReset(3));
    }
}
