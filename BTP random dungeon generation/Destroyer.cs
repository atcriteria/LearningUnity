using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Used to detect collisions between multiple rooms
    // in the event that we spawn a room on top of another
    // we delete the room and try again.
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
