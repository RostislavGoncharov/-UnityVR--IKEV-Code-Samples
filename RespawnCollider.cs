using UnityEngine;

/* This is a "safety net" collider which respawns objects inside the room
 * if they have somehow fallen out of bounds (to make the demo easier to present/playtest).
 */

public class RespawnCollider : MonoBehaviour
{
    [SerializeField] Transform _respawnTransform;
    private void OnCollisionEnter(Collision collision)
    {
        //collision.gameObject.transform.position = new Vector3(-0.5f, 1.0f, 2.0f);
        collision.gameObject.transform.position = _respawnTransform.position;
    }
}
