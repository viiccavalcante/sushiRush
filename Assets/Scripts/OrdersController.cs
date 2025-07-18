using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class OrdersController : MonoBehaviour
{
    public int maxActiveOrders = 4;
    public float timeBetweenOrders = 35f;
    public Transform orders;
    public GameObject orderPrefab;
    public List<SushiData> sushiOptions;
    private List<GameObject> ActiveOrders = new();
    private float timer = 32f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenOrders)
        {
            timer = 0f;

            if (ActiveOrders.Count <= maxActiveOrders)
            {
                CreateOrder();
            }
        }
    }

    void CreateOrder()
    {
        GameObject newOrder = Instantiate(orderPrefab, orders);

        Transform orderLine = newOrder.transform.Find("OrderLine");
        Transform orderLine2 = newOrder.transform.Find("OrderLine2");

        List<SushiData> sushisSelected = new();
        while (sushisSelected.Count < 2)
        {
            var sushi = sushiOptions[Random.Range(0, sushiOptions.Count)];
            if (!sushisSelected.Contains(sushi))
                sushisSelected.Add(sushi);
        }

        int amount1 = Random.Range(1, 5);
        int amount2 = Random.Range(1, 5);

        orderLine.Find("Icon").GetComponent<Image>().sprite = sushisSelected[0].sprite;
        orderLine.Find("Amount").GetComponent<TextMeshProUGUI>().text = $"x{amount1}";

        orderLine2.Find("Icon").GetComponent<Image>().sprite = sushisSelected[1].sprite;
        orderLine2.Find("Amount").GetComponent<TextMeshProUGUI>().text = $"x{amount2}";

        OrderData data = newOrder.GetComponent<OrderData>();
        data.ordersController = this;
        data.itens[0] = new OrderData.SushiOrder { sushi = sushisSelected[0], amount = amount1 };
        data.itens[1] = new OrderData.SushiOrder { sushi = sushisSelected[1], amount = amount2 };

        ActiveOrders.Add(newOrder);
    }

    public void DeleteOrder(GameObject order)
    {
        if (ActiveOrders.Contains(order))
        {
            ActiveOrders.Remove(order);
            Destroy(order);
        }
    }

    public List<GameObject> GetActiveOrders()
    {
        return ActiveOrders;
    }

    public bool ConsumePiece(string pieceName)
    {
        foreach (GameObject orderObj in ActiveOrders)
        {
            OrderData data = orderObj.GetComponent<OrderData>();
            if (data != null)
            {
                for (int i = 0; i < data.itens.Length; i++)
                {
                    var item = data.itens[i];
                    if (item.sushi.name == pieceName && item.amount > 0)
                    {
                        item.amount--;
                        data.itens[i] = item;
                        data.UpdateUI();
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
