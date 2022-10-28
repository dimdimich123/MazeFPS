using UnityEngine;

public static class CanvasGroupExtension
{
    public static void Open(this CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public static void Close(this CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
