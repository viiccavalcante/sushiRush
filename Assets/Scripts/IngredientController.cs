using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public Sprite imageCursor;
    public GameObject prefab;
    private GameObject cursor;
    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;
        cursor = CursorController.Instance.CreateCursor(imageCursor);
    }

    void OnMouseDrag()
    {
        if (cursor != null && isDragging)
        {
            cursor.transform.position = Input.mousePosition;
        }
    }

    void OnMouseUp()
    {
        if (cursor != null)
        {
            Destroy(cursor);
        }

        isDragging = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("WoodTray"))
            {
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
        }
    }
}
