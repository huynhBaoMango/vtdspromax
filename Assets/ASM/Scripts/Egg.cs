using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject damageZonePrefab;
    public float fallImpactForce = 5f;
    public float damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > fallImpactForce)
        {
            Vector3 damageSpot = new Vector3(transform.position.x, 1, transform.position.z);
            GameObject zone = Instantiate(damageZonePrefab, transform.position, Quaternion.identity);
            zone.GetComponent<DamageZone>().damage = damage;
            Destroy(gameObject);
        }
    }
}
