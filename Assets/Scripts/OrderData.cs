using UnityEngine;
using TMPro;

public class OrderData : MonoBehaviour
{
    [System.Serializable]
    public class SushiOrder
    {
        public SushiData sushi;
        public int amount;
    }

    public SushiOrder[] itens = new SushiOrder[2];

    public OrdersController ordersController;

    void Awake()
    {
        if (ordersController == null)
        {
            GameObject ordersGO = GameObject.Find("Orders");
            if (ordersGO != null)
                ordersController = ordersGO.GetComponent<OrdersController>();
        }
    }

    public void UpdateUI()
    {
        Transform orderLine = transform.Find("OrderLine");
        Transform orderLine2 = transform.Find("OrderLine2");

        if (orderLine == null || orderLine2 == null)
        {
            return;
        }

        if (itens[0].amount <= 0)
            orderLine.gameObject.SetActive(false);
        else
            orderLine.Find("Amount").GetComponent<TextMeshProUGUI>().text = $"x{itens[0].amount}";

        if (itens[1].amount <= 0)
            orderLine2.gameObject.SetActive(false);
        else
            orderLine2.Find("Amount").GetComponent<TextMeshProUGUI>().text = $"x{itens[1].amount}";

        // Se ambas as linhas acabaram, deleta o pedido
        if (itens[0].amount <= 0 && itens[1].amount <= 0 && ordersController != null)
        {
            ordersController.DeleteOrder(gameObject);
        }
    }
}
