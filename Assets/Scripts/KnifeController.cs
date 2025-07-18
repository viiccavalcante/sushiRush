using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public GameObject sushiPrefab;
    public GameObject cursor;
    public GameObject tray;
    public Sprite imageCursor;
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
            mousePosition.z = 5f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            cursor.transform.position = worldPos;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Cursor.visible = true;
        if (cursor != null)
            Destroy(cursor);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("WoodTray"))
            {
                DestroyAllIngredients();

                for (int i = 0; i < 6; i++)
                {
                    Vector3 pos = tray.transform.position + new Vector3(0, 0, i * 0.5f);
                    Instantiate(sushiPrefab, pos, Quaternion.identity);
                }
                
            }
        }
    }
    
    void DestroyAllIngredients()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ingredient");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}
