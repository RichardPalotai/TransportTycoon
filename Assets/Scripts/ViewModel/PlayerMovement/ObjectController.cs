using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectController : MonoBehaviour
{
    public static ObjectController instance;
    
    [SerializeField]
    public GridObject ObjScript;

    void Update()
    {
        if (Mouse.current != null && Camera.main != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && GameViewModel.instance.Gamemode == GameViewModel.GameMode.MOUSE && !GameViewModel.instance.IsRouteDisplayOn)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform);
                    if (hit.transform == transform)
                    {
                        CameraController.instance.followTransform = transform;
                        // TODO - SOME DATA SCRIPT WHICH HAS THE OBJECT INFO <BINDING>
                        GameViewModel.instance.SelectObject(ObjScript.modelSelf);
                    }
                }
            }
        }
    }
}
