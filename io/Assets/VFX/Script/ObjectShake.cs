using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float shakeAmount = 0.1f; // The intensity of the shaking
    public float shakeDuration = 0.5f; // The duration of the shaking in seconds

    private Vector3 originalPosition; // The original position of the object

    private void Start()
    {
        originalPosition = transform.position; // Store the original position
        StartShaking();
    }

    public void StartShaking()
    {
        // Start the coroutine to perform the shaking effect
        StartCoroutine(ShakeCoroutine());
    }

    private System.Collections.IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Generate random displacement in each axis
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            float offsetZ = Random.Range(-shakeAmount, shakeAmount);

            // Apply the shake
            transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ);

            // Wait for the next frame
            yield return null;

            // Update elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Reset the position to the original position when shaking is done
        transform.position = originalPosition;
    }
}