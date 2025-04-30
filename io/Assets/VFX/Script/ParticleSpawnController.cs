using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ParticleSpawnController : MonoBehaviour
{
    public GameObject prefabToSpawn; // Drag and drop the prefab you want to spawn in the Inspector.

    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        // Subscribe to the particle system's collision events.
    }

    private void OnParticleCollision(GameObject other)
    {
        // Instantiate the prefab at the collision point.
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[particles.GetSafeCollisionEventSize()];
        int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);

        // Instantiate the prefab at each collision point.
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Instantiate(prefabToSpawn, collisionEvents[i].intersection + new Vector3(-0.35f, 0.0f, 0.0f ), Quaternion.Euler(0.0f, -90.0f, 0.0f));
        }
    }
}