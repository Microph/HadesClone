using UnityEngine;

[ExecuteInEditMode]
public class IsometricProjection : MonoBehaviour
{
        private Quaternion savedRotation;

        void OnWillRenderObject()
        {
            savedRotation = transform.rotation;
            var eulers = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(45, eulers.y, eulers.z); //TODO: extract to variable
        }

        void OnRenderObject()
        {
            transform.rotation = savedRotation;
        }
}