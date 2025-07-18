using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public GameObject sushiPrefab;     
    public GameObject nigiriSalmonPrefab; 
    public GameObject nigiriPinkPrefab;
    public GameObject nigiriRedPrefab;
    public GameObject cursor;
    public Transform trayTransform;
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

    private void OnMouseUp()
    {
        isDragging = false;
        Cursor.visible = true;

        if (cursor != null)
        {
            Destroy(cursor);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("WoodTray"))
        {
            WoodTrayController tray = hit.collider.GetComponent<WoodTrayController>();
            if (tray == null) return;

            tray.ClearTray();

            if (tray.ContainsNori())
            {
                for (int i = 0; i < 6; i++)
                    Instantiate(sushiPrefab, trayTransform.position + new Vector3(0, 0, -0.8f + i * 0.3f), Quaternion.identity);
            }
            else
            {
                string fish = tray.GetFishType();
                GameObject nigiriPrefab = nigiriSalmonPrefab;

                if (fish == "red")
                {
                    nigiriPrefab = nigiriRedPrefab;
                }

                if (fish == "pink")
                {
                    nigiriPrefab = nigiriPinkPrefab;
                }

                for (int i = 0; i < 6; i++)
                    Instantiate(nigiriPrefab, trayTransform.position + new Vector3(0, 0, -0.8f + i * 0.3f), Quaternion.Euler(0, 90, 0));
            }
        }
    }
    
}
