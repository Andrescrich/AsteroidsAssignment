public class PowerUpBehaviour : PickUpBehaviour
{
    protected override void OnPlayerCollision() => GameManager.Instance.PowerUpBuff();

}
