namespace GameCore.CommonLogic
{
    public interface IUIMovementController
    {
        UIMovementControllerTypeId UIMovementControlleId { get; set; }
        float AxisX { get; set; }
        float AxisY { get; set; }
    }
}

