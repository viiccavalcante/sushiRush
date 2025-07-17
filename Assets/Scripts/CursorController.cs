using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;
    public Canvas canvas;
    public GameObject cursorImagePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject CreateCursor(Sprite sprite)
    {
        GameObject obj = Instantiate(cursorImagePrefab, canvas.transform);
        obj.GetComponent<Image>().sprite = sprite;
        return obj;
    }
}
