using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class BadgersJourney : PhysicsGame
{
    Image olionKuva = LoadImage("badger1");
    public override void Begin()
    {
        PhysicsObject olio = new PhysicsObject(32, 32);
        olio.Shape = Shape.Circle;
        olio.Mass = 10.0;
        Add(olio);
        olio.Image = olionKuva;

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
   
}