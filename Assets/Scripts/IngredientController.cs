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
             Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5f; // Distância da câmera até onde o cursor deve aparecer (ajuste conforme necessário)

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            cursor.transform.position = worldPos;
        }
    }

    void OnMouseUp()
{
    isDragging = false;
    Cursor.visible = true;

    if (cursor != null)
    {
        Destroy(cursor);
    }

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
    if (Physics.Raycast(ray, out RaycastHit hit))
        {
           
            if (hit.collider.CompareTag("WoodTray"))
            {
                Vector3 spawnPosition = hit.collider.transform.position + new Vector3(0f, 0.5f, 0f); // meio acima

                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
}

}
