using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

//todo detectar cuando el spawnpoint esta dentro de un compare tag y detectar cuando esta dentro de otro spawnpoint
//todo cuando esta dentro de otro spawnpoint es verdadero
//todo cuando no esta dentro de otro spawnpoint es falso
    public bool spawned = false;
    public int openSide;
        private void OnTriggerStay(Collider other)
    {
        
    }
}
