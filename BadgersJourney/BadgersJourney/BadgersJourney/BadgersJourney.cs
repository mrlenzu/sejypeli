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
    double liikutaVasemmalle = -1000;
    double liikutaOikealle = 1000;
    public override void Begin()
    {
        MediaPlayer.Play("musa");
        //Gravity = new Vector(0.0, -800.0);
        LuoKentta();
        LuoNappaimet();
        Camera.Follow(pelaaja);
        Camera.Zoom(0.7);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoKentta()
    {



        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta");


        ruudut.SetTileMethod(Color.FromHexCode("42FF3F"), LuoPelaaja);
        ruudut.SetTileMethod(Color.Black, LuoTaso);
        ruudut.SetTileMethod(Color.Blue, LuoTahti);
        ruudut.SetTileMethod(Color.Red, LuoVihollinen);

        ruudut.Execute(20, 20);
    }

    void LuoPelaaja(Vector paikka, double leveys, double korkeus)
    {
        

        pelaaja = new PlatformCharacter(32, 32);
        pelaaja.Shape = Shape.Circle;
        pelaaja.Mass = 10.0;
        Add(pelaaja);
        pelaaja.Image = olionKuva;
        pelaaja.Position = paikka;
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
    void LuoNappaimet()
    {
        Keyboard.Listen(Key.Left, ButtonState.Down, LiikutaPelaajaa, "liikuta Pelaajaa vasemmalle" , liikutaVasemmalle);
        Keyboard.Listen(Key.Right, ButtonState.Down, LiikutaPelaajaa,"liikuta pelaajaa oikealle", liikutaOikealle);
        Keyboard.Listen(Key.Up, ButtonState.Down, Hyppaa, "hyppaa");

    }
    void LiikutaPelaajaa(Double nopeus)
    {
        pelaaja.Walk(nopeus);
    }
    void Hyppaa()
    {
        pelaaja.Jump(500);
    }



}








