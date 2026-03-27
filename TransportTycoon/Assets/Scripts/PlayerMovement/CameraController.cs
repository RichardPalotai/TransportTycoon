using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform followTransform; // Transform reference for Vehicle object
    public Transform cameraTransform;

    public float normalSpeed;
    public float fastSpeed;
    public float moveSpeed;
    public float moveTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            MouseInput();
            KeyboardInput();
            HandleMovementInput();
        }

        if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            followTransform = null;
        }
    }

    /// <summary>
    /// Handle mouse inputs and change the new position/rotation/zoom accordingly
    /// </summary>
    private void MouseInput()
    {
        if (Mouse.current != null)
        {
            Mouse mInput = Mouse.current;
            if (mInput.scroll.ReadValue().y != 0)
            {
                newZoom += mInput.scroll.ReadValue().y * zoomAmount;
            }

            if (mInput.leftButton.wasPressedThisFrame)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero); // Plane = An infinite surface defined by a Normal vector and a point in space
                Ray ray = Camera.main.ScreenPointToRay(mInput.position.ReadValue()); // Ray = Straight line in 3D space going from one Object to infinity

                float entry; // Distance between the ray's origin and the plane

                if (plane.Raycast(ray, out entry)) // Check if the ray intersects the plane
                {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }
            if (mInput.leftButton.isPressed)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(mInput.position.ReadValue());

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }

            if (mInput.rightButton.wasPressedThisFrame)
            {
                rotateStartPosition = mInput.position.ReadValue();
            }
            if (mInput.rightButton.isPressed)
            {
                rotateCurrentPosition = mInput.position.ReadValue();

                Vector3 difference = rotateStartPosition - rotateCurrentPosition;

                rotateStartPosition = rotateCurrentPosition;

                newRotation *= Quaternion.Euler(Vector3.up * (-difference.x/5f));
            }
        }
    }

    /// <summary>
    /// Handle keyboard inputs and change the new position/rotation/zoom accordingly
    /// </summary>
    private void KeyboardInput()
    {
        if (Keyboard.current != null)
        {
            Keyboard kInput = Keyboard.current;

            if (kInput.leftShiftKey.isPressed)
            {
                moveSpeed = fastSpeed;
            }
            else
            {
                moveSpeed = normalSpeed;
            }

            if (kInput.wKey.isPressed || kInput.upArrowKey.isPressed)
            {
                newPosition += (transform.forward * moveSpeed);
            }
            if (kInput.sKey.isPressed || kInput.downArrowKey.isPressed)
            {
                newPosition += (transform.forward * -moveSpeed);
            }
            if (kInput.dKey.isPressed || kInput.rightArrowKey.isPressed)
            {
                newPosition += (transform.right * moveSpeed);
            }
            if (kInput.aKey.isPressed || kInput.leftArrowKey.isPressed)
            {
                newPosition += (transform.right * -moveSpeed);
            }
            
            if (kInput.qKey.isPressed)
            {
                newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            }
            if (kInput.eKey.isPressed)
            {
                newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
            }

            if (kInput.rKey.isPressed)
            {
                newZoom += zoomAmount;
            }
            else if (kInput.fKey.isPressed)
            {
                newZoom -= zoomAmount;
            }
        }
    }

    /// <summary>
    /// Sets the current positions/rotations of the object to the changed positons/rotations with linear interpolation
    /// </summary>
    private void HandleMovementInput()
    {
        if (Keyboard.current != null || Mouse.current != null)
        {
            // Linear interpolations (t = Framerate time (seconds) * <some value>)
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveTime); // between vectors
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveTime); // between Quaternions = 3D rotations in math
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * moveTime); // between vectors
        }
    }
}
