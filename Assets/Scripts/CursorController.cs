using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;
    public GameObject cursorPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject CreateCursor(Sprite sprite)
    {
        GameObject obj = Instantiate(cursorPrefab);
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
        return obj;
    }
}
