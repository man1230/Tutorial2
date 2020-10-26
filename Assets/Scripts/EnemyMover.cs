using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
// Transforms to act as start and end markers for the journey.
private Vector3 pos1;
private Vector3 pos2;
public Vector3 offset = new Vector3 (10f, 0f, 0f);
public float speed = 1.0f;

// Time when the movement started.
private float startTime;

// Total distance between the markers.
private float journeyLength;

void Start()
     {
         pos1 = transform.position;
         pos2 = transform.position + offset;
     // Keep a note of the time the movement started.
          startTime = Time.time;

     // Calculate the journey length.
          journeyLength = Vector3.Distance(pos1, pos2);
     }

// Follows the target position like with a spring
void Update()
     {
     // Distance moved = time * speed.
          float distCovered = (Time.time - startTime) * speed;

     // Fraction of journey completed = current distance divided by total distance.
          float fracJourney = distCovered / journeyLength;

     // Set our position as a fraction of the distance between the markers and pingpong the movement.
          transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong (fracJourney, 1));
     }
}
