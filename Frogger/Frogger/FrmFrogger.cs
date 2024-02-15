using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Frogger
{
    public partial class FrmFrogger : Form
    {

        private SoundPlayer deathsound = new SoundPlayer("death_effect.wav");
        private SoundPlayer soundPlayer = new SoundPlayer("move_sound.wav");
        static int anzahlBereiche = 6;

        static int anzahlBereicheY = 6;
        static int anzahlBereicheX = 12;

        // -1 ist ein Platzhalter
        int breite = -1;
        int hoehe = -1;
        int hoeheJeBereich = -1;
        int breiteJeBereich = -1;
        Rectangle[] alleBahnen = new Rectangle[anzahlBereicheY];
        Bahn[] alleBahnenCS = new Bahn[anzahlBereicheY];
        List<Hindernis> alleHindernisse = new List<Hindernis>();
        Rectangle spieler;
        int spawnRate = 18;
        int spawnZaehler = 0;
        Random rndBahn = new Random();
        int winCounter = 0;
        int speed = 5;
        TextBox winsAnzeigen = new TextBox();
        Point nullPunkt = new Point(1000, 1000);
        

        public FrmFrogger()
        {
            InitializeComponent();
        }

        private void FrmFrogger_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void WinsAnzeigen()
        {
            winsAnzeigen.Location = nullPunkt;
            winsAnzeigen.Text = ("Wins: " + Convert.ToString(winCounter));
            winsAnzeigen.Show();
        }

        private void FrmFrogger_Paint(object sender, PaintEventArgs e)
        {
            WinsAnzeigen();
            

            if (tmrGameTick.Enabled == false)
            {
                breite = this.ClientSize.Width;
                hoehe = this.ClientSize.Height;

                hoeheJeBereich = hoehe / anzahlBereicheY;

                breiteJeBereich = breite / anzahlBereicheX;

                //Puffer für Rundungsfehler
                hoeheJeBereich = hoeheJeBereich + 2;

                //Puffer für Rundungsfehler
                breiteJeBereich = breiteJeBereich + 2;

                spieler = new Rectangle((breite / 2) - 15, hoehe - 35, 30, 30);

                //for (int i = 0; i < alleBahnen.Length; i++)
                //{
                //    alleBahnen[i] = new Rectangle(0, i * hoeheJeBereich, breite, hoeheJeBereich);
                //}
                for (int i = 0; i < alleBahnenCS.Length; i++)
                {
                    alleBahnen[i] = new Rectangle(0, i * hoeheJeBereich, breite, hoeheJeBereich);
                }

                tmrGameTick.Start();
            }
  

            SolidBrush brStart = new SolidBrush(Color.LightBlue);
            SolidBrush brZiel = new SolidBrush(Color.LightYellow);
            SolidBrush brBahnHell = new SolidBrush(Color.LightGray);
            SolidBrush brBahnDunkel = new SolidBrush(Color.Gray);
            SolidBrush brSpieler = new SolidBrush(Color.Green);
            Pen pnRand = new Pen(Color.Black, 1);

            e.Graphics.FillRectangles(brBahnHell, alleBahnen);

            // FERTIG: Bahnen sollen sich in der Farbe abwechseln (brBahnHell, brBahnDunkel)

            e.Graphics.FillRectangle(brZiel, alleBahnen[0]);
            e.Graphics.FillRectangle(brStart, alleBahnen[alleBahnen.Length-1]);
            
            e.Graphics.DrawRectangles(pnRand, alleBahnen);

            for (int i = 1; i < alleBahnen.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    e.Graphics.FillRectangle(brBahnHell, alleBahnen[i]);
                }
                else
                {
                    e.Graphics.FillRectangle(brBahnDunkel, alleBahnen[i]);
                }
            }

            foreach (Hindernis aktuellesHindernis in alleHindernisse)
            {
                e.Graphics.FillRectangle(
                    aktuellesHindernis.Brush,
                    aktuellesHindernis.X,
                    aktuellesHindernis.Y,
                    aktuellesHindernis.Width,
                    aktuellesHindernis.Height);
            }

            if (alleBahnen[0].Contains(spieler))
            {
                winCounter++;
                spieler = new Rectangle((breite / 2) - 15, hoehe - 35, 30, 30);
                //MessageBox.Show($"Counter: " + Convert.ToString(winCounter));
                speed = speed + winCounter;
            }
         

            e.Graphics.FillEllipse(brSpieler, spieler);


        }

        private void tmrGameTick_Tick(object sender, EventArgs e)
        {
            spawnZaehler++;
            if(spawnRate <= 7)
            {
                spawnRate = 7;
            }

            if(spawnZaehler == spawnRate)
            {
                spawnZaehler = spawnZaehler + 1 + winCounter;
                spawnRate = spawnRate - winCounter;
                spawnZaehler = 0;

                int zufall = rndBahn.Next(1, anzahlBereicheY-1);
                int yWertDerBahn = alleBahnen[zufall].Top;

                alleHindernisse.Add(new Hindernis(breite, yWertDerBahn, 60, hoeheJeBereich, speed, Color.Red));
            }

            foreach (Hindernis aktuellesHindernis in alleHindernisse)
            {
                aktuellesHindernis.Move();
            }

            for(int i = alleHindernisse.Count -1; i >= 0; i--)
            {
                

                if ((alleHindernisse[i].X + alleHindernisse[i].Width) < 0)
                {
                    alleHindernisse.RemoveAt(i);

                }
            }

            // TODO Kontrollieren, ob Spieler überfahren wurde.

            for (int i = 0; i < alleHindernisse.Count; i++)
            {
                Rectangle rect = new Rectangle(alleHindernisse[i].X, alleHindernisse[i].Y, alleHindernisse[i].Width, alleHindernisse[i].Height);
                
                if (rect.Contains(spieler.X, spieler.Y) || rect.Contains(spieler.X + spieler.Width,spieler.Y +  spieler.Height))
                {
                    //MessageBox.Show("nigga");
                    deathsound.Play();
                    spieler = new Rectangle((breite / 2) - 15, hoehe - 35, 30, 30);
                   

                }


            }

      
            this.Refresh();
        }

        private void FrmFrogger_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO Spieler darf nicht nach unten aus dem Spielfeld laufen
            // TODO Weitere Bewegungen (links, rechts) einbauen
            // TODO Wenn Spieler im Ziel ist, soll er wieder auf Start zurückgesetzt werden und das Spiel soll schwerer werden
            if (e.KeyCode == Keys.Up)
            {
                spieler.Y = spieler.Y - hoeheJeBereich;
                soundPlayer.Play();
            }

            if(e.KeyCode == Keys.Down)
            {
                spieler.Y = spieler.Y + hoeheJeBereich;
                soundPlayer.Play();
            }

            if (e.KeyCode == Keys.Left)
            {
                spieler.X = spieler.X - breiteJeBereich;
                soundPlayer.Play();
            }

            if (e.KeyCode == Keys.Right)
            {
                spieler.X = spieler.X + breiteJeBereich;
                soundPlayer.Play();
            }

            if (e.KeyCode == Keys.W)
            {
                spieler.Y = spieler.Y - hoeheJeBereich;
                soundPlayer.Play();
            }

            if (e.KeyCode == Keys.S)
            {
                spieler.Y = spieler.Y + hoeheJeBereich;
                soundPlayer.Play();
            }

            if (e.KeyCode == Keys.A)
            {
                spieler.X = spieler.X - breiteJeBereich;
                soundPlayer.Play();
            }

            if (e.KeyCode == Keys.D)
            {
                spieler.X = spieler.X + breiteJeBereich;
                soundPlayer.Play();
            }
            //Rechte Bande
            if (spieler.X + spieler.Width >=  breite)
            {

                spieler = new Rectangle(breite -30, spieler.Y, 30, 30);


            }
            //Linke Bande
            if (spieler.X  <=0)
            {

                spieler = new Rectangle(breite - this.ClientSize.Width, spieler.Y, 30, 30);


            }
            // Untere Bande 
            if (spieler.Y > hoehe)
            {
                
                spieler = new Rectangle((spieler.X), hoehe - 35, 30, 30);


            }

            this.Refresh();
        }
    }
}
