using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isDragging;
    private Vector3 startDragPos;
    public Vector2 borderMin;
    public Vector2 borderMax;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }

        if (isDragging)
        {
            DragCamera();
        }
    }

    private void StartDragging()
    {
        isDragging = true;
        startDragPos = GetGroundIntersection();
    }

    private void StopDragging()
    {
        isDragging = false;
    }

    private void DragCamera()
    {
        Vector3 currentDragPos = GetGroundIntersection();
        Vector3 cameraMoveDistance = startDragPos - currentDragPos;
        Vector3 newPosition = transform.position + cameraMoveDistance;
        // Clamp camera position within the specified borders
        newPosition.x = Mathf.Clamp(newPosition.x, borderMin.x, borderMax.x);
        newPosition.z = Mathf.Clamp(newPosition.z, borderMin.y, borderMax.y);
        transform.position = newPosition;
    }

    private Vector3 GetGroundIntersection()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePositionScreen);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float hitDistance;
        
        if (groundPlane.Raycast(ray, out hitDistance))
        {
            return ray.GetPoint(hitDistance);
        }

        return Vector3.zero;
    }
}