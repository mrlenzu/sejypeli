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
    //Image tasonKuva = LoadImage("kentta");
    Image tahdenkuva = LoadImage("tähti");
    Image vihollisenkuva = LoadImage("mallerssi");
    PlatformCharacter pelaaja;
    PhysicsObject olio;
    public override void Begin()
    {
        

        LuoKentta();
        Camera.Follow(olio);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoKentta()
    {
        
        
        
        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta");

       
        ruudut.SetTileMethod(Color.Green, LuoPelaaja);
        ruudut.SetTileMethod(Color.Black, LuoTaso);
        ruudut.SetTileMethod(Color.Blue, LuoTahti);
        ruudut.SetTileMethod(Color.Red, LuoVihollinen);
        
        ruudut.Execute(20, 20);
    }

    void LuoPelaaja(Vector paikka, double leveys, double korkeus)
    {
         pelaaja = new PlatformCharacter(10, 10);
        pelaaja.Position = paikka;
        AddCollisionHandler(pelaaja, "tahti", TormaaTahteen);
        Add(pelaaja);

        olio = new PhysicsObject(32, 32);
        olio.Shape = Shape.Circle;
        olio.Mass = 10.0;
        Add(olio);
        olio.Image = olionKuva;
        olio.Position = paikka;
    }

    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Black;
        //taso.Image = tasonKuva;
        //taso.CollisionIgnoreGroup = 1;
        Add(taso);
        
    }

    void LuoTahti(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject tahti = new PhysicsObject(40, 40);
        tahti.IgnoresCollisionResponse = true;
        tahti.Position = paikka;
        tahti.Image = tahdenkuva;
        tahti.Tag = "tahti";
        Add(tahti, 1);
    }
    void TormaaTahteen(PhysicsObject tormaa, PhysicsObject tahti)
    {

    }
    void LuoVihollinen(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihollinen = new PhysicsObject(50, 50);
        vihollinen.IgnoresCollisionResponse = true;
        vihollinen.Position = paikka;
        vihollinen.Image = vihollisenkuva;
        vihollinen.Tag = "mallerssi";
        Add(vihollinen, 1);




    }
}









