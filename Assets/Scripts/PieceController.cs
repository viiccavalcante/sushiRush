using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class PieceController : MonoBehaviour
{
    public Sprite imageCursor;
    public GameObject cursor;
    private bool isDragging = false;
    private OrdersController ordersController;
    public string name;

    void Awake()
    {
        ordersController = GameObject.Find("Orders").GetComponent<OrdersController>();
    }


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

        if (TryDeliverToOrder())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("ExtrasTray"))
            {
                transform.position = hit.collider.transform.position + new Vector3(0f, 0.1f, 0f);
                return;
            }
        }

        Destroy(gameObject);
    }

    bool TryDeliverToOrder()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        GraphicRaycaster raycaster = ordersController.GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("OrderNote"))
            {
                ordersController.ConsumePiece(name);
                Destroy(gameObject);
            
                return true; 
            }
        }

        return false; 
    }
}
