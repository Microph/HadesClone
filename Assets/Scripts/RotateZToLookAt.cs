using UnityEngine;

public class RotateZToLookAt : MonoBehaviour
{
    public Transform targetTRA;
    
    void Update()
    {
        Vector3 difference = targetTRA.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
