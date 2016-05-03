using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LegendofEveryoneElse {
    class Program {
        static string DataDirectory;
        static List<Scene> Scenes;
        static List<Cutscene> Cutscenes;
        static bool GameOver;
        static bool PlayAgain;

        static void Main(string[] args) {
            DataDirectory = Environment.CurrentDirectory + "\\data";

            // Exit if data directory cannot be found
            if (!Directory.Exists(DataDirectory)) {
                Console.WriteLine("Required directory does not exist: " + DataDirectory + "\n\nPress any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Console.Title = "Legend of Everyone Else";

            Scenes = new List<Scene>();
            Cutscenes = new List<Cutscene>();
            GameOver = false;
            PlayAgain = true;

            ConsoleHelper.SetConsoleFont(10);
            Console.SetBufferSize(80, 30);
            Console.SetWindowSize(80, 30);
            
            DeserializeContent();

            while (PlayAgain == true) {
                ShuffleList();
                Cutscenes[0].Draw();
                foreach (Scene scene in Scenes) {
                    scene.Draw();
                    scene.GetResponse();
                    if (scene.CurrentHeroState != "Alive") {
                        GameOver = true;
                        break;
                    }
                }

                if (GameOver) {
                    Cutscenes[1].Draw();
                }
                else {
                    Cutscenes[2].Draw();
                }

                Console.Clear();

                Console.WriteLine("Press 'Y' to play again. Any other key will exit: ");
                ConsoleKeyInfo result = Console.ReadKey();

                if (result.Key != ConsoleKey.Y) {
                    PlayAgain = false;
                }

                Console.Clear();
            }
        }

        public static void ShuffleList() {
            Random random = new Random();
            Scenes = Scenes.OrderBy(item => random.Next()).ToList<Scene>();
        }

        public static void SerializeScenes() {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Scene>));
            using (TextWriter writer = new StreamWriter(DataDirectory + "\\scenes.xml")) {
                serializer.Serialize(writer, Scenes);
            }
        }

        public static void DeserializeContent() {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Scene>));
            using (TextReader reader = new StreamReader(DataDirectory + "\\scenes.xml")) {
                Scenes = (List<Scene>)deserializer.Deserialize(reader);
            }

            deserializer = new XmlSerializer(typeof(List<Cutscene>));
            using (TextReader reader = new StreamReader(DataDirectory + "\\cutscenes.xml")) {
                Cutscenes = (List<Cutscene>)deserializer.Deserialize(reader);
            }
        }
    }
}
