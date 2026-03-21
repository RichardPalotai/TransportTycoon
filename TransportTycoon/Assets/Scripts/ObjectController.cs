using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectController : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current != null && Camera.main != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray))
                {
                    CameraController.instance.followTransform = transform;
                }
            }
        }
    }
}
