using System;
using System.IO;
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
using System.Globalization;


namespace ChristiansOeApp
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();

        }

        GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        Attraction[] attractions;


        private void Form1_Load(object sender, EventArgs e)
        {
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SelectVoice("Microsoft Helle");

            attractions = new Attraction[] {
                new Attraction("Store Tårn", new GeoCoordinate(55.3205708509138, 15.186948054765407), "storeTaarn"),
                new Attraction("Lille Tårn", new GeoCoordinate(55.3220816185224, 15.183708648251965), "lilleTaarn"),
                new Attraction("Fængslet Ballonen", new GeoCoordinate(55.31996652774902, 15.184520396488354), "faengsletBallonen"),
                new Attraction("Kongens Bastion", new GeoCoordinate(55.317781715389316, 15.188406982107372), "kongensBastion")
            };

        }

        //Stories
        private void stories_Click(object sender, EventArgs e)
        {
            //Shows right elements
            storiesPage(true);
            mapPage(false);
            backToShipPage(false);

            watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += (sender1, args) => nearbyAttractions(args.Position.Location);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));


            void nearbyAttractions(GeoCoordinate coord)
            {
                Attraction nearbyAttraction = checkDistances(coord);

                if(nearbyAttraction != null)
                {
                    nearbyAttractionButton.Text = nearbyAttraction.Name;
                    nearbyAttractionButton.Enabled = true;
                }else {
                    nearbyAttractionButton.Text = "Ingen fortællinger i nærheden";
                    nearbyAttractionButton.Enabled = false;
                }
                
            }
            



        }
        private Attraction checkDistances(GeoCoordinate coord)
        {
            foreach (Attraction attraction in attractions)
            {
                double distance = coord.GetDistanceTo(attraction.Coords);
                if (distance <= 50)
                {
                    return attraction;
                }
            }
            return null;
        }

        private void resumeSpeechButton_Click(object sender, EventArgs e)
        {
            synthesizer.Resume();
        }

        private void pauseSpeechButton_Click(object sender, EventArgs e)
        {
            synthesizer.Pause();
        }

        private void stopSpeech_Click(object sender, EventArgs e)
        {
            synthesizer.SpeakAsyncCancelAll();
        }

        private void playStory(string name)
        {
            string path = Directory.GetCurrentDirectory();
            string story = File.ReadAllText(path + "/" + name + ".txt");

            synthesizer.SpeakAsync(story);

        }

        private void aboutChr_Click(object sender, EventArgs e)
        {
            playStory("aboutChr");
        }

        private void nearbyAttractionButton_Click(object sender, EventArgs e)
        {
            GeoCoordinateWatcher watcher2 = new GeoCoordinateWatcher();
            GeoCoordinate deviceCoord = watcher.Position.Location;
            playStory(checkDistances(deviceCoord).FileName);
            watcher2.Dispose();
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
            GeoCoordinate deviceCoord = watcher.Position.Location;

            //If device can't get a location
            if (deviceCoord.IsUnknown == true)
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
                deviceCoord = new GeoCoordinate(x.Position.Location.Latitude, x.Position.Location.Longitude);

                double distance = Math.Round(deviceCoord.GetDistanceTo(distanceToDock), 0);
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
            stopSpeech.Visible = visible;
            resumeSpeechButton.Visible = visible;
            pauseSpeechButton.Visible = visible;
            aboutChr.Visible = visible;
            nearbyAttractionButton.Visible = visible;
            nearbyStoriesLabel.Visible = visible;

        }

        private void backToShipPage(bool visible)
        {
            distToShip.Visible = visible;
            timeToShip.Visible = visible;
        }

        //Classes
        public class Attraction
        {
            public Attraction(string name, GeoCoordinate coords, string fileName)
            {
                Name = name;
                Coords = coords;
                FileName = fileName;
            }

            public string Name { get; }
            public GeoCoordinate Coords { get; }
            public string FileName { get; }
        }

        private void mapPicture_Click(object sender, EventArgs e)
        {

        }
    }
}
