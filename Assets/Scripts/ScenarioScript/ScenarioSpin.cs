using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For Text Mesh pro

public class ScenarioSpin : MonoBehaviour
{
    private float rotationAngle = 45f;
    private float rotationDuration = 0.5f;
    private bool isRotating = false;
    private bool canRotate = true;

    // Counters
    public int maxRotations = 5;
    public int maxGravityFlips = 3;
    public int remainingRotations;
    public int remainingGravityFlips;

    // Text References
    public TMP_Text rotationCounterText;
    public TMP_Text gravityCounterText;

    // Track gravity flip state
    private bool isGravityFlipped = true;

    // Start is called before the first frame update
    void Start()
    {
        remainingRotations = maxRotations;
        remainingGravityFlips = maxGravityFlips;
        // Initialize the UI to display counter
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (!canRotate)
        {
            return;
        }

        if (remainingGravityFlips <= 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) && !isRotating && remainingRotations > 0)
        {
            StartCoroutine(SmoothRotate(rotationAngle));
            remainingRotations--;  // Decrease rotation count
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.D) && !isRotating && remainingRotations > 0)
        {
            StartCoroutine(SmoothRotate(-rotationAngle));
            remainingRotations--;  // Decrease rotation count
            UpdateUI();
        }
        // Flip Gravity (G Key)
        if (Input.GetKeyDown(KeyCode.G) && remainingGravityFlips > 0)
        {
            // Flip gravity direction
            if (!isGravityFlipped)
            {
                Physics2D.gravity = new Vector2(0, -9.81f); // Flip gravity downwards
                isGravityFlipped = true;  // Mark gravity as flipped
            }
            else
            {
                Physics2D.gravity = new Vector2(0, 9.81f); // Restore gravity to normal
                isGravityFlipped = false;  // Mark gravity as normal
            }

            remainingGravityFlips--;  // Decrease gravity flip count
            UpdateUI();  // Update the UI after gravity flip
        } 
    }

    IEnumerator SmoothRotate(float angle)
    {
        isRotating = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // 确保最终角度准确
        isRotating = false;
    }

    // Update the UI to display current counters
    void UpdateUI()
    {
        if (rotationCounterText != null)
        {
            rotationCounterText.text = "Rotations Left: " + remainingRotations;
        }

        if (gravityCounterText != null)
        {
            gravityCounterText.text = "Gravity Flips Left: " + remainingGravityFlips;
        }
    }

    public void DisableRotation()
    {
        canRotate = false;
    }

}
