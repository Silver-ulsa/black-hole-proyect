using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomToListScript : MonoBehaviour
{
    private RoomTemplatesScript templates;
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplatesScript>();
        templates.rooms.Add(this.gameObject);
    }
}
