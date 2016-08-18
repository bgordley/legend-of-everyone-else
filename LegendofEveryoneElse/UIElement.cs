using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendofEveryoneElse {
    enum UIBorderMode {
        NONE,
        TOP,
        MID,
        BOTTOM,
        FULL,
    }
    class UIElement {
        private int X;
        private int Y;
        private int Width;
        private int Height;
        private string Text;
        private UIBorderMode BorderMode;
        private ConsoleColor ForeColor;
        private ConsoleColor BackColor;

        public UIElement(int x, int y, int width, int height, UIBorderMode borderMode, ConsoleColor foreColor, ConsoleColor backColor, string text) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BorderMode = borderMode;
            ForeColor = foreColor;
            BackColor = backColor;
            Text = text;
        }

        public void Draw() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            if (BorderMode != UIBorderMode.NONE) {
                CreateBorder();
            }
            Console.ForegroundColor = ForeColor;
            Console.BackgroundColor = BackColor;
            CreateText();
            Console.ResetColor();
        }

        private void CreateBorder() {
            string top = "┌";
            string mid = "│";
            string bottom = "└";
            int mod = 1;

            for (int i = 0; i < Width; i++) {
                top += "─";
                mid += " ";
                bottom += "─";
                if(i + 1 == Width) {
                    top += "┐";
                    mid += "│";
                    bottom += "┘";
                }
            }

            for (int i = 0; i <= Height + mod; i++) {
                Console.CursorTop = (Y - 1) + i;
                Console.CursorLeft = X - 1;

                if (i == 0) {
                    if(BorderMode == UIBorderMode.TOP || BorderMode == UIBorderMode.FULL)
                        Console.Write(top);
                }
                else if(i-1 != Height) {
                    Console.Write(mid);
                }
                else {
                    if(BorderMode == UIBorderMode.BOTTOM || BorderMode == UIBorderMode.FULL)
                        Console.Write(bottom);
                }
            }
        }

        private void CreateText() {
            string newLine = "";
            string outText = Text;
            int difference = 0;
            for (int i = 0; i < Height; i++) {
                difference = (Width * i) + Width - outText.Length;
                if (difference >= 0) {
                    for (int j = 0; j < difference; j++)
                        outText += " ";
                }
                newLine = outText.Substring((Width) * i, Width);

                Console.CursorTop = Y + i;
                Console.CursorLeft = X;
                Console.Write(newLine);
            }
        }
    }
}
