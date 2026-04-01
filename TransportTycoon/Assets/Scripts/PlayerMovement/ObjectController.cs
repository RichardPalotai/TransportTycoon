using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectController : MonoBehaviour
{
    public static ObjectController instance;
    void Update()
    {
        if (Mouse.current != null && Camera.main != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        CameraController.instance.followTransform = transform;
                        // GameViewModel.instance.SelectedObject = gameObject.GetComponent<DataScript>; SOME DATA SCRIPT WHICH HAS THE OBJECT INFO
                    }
                }
            }
        }
    }
}
