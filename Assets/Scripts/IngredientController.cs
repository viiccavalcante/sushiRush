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
        Cursor.visible = false;
        cursor = CursorController.Instance.CreateCursor(imageCursor);
    }

    void Update()
    {
        if (isDragging && cursor != null)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                CursorController.Instance.canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main,
                out Vector3 worldPos
            );

            cursor.transform.position = worldPos;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Cursor.visible = true;

        // if (cursor != null)
        // {
        //     Destroy(cursor);
        // }

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
