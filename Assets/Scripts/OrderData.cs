using UnityEngine;

public class OrderData : MonoBehaviour
{
    [System.Serializable]
    public class SushiOrder
    {
        public SushiData sushi;
        public int amount;
    }

    public SushiOrder[] itens = new SushiOrder[2];
}
