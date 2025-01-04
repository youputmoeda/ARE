using UnityEngine;

public static class SetCursorStateScript
{
    public static void SetCursorState(bool isCursorVisible)
    {
        if (isCursorVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}