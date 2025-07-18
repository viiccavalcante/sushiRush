using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public Sprite imageCursor;
    public GameObject prefab;
    private GameObject cursor;
    private bool isDragging = false;
    public string name = null;
    public WoodTrayController trayController;

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
            
                if (hit.collider.CompareTag("WoodTray"))
                {
                    trayController.AddIngredient(name);

                    Vector3 spawnPosition = hit.collider.transform.position + new Vector3(0f, 0.1f, 0f);
                    Instantiate(prefab, spawnPosition, Quaternion.identity);
                }
            }
    }

}
