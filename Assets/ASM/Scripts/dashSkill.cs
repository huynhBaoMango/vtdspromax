using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DashSkill")]
public class DashSkill : Skill
{
    public float dashVelocity;
    public float dashDuration = 0.2f; // Duration of the dash in seconds

    public override void Active(GameObject parent)
    {
        playerMove playermove = parent.GetComponent<playerMove>();
        if (playermove != null)
        {
            playermove.StartCoroutine(DashCoroutine(playermove, parent));
            Debug.Log("Dash started.");
        }
        else
        {
            Debug.LogWarning("PlayerMove component not found on the parent object.");
        }
    }

    private IEnumerator DashCoroutine(playerMove playermove, GameObject parent)
    {
        Rigidbody rb = parent.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody component not found on the parent object.");
            yield break;
        }

        Vector3 dashDirection = playermove.movement.normalized;
        if (dashDirection == Vector3.zero)
        {
            Debug.LogWarning("Dash direction is zero. Make sure the movement input is set correctly.");
            yield break;
        }

        // Calculate the force to apply over time
        Vector3 dashForce = dashDirection * dashVelocity / dashDuration;

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.AddForce(dashForce, ForceMode.VelocityChange);
            yield return null; // Wait for the next frame
        }

        // Optionally, you can smoothly reduce the velocity to zero after the dash duration
        float decelerationTime = 0.1f;
        float decelerationStartTime = Time.time;

        while (Time.time < decelerationStartTime + decelerationTime)
        {
            float t = (Time.time - decelerationStartTime) / decelerationTime;
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, t);
            yield return null;
        }

        // Ensure the velocity is set to zero at the end
        rb.velocity = Vector3.zero;
        Debug.Log("Dash ended.");
    }
}
