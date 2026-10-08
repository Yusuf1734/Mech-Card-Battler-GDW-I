using UnityEngine;
using UnityEngine.InputSystem;

// lets the player pan and zoom the existing 2D board view.
[RequireComponent(typeof(Camera))]
public class BoardCameraPanZoom : MonoBehaviour
{
    [SerializeField] private Camera boardCamera;
    [SerializeField] private float zoomSensitivity = 0.18f;
    [SerializeField] private float minOrthographicSize = 3f;
    [SerializeField] private float maxOrthographicSize = 30f;

    private bool isPanning;

    private void Awake()
    {
        if (boardCamera == null)
            boardCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null || boardCamera == null || !boardCamera.orthographic)
            return;

        if (mouse.rightButton.wasPressedThisFrame)
            isPanning = true;

        if (!mouse.rightButton.isPressed)
            isPanning = false;

        if (isPanning)
        {
            Vector2 pointerDelta = mouse.delta.ReadValue();
            float worldUnitsPerPixel = (2f * boardCamera.orthographicSize) / Screen.height;
            Vector3 cameraMovement =
                (boardCamera.transform.right * pointerDelta.x +
                 boardCamera.transform.up * pointerDelta.y) * worldUnitsPerPixel;

            // move the camera opposite to the pointer so the board follows the drag.
            boardCamera.transform.position -= cameraMovement;
        }

        float wheel = mouse.scroll.ReadValue().y;
        if (Mathf.Abs(wheel) > 10f)
            wheel /= 120f;

        if (Mathf.Abs(wheel) > 0.001f)
        {
            boardCamera.orthographicSize = Mathf.Clamp(
                boardCamera.orthographicSize * Mathf.Exp(-wheel * zoomSensitivity),
                minOrthographicSize,
                maxOrthographicSize);
        }
    }
}
