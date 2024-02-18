using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openSide;
    public bool visited = false;
    public List<GameObject> spawnPoints = new List<GameObject>();
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.green);
    }
}
