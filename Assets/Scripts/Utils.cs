using UnityEngine;

public static class Utils
{
    public static GameObject CursorOnMouseDown(ref bool isDragging, Sprite imageCursor)
    {
        isDragging = true;
        Cursor.visible = false;
       
        return CursorController.Instance.CreateCursor(imageCursor);
    }

    public static void CursorUpdate(bool isDragging, GameObject cursor)
    {
        if (isDragging && cursor != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5f;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            cursor.transform.position = worldPos;
        }
    }

    public static void StopDragging(ref bool isDragging, ref GameObject cursor)
    {
        isDragging = false;
        Cursor.visible = true;

        if (cursor != null)
        {
            Object.Destroy(cursor);
            cursor = null;
        }
    }
}
