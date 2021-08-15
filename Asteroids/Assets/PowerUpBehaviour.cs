public class PowerUpBehaviour : PickUpBehaviour
{
    protected override void OnPlayerCollision() => SpaceshipController.Instance.PowerUpBuff();

}
