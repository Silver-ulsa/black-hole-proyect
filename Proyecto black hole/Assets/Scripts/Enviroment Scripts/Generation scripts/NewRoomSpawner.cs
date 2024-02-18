using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoomSpawner : MonoBehaviour
{
    public int iterations;
    public RoomTemplatesScript templates; // Asegúrate de asignar esto en el Inspector
    private int rand;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> spawnPointsUsed = new List<GameObject>();

    GameObject newRoom; // Declara newRoom aquí para que esté disponible en todo el método

    void Start()
    {
        Invoke("SpawnInitialRoom", 0.1f);
        InvokeRepeating("SpawnNewRooms", 1f, 1f); // Invoca repetidamente SpawnNewRooms cada segundo
        Invoke("SpawnEndsRooms", 2f);
        Invoke("CorrectiveRooms", 3f);
    }

    void SpawnInitialRoom()
    {
        // Genera un índice aleatorio dentro del rango de habitaciones disponibles
        rand = Random.Range(0, templates.initialRooms.Length);

        //spawn a room in the initial position (0,0,0)
        Instantiate(templates.initialRooms[rand], Vector3.zero, Quaternion.identity);

        spawnedRooms.Add(templates.initialRooms[rand]);

        // Agrega los puntos de spawn de la habitación inicial a la lista de puntos de spawn
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

        // Itera sobre cada spawn point existente
        foreach (GameObject spawnPoint in new List<GameObject>(spawnPoints))
        {
            RoomSpawner roomSpawner = spawnPoint.GetComponent<RoomSpawner>();

            // Comprueba si el spawn point tiene el componente RoomSpawner adjunto
            if (roomSpawner != null)
            {
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

                // Si encontramos la habitación, continuamos con la lógica
                if (newRoom != null)
                {
                    // Obtén los nuevos spawn points generados en la sala recién instanciada
                    foreach (Transform child in newRoom.transform)
                    {
                        if (child.CompareTag("SpawnPoint"))
                        {
                            spawnPointsUsed.Add(child.gameObject); // Agrega el spawn point a la lista de spawn points utilizados
                        }
                    }

                    spawnPoints.Remove(spawnPoint);
                }
            }
            else
            {
                // Si el GameObject es nulo, simplemente continúa con la iteración
                continue;
            }
        }

        iterations--;
        if (iterations <= 0)
        {
            CancelInvoke("SpawnNewRooms");
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