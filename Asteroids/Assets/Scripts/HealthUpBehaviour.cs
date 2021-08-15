
public class HealthUpBehaviour : PickUpBehaviour
{
    protected override void OnPlayerCollision()
    {
        GameManager.Instance.ModifyHealth(1);
    }
}
