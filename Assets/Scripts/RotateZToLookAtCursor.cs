using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZToLookAtCursor : MonoBehaviour
{
    void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mouseScreenPosition - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(45f, 0.0f, rotationZ); //TODO: extract to variable
    }
}
