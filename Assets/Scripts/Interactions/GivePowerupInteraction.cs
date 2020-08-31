using System;
using Core;
using Interactions;
using Player;
using Ui;

public class GivePowerupInteraction : Interaction
{
    public enum Powerup
    {
        Speed,
        Jump,
        Win
    }

    public Powerup powerup;
    
    public override void Interact()
    {
        switch (powerup)
        {
            case Powerup.Speed:
                PlayerController.SetSpeed(10);
                break;
            case Powerup.Jump:
                PlayerController.SetJump(8);
                break;
            case Powerup.Win:
                GameInitialization.GameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
