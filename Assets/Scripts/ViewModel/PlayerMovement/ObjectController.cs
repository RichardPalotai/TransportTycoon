using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectController : MonoBehaviour
{
    public static ObjectController instance;
    
    [SerializeField]
    public GridObject ObjScript;

    private bool _isCtrlPressed;

    void Update()
    {
        if (Keyboard.current != null && Mouse.current != null && Camera.main != null)
        {
            _isCtrlPressed = Keyboard.current.leftCtrlKey.isPressed;
            if (Mouse.current.leftButton.wasPressedThisFrame && GameViewModel.instance.Gamemode == GameViewModel.GameMode.MOUSE && !GameViewModel.instance.IsRouteDisplayOn)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform);
                    if (hit.transform == transform && !GameViewModel.instance.IsMouseOverUI())
                    {
                        if (_isCtrlPressed)
                            CameraController.instance.followTransform = transform;
                        // TODO - SOME DATA SCRIPT WHICH HAS THE OBJECT INFO <BINDING>
                        GameViewModel.instance.SelectObject(ObjScript.modelSelf);
                        Debug.Log("CLICKEEEEED " + ObjScript.modelSelf);
                    }
                }
            }
        }
    }
}
