using UnityEngine;

[ExecuteInEditMode]
public class IsometricProjection : MonoBehaviour
{
        private void Update()
        {
            var eulers = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(45, eulers.y, eulers.z); //TODO: extract to variable
        }
}