using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float panBorderThickness = 10f;
    public float minY = 10f;
    public float maxY = 80f;
    public float scrollSpeed = 5f; 

    void Update()
    {
        if (GameManager.GameIsOver || GameManager.GameIsWin)
        {
            enabled = false;
            return;
        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            Vector3 newPos = transform.position;
            newPos += new Vector3(-touchDeltaPosition.x, 0, -touchDeltaPosition.y) * moveSpeed / 2 * Time.deltaTime;

            
            newPos.x = Mathf.Clamp(newPos.x, 0f, 70f);
            newPos.z = Mathf.Clamp(newPos.z, 0f, 70f);

            transform.position = newPos;
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 pos = transform.position;
            pos.y += deltaMagnitudeDiff * moveSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * 10 * Time.deltaTime;
        transform.Translate(movement, Space.World);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0f, 70f);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, 0f, 70f);
        transform.position = clampedPosition;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 scrollMovement = transform.forward * scrollInput * scrollSpeed * 1000 * Time.deltaTime;
        transform.position += scrollMovement;
    }
}
