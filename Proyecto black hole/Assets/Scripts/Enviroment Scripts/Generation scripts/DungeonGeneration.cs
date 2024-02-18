using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public RoomTemplatesScript templates;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();

    public List<GameObject> spawnPointsUsed = new List<GameObject>();
    private int rand;

    GameObject newRoom;
        void Start()
    {
        Invoke("SpawnInitialRoom", 0.1f);
        Invoke("SpawnEndsRooms", 0.2f);
    }


        void SpawnInitialRoom()
    {
        rand = Random.Range(0, templates.initialRooms.Length);
        Instantiate(templates.initialRooms[rand], Vector3.zero, Quaternion.identity);
        spawnedRooms.Add(templates.initialRooms[rand]);
        foreach (Transform child in templates.initialRooms[rand].transform)
        {
            if (child.CompareTag("SpawnPoint"))
            {
                spawnPoints.Add(child.gameObject);
                spawnPointsUsed.Add(child.gameObject);
            }
        }
    }

        void SpawnEndsRooms()
    {
        Debug.Log("Spawning end rooms");

        // Recorremos la lista de spawnPointsUsed con un bucle for
        for (int i = 0; i < spawnPointsUsed.Count; i++)
        {
            GameObject spawnPoint = spawnPointsUsed[i];
            // Verificamos si el spawnPoint es nulo
            if (spawnPoint == null)
            {
                // Si es nulo, simplemente continuamos con el siguiente elemento en la lista
                continue;
            }

            RoomSpawner roomSpawner = spawnPoint.GetComponent<RoomSpawner>();
            Vector3 newPosition = spawnPoint.transform.position;
            Quaternion newRotation = spawnPoint.transform.rotation;

            switch (roomSpawner.openSide)
            {
                case 1: // Bottom door
                    newRoom = Instantiate(templates.endRoomBottom, newPosition, newRotation);
                    break;
                case 2: // Top door
                    newRoom = Instantiate(templates.endRoomTop, newPosition, newRotation);
                    break;
                case 3: // Left door
                    newRoom = Instantiate(templates.endRoomRight, newPosition, newRotation);
                    break;
                case 4: // Right door
                    newRoom = Instantiate(templates.endRoomLeft, newPosition, newRotation);
                    break;
                default:
                    break;
            }
        }
    }

}
