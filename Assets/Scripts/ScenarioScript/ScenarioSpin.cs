using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSpin : MonoBehaviour
{

    private float rotationAngle = 45f;
    private float rotationDuration = 0.5f;
    private bool isRotating = false;
    private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!canRotate)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A) && !isRotating)
        {
            StartCoroutine(SmoothRotate(rotationAngle)); 
        }
        if (Input.GetKeyDown(KeyCode.D) && !isRotating)
        {
            StartCoroutine(SmoothRotate(-rotationAngle));
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

    public void DisableRotation()
    {
        canRotate = false;
    }

}
