using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LegendofEveryoneElse {
    public class Decision {
        public string Action { get; set; }
        public string Response { get; set; }
        public string HeroState { get; set; }
    }

    public class Scene {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DefaultDialog { get; set; }
        public string EndDialog { get; set; }
        [XmlIgnore]
        public string CurrentHeroState { get; set; }
        public List<Decision> Decisions { get; set; }

        public void Draw() {
            CurrentHeroState = "Alive";

            new UIElement(1, 1, 25, 2, UIBorderMode.NONE, ConsoleColor.White, ConsoleColor.Black, "Scene: " + Title).Draw();
            new UIElement(28, 1, 51, 2, UIBorderMode.NONE, ConsoleColor.Gray, ConsoleColor.Black, Description).Draw();
            new UIElement(1, 5, 25, 1, UIBorderMode.FULL, ConsoleColor.Black, ConsoleColor.Green, "Hero State: " + CurrentHeroState).Draw();
            new UIElement(1, 8, 78, 10, UIBorderMode.FULL, ConsoleColor.Yellow, ConsoleColor.DarkGreen, "Hero: " + DefaultDialog).Draw();

            int count = 0;
            foreach (Decision decision in Decisions) {
                count += 1;
                if (count == 1) {
                    new UIElement(1, 20 + count, 78, 1, UIBorderMode.TOP, ConsoleColor.Cyan, ConsoleColor.DarkGreen, "Option " + count + ": " + decision.Action).Draw();
                }
                else if (count == Decisions.Count + 1) {
                    new UIElement(1, 20 + count, 78, 1, UIBorderMode.MID, ConsoleColor.Cyan, ConsoleColor.DarkGreen, "Option " + count + ": " + decision.Action).Draw();
                }
                else {
                    new UIElement(1, 20 + count, 78, 1, UIBorderMode.BOTTOM, ConsoleColor.Cyan, ConsoleColor.DarkGreen, "Option " + count + ": " + decision.Action).Draw();
                }
            }
        }

        public void GetResponse() {
            int cursorHeight = 23 + Decisions.Count;
            ConsoleKeyInfo response = new ConsoleKeyInfo();
            bool keyAccepted = false;

            Console.CursorTop = cursorHeight;
            Console.CursorLeft = 0;
            Console.Write("Press a key between 1 and " + Decisions.Count + ": ");

            while (!keyAccepted) {
                response = Console.ReadKey(true);

                Console.CursorTop = cursorHeight;
                Console.CursorLeft = 0;

                try {
                    int responseInt = Int32.Parse(response.KeyChar.ToString());
                    if (responseInt > 0 && responseInt <= Decisions.Count)
                        keyAccepted = true;
                }
                catch {}

                if(!keyAccepted)
                    Console.Write("Your entry must be a number between 1 and " + Decisions.Count + ": ");
            }
            Console.CursorTop = cursorHeight;
            Console.CursorLeft = 0;
            Console.Write("                                                ");

            switch (response.KeyChar) {
                case '1':
                    new UIElement(1, 8, 78, 10, UIBorderMode.FULL, ConsoleColor.Yellow, ConsoleColor.DarkGreen, "Hero: " + Decisions[0].Response).Draw();
                    if (Decisions[0].HeroState != "Alive") {
                        CurrentHeroState = Decisions[0].HeroState;
                        new UIElement(1, 5, 25, 1, UIBorderMode.FULL, ConsoleColor.Black, ConsoleColor.Red, "Hero State: " + CurrentHeroState).Draw();
                    }
                    break;
                case '2':
                    new UIElement(1, 8, 78, 10, UIBorderMode.FULL, ConsoleColor.Yellow, ConsoleColor.DarkGreen, "Hero: " + Decisions[1].Response).Draw();
                    if (Decisions[1].HeroState != "Alive") {
                        CurrentHeroState = Decisions[1].HeroState;
                        new UIElement(1, 5, 25, 1, UIBorderMode.FULL, ConsoleColor.Black, ConsoleColor.Red, "Hero State: " + CurrentHeroState).Draw();
                    }
                    break;
                case '3':
                    new UIElement(1, 8, 78, 10, UIBorderMode.FULL, ConsoleColor.Yellow, ConsoleColor.DarkGreen, "Hero: " + Decisions[2].Response).Draw();
                    if (Decisions[2].HeroState != "Alive") {
                        CurrentHeroState = Decisions[2].HeroState;
                        new UIElement(1, 5, 25, 1, UIBorderMode.FULL, ConsoleColor.Black, ConsoleColor.Red, "Hero State: " + CurrentHeroState).Draw();
                    }
                    break;
                case '4':
                    new UIElement(1, 8, 78, 10, UIBorderMode.FULL, ConsoleColor.Yellow, ConsoleColor.DarkGreen, "Hero: " + Decisions[3].Response).Draw();
                    if (Decisions[3].HeroState != "Alive") {
                        CurrentHeroState = Decisions[3].HeroState;
                        new UIElement(1, 5, 25, 1, UIBorderMode.FULL, ConsoleColor.Black, ConsoleColor.Red, "Hero State: " + CurrentHeroState).Draw();
                    }
                    break;
            }

            Console.CursorTop = cursorHeight;
            Console.CursorLeft = 0;
            Console.Write("Press any key to continue: ");
            Console.ReadKey();

            if (CurrentHeroState == "Alive") {
                new UIElement(1, 8, 78, 10, UIBorderMode.FULL, ConsoleColor.Yellow, ConsoleColor.DarkGreen, "Hero: " + EndDialog).Draw();

                Console.CursorTop = cursorHeight;
                Console.CursorLeft = 0;
                Console.Write("Press any key to continue: ");
                Console.ReadKey();
            }

            Console.Clear();
        }
    }
}
