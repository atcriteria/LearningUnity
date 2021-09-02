using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // I reversed 3 and 4 on accident, just rolling with it.
    // 3 --> need right door
    // 4 --> need left door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        // The time between spawning a room by calling the Spawn function below
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        // If we have yet to spawn a room attached to this one,
        // determine the type of room to try and spawn.
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //  Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //  Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                //  Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //  Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    // If we collide with another room and neither room have spawned their adjacent rooms
    // then we create a closed room and destroy the game object to keep it from spawning
    // multiples, then we flip spawned to true to keep this script from running infinitely.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                // spawn walls blocking any opening
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

}
