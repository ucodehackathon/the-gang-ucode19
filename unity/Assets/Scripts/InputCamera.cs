using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class InputCamera : MonoBehaviour
{

    private const float hSens = 0.4f;
    private const float vSens = 0.4f;

    private const float h2Sens = 1;
    private const float v2Sens = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 newPosition = this.transform.localPosition + (hSens * h * this.transform.right) + (vSens * v * this.transform.forward);
        this.transform.localPosition = newPosition;
    }

    private void UpdateRotation()
    {
        float h2 = Input.GetAxis("JYRightHorizontal");
        float v2 = Input.GetAxis("JYRightVertical");

        Vector3 newRotation = new Vector3(trueModulus(this.transform.localEulerAngles.x + h2 * h2Sens, 360), trueModulus(this.transform.localEulerAngles.y + v2 * v2Sens, 360), 0);

        this.transform.localEulerAngles = newRotation;
    }

    float trueModulus(float a, float b)
    {
        return (float) (a - b * System.Math.Floor(a / b));
    }
}
