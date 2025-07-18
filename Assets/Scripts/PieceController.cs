using UnityEngine;

public class PieceController : MonoBehaviour
{
    public Sprite imageCursor;
    public GameObject cursor;
    private bool isDragging = false;

    void OnMouseDown()
    {
        cursor = Utils.CursorOnMouseDown(ref isDragging, imageCursor);
    }

    void Update()
    {
        Utils.CursorUpdate(isDragging, cursor);
    }

    void OnMouseUp()
    {
        Utils.StopDragging(ref isDragging, ref cursor);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("ExtrasTray"))
            {
                Debug.log("aqui");
                transform.position = hit.collider.transform.position + new Vector3(0f, 0.1f, 0f);
                return;
            }
        }

    }
}
