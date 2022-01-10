using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Device.Location;
using System.Speech.Synthesis;


namespace ChristiansOeApp
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();

        }

        GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        //Stories
        private void stories_Click(object sender, EventArgs e)
        {
            //Shows right elements
            storiesPage(true);
            mapPage(false);
            backToShipPage(false);

            var synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.Speak("All we need to do is to make sure we keep talking");




        }

        //Map
        private void mapButton_Click(object sender, EventArgs e)
        {
            //Shows right elements
            mapPage(true);
            storiesPage(false);
            backToShipPage(false);

        }

        //Back to ship
        private void backToShipButton_Click(object sender, EventArgs e)
        {
            //Shows right elements
            backToShipPage(true);
            mapPage(false);
            storiesPage(false);
            
       
            //ChristiansOe dock coordinates
            GeoCoordinate distanceToDock = new GeoCoordinate(55.320769, 15.186029);
            //Device coordinates
            GeoCoordinate coord = watcher.Position.Location;

            //If device can't get a location
            if (coord.IsUnknown == true)
            {
                distToShip.Text = "Kan ikke finde lokation.";
                timeToShip.Text = "";
            }

            //Position changed
            watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            
            //Method for displaying the distance from the device's location to the docks
            void watcher_PositionChanged(object sender1, GeoPositionChangedEventArgs<GeoCoordinate> x)
            {
                coord = new GeoCoordinate(x.Position.Location.Latitude, x.Position.Location.Longitude);
                double distance = Math.Round(coord.GetDistanceTo(distanceToDock), 0);
                distToShip.Text = distance.ToString() + " meter til færgen.";
                timeToShip.Text = (Math.Round((distance/3000)*60, 0)).ToString() + " minutter til færgen.";

            }
        }

        //Pages
        private void mapPage(bool visible)
        {
            mapPicture.Visible = visible;
        }

        private void storiesPage(bool visible)
        {

        }

        private void backToShipPage(bool visible)
        {
            distToShip.Visible = visible;
            timeToShip.Visible = visible;
        }

        
    }
}
