using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendofEveryoneElse {
    public class Cutscene {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Dialog { get; set; }

        public void Draw() {
            ConsoleColor foreColor = ConsoleColor.Yellow;
            ConsoleColor backColor = ConsoleColor.DarkGreen;

            switch (Type) {
                case "Intro":
                    backColor = ConsoleColor.DarkCyan;
                    break;
                case "Fail":
                    backColor = ConsoleColor.DarkRed;
                    break;
                case "Win":
                    backColor = ConsoleColor.DarkGreen;
                    break;
            }

            new UIElement(1, 1, 25, 2, UIBorderMode.NONE, ConsoleColor.White, ConsoleColor.Black, Title).Draw();
            new UIElement(6, 6, 68, 15, UIBorderMode.FULL, foreColor, backColor, "Bartender: " + Dialog).Draw();

            Console.CursorTop = 24;
            Console.CursorLeft = 5;
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
