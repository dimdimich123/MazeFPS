using UnityEngine;

namespace UI.HUD
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class HUDView : MonoBehaviour, IPanel
    {
        [SerializeField] private CanvasGroup _canvas;

        public void Close()
        {
            _canvas.Close();
        }

        public void Open()
        {
            _canvas.Open();
        }
    }
}
