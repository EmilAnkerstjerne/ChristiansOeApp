using System;
using System.IO;
using System.Windows.Forms;
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
        private void Stories_Click(object sender, EventArgs e)
        {
            //Shows right elements
            StoriesPage(true);
            MapPage(false);
            BackToShipPage(false);

            watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += (sender1, args) => NearbyAttractions(args.Position.Location);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));


            void NearbyAttractions(GeoCoordinate coord)
            {
                Attraction nearbyAttraction = CheckDistances(coord);

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
        private Attraction CheckDistances(GeoCoordinate coord)
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

        private void ResumeSpeechButton_Click(object sender, EventArgs e)
        {
            synthesizer.Resume();
        }

        private void PauseSpeechButton_Click(object sender, EventArgs e)
        {
            synthesizer.Pause();
        }

        private void StopSpeech_Click(object sender, EventArgs e)
        {
            synthesizer.SpeakAsyncCancelAll();
        }

        private void PlayStory(string name)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + @"Properties\";
            string story = File.ReadAllText(path + @"\" + name + ".txt");

            synthesizer.SpeakAsync(story);
            synthesizer.Resume();

        }

        private void AboutChr_Click(object sender, EventArgs e)
        {
            PlayStory("aboutChr");
        }

        private void NearbyAttractionButton_Click(object sender, EventArgs e)
        {
            GeoCoordinateWatcher watcher2 = new GeoCoordinateWatcher();
            GeoCoordinate deviceCoord = watcher.Position.Location;
            PlayStory(CheckDistances(deviceCoord).FileName);
            watcher2.Dispose();
        }



        //Map
        private void MapButton_Click(object sender, EventArgs e)
        {
            //Shows right elements
            MapPage(true);
            StoriesPage(false);
            BackToShipPage(false);

            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(Path.GetFullPath(Path.Combine(path, @"..\..\")) + @"Properties\");



        }

        //Back to ship
        private void BackToShipButton_Click(object sender, EventArgs e)
        {
            //Shows right elements
            BackToShipPage(true);
            MapPage(false);
            StoriesPage(false);


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
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(Watcher_PositionChanged);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            
            //Method for displaying the distance from the device's location to the docks
            void Watcher_PositionChanged(object sender1, GeoPositionChangedEventArgs<GeoCoordinate> x)
            {
                deviceCoord = new GeoCoordinate(x.Position.Location.Latitude, x.Position.Location.Longitude);

                double distance = Math.Round(deviceCoord.GetDistanceTo(distanceToDock), 0);
                distToShip.Text = distance.ToString() + " meter til færgen.";
                timeToShip.Text = (Math.Round((distance/3000)*60, 0)).ToString() + " minutter til færgen.";

            }
        }


        

        //Pages
        private void MapPage(bool visible)
        {
            mapPicture.Visible = visible;
        }

        private void StoriesPage(bool visible)
        {
            stopSpeech.Visible = visible;
            resumeSpeechButton.Visible = visible;
            pauseSpeechButton.Visible = visible;
            aboutChr.Visible = visible;
            nearbyAttractionButton.Visible = visible;
            nearbyStoriesLabel.Visible = visible;

        }

        private void BackToShipPage(bool visible)
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

        private void MapPicture_Click(object sender, EventArgs e)
        {

        }
    }
}
