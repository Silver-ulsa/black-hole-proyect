using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerationScript : MonoBehaviour
{
    public int iterations;
    public RoomTemplatesScript templates;
    private int rand;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> neighborSpawnPoints = new List<GameObject>();
    public List<GameObject> visidedSpawnPoints = new List<GameObject>();
    GameObject newRoom;

    void Start()
    {
        Invoke("SpawnInitialRoom", 0.1f);
        InvokeRepeating("SpawnNewRooms", 1f, 1f);
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
            }
        }
    }

    void SpawnNewRooms()
    {
        foreach (GameObject spawnPoint in new List<GameObject>(spawnPoints))
        {
            RoomSpawner roomSpawner = spawnPoint.GetComponent<RoomSpawner>();
            // Variables para guardar la posición y rotación de la nueva sala
            Vector3 newPosition = spawnPoint.transform.position;
            Quaternion newRotation = spawnPoint.transform.rotation;

            // Elige la sala adecuada según la dirección de apertura del spawn point
            switch (roomSpawner.openSide)
            {
                case 1: // Bottom door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    newRoom = Instantiate(templates.bottomRooms[rand], newPosition, newRotation); // Asigna la referencia a newRoom aquí
                    break;
                case 2: // Top door
                    rand = Random.Range(0, templates.topRooms.Length);
                    newRoom = Instantiate(templates.topRooms[rand], newPosition, newRotation);
                    break;
                case 3: // Left door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    newRoom = Instantiate(templates.rightRooms[rand], newPosition, newRotation);
                    break;
                case 4: // Right door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    newRoom = Instantiate(templates.leftRooms[rand], newPosition, newRotation);
                    break;
                default:
                    break;
            }
        }

        foreach (GameObject spawnPoint in new List<GameObject>(spawnPoints))
        {
            neighborSpawnPoints.Add(spawnPoint);
            spawnPoints.Remove(spawnPoint);
        }

        spawnPoints.Add(neighborSpawnPoints[0]);
        neighborSpawnPoints.Remove(neighborSpawnPoints[0]);

        if (iterations  >= 0)
        {
                    CancelInvoke("SpawnNewRooms");
        }

    }
}
