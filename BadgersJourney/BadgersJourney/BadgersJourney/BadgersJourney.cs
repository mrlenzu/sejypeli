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
    Image keksinkuva = LoadImage("keksi");
    Image vihollisenkuva = LoadImage("mallerssi");
    Image tekstinkuva = LoadImage("teksti");
    PlatformCharacter pelaaja;
    double liikutaVasemmalle = -1000;
    double liikutaOikealle = 1000;
    List<Label> valikonKohdat;
    public override void Begin()
    {
        MediaPlayer.Play("gm");
        Gravity = new Vector(0.0, -900.0);
        LuoKentta();
        LuoNappaimet();
        LuoElamalaskuri();
        Camera.Follow(pelaaja);
        Camera.Zoom(0.8);
        Level.Background.Color = Color.LightYellow;
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        
    }
    void LuoKentta()
    {



        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta");


        ruudut.SetTileMethod(Color.FromHexCode("42FF3F"), LuoPelaaja);
        ruudut.SetTileMethod(Color.Black, LuoTaso);
        ruudut.SetTileMethod(Color.FromHexCode("0026FF"), Luokeksi);
        ruudut.SetTileMethod(Color.Red, LuoVihollinen);

        ruudut.Execute(20, 20);
    }

    void LuoPelaaja(Vector paikka, double leveys, double korkeus)
    {
        

        pelaaja = new PlatformCharacter(32, 32);
        pelaaja.Shape = Shape.Circle;
        pelaaja.Mass = 10.0;
        Add(pelaaja);
        pelaaja.Tag = "badger1";
        pelaaja.Image = olionKuva;
        pelaaja.Position = paikka;
        pelaaja.CollisionIgnoreGroup = 1;
        AddCollisionHandler(pelaaja, PelaajaTormasi);
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

    void Luokeksi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject keksi = new PhysicsObject(40, 40);
        //keksi.IgnoresCollisionResponse = true;
        keksi.Position = paikka;
        keksi.Image = keksinkuva;
        keksi.Tag = "keksi";
        AddCollisionHandler(keksi, Pelaajavoittaa);
        Add(keksi);
    }
    void TormaaTahteen(PhysicsObject tormaa, PhysicsObject tahti)
    {

    }
    void LuoVihollinen(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihollinen = new PhysicsObject(50, 50);
        //vihollinen.IgnoresCollisionResponse = true;
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
        Keyboard.Listen(Key.Space, ButtonState.Down, Isku, "iske");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LiikutaPelaajaa(Double nopeus)
    {
        pelaaja.Walk(nopeus);
    }
    void Hyppaa()
    {
        pelaaja.Jump(500);
    }
    void Isku()
    {
        SoundEffect iskuaani = LoadSoundEffect("burb");
        Sound pelaaja1Iskuaani = iskuaani.CreateSound();
        pelaaja1Iskuaani.Pitch = 0.9;
        pelaaja1Iskuaani.Play();
        PhysicsObject isku = new PhysicsObject(200, 60);
        isku.Position = pelaaja.Position;
        //isku.IgnoresPhysicsLogics = true;
        Add(isku);
        isku.LifetimeLeft = TimeSpan.FromSeconds(1.0);
        AddCollisionHandler(isku, osuma);
        //PhysicsStructure rakenne = new PhysicsStructure(pelaaja, isku);
        //Add(rakenne);
        isku.IgnoresCollisionWith(pelaaja);
        isku.CollisionIgnoreGroup = 1;
        isku.IsVisible = false;
    }
    void osuma(PhysicsObject tormaaja, PhysicsObject kohde)
    {
        if (kohde.Tag.ToString() == "mallerssi")
        {
            kohde.Destroy();
        }
    }
    IntMeter ElamaLaskuri;

    void LuoElamalaskuri()
    {
        ElamaLaskuri = new IntMeter(3);

        Label ElamaNaytto = new Label();
        ElamaNaytto.X = Screen.Left + 100;
        ElamaNaytto.Y = Screen.Top - 100;
        ElamaNaytto.TextColor = Color.Black;
        ElamaNaytto.Color = Color.White;

        ElamaNaytto.BindTo(ElamaLaskuri);
        Add(ElamaNaytto);
        ElamaNaytto.Title = "ëlämät";
    }
    void PelaajaTormasi(PhysicsObject tormaaja, PhysicsObject kohde)
    {
        if (kohde.Tag.ToString() == "mallerssi")
        {
            ElamaLaskuri.Value--;
        }

        if (ElamaLaskuri.Value == 0)
        {
            ClearAll();
            Valikko();
        }
    }
    void Valikko()
    {
        ClearAll();

        valikonKohdat = new List<Label>();

        Label kohta1 = new Label("Aloita uusi peli");
        kohta1.Position = new Vector(0, 40);
        valikonKohdat.Add(kohta1);

        Label kohta3 = new Label("Lopeta peli");
        kohta3.Position = new Vector(0, 0);
        valikonKohdat.Add(kohta3);


        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }
        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed,AloitaPeli , null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, Exit, null);
        Mouse.IsCursorVisible = true;
    }
    void AloitaPeli()
    {
        ClearAll();
        Begin();
    }
    void Pelaajavoittaa(PhysicsObject keksi, PhysicsObject kohde)
    {


        if (kohde.Tag.ToString() == "badger1")
        {
            ClearAll();
            LuoNappaimet();
            PhysicsObject teksti = PhysicsObject.CreateStaticObject(Screen.Width,Screen.Height);
            teksti.Image = tekstinkuva;
            Add(teksti);
        }
    }
}








