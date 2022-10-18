using UnityEngine;
using UnityEngine.EventSystems;
using GameCore.CommonLogic;

public class JoystickUI : MonoBehaviour, IUIMovementController, IDragHandler, IEndDragHandler
{
    private const float _scaleFactor = 0.073f;
    private const float _defaultMoveRadius = 140f;

    [SerializeField] private Transform _handle;
    [SerializeField] private UIMovementControllerTypeId _uiMovementControlleId;

    public UIMovementControllerTypeId UIMovementControlleId { get => _uiMovementControlleId; set => _uiMovementControlleId = value; }
    public float AxisX { get; set; } = 0;
    public float AxisY { get; set; } = 0;

    private float _moveRadius;
    private RectTransform _transform;
    private Vector2 _position;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void Init(float screenWidth)
    {
        _moveRadius = screenWidth * _scaleFactor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _position = eventData.position - (Vector2)_transform.position;
        _handle.position = _transform.position + Vector3.ClampMagnitude(_position, _moveRadius);
        AxisX = _handle.localPosition.x / _defaultMoveRadius;
        AxisY = _handle.localPosition.y / _defaultMoveRadius;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _handle.localPosition = Vector3.zero;
        AxisX = 0;
        AxisY = 0;
    }
}
