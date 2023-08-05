using System.Drawing;

using Pastel;

namespace View
{
    class Txt
    {
        public static string Cyan(string message)
        {
            return message.Pastel(Color.Cyan);
        }

        public static string Yellow(string message)
        {
            return message.Pastel(Color.Yellow);
        }

        public static string White(string message)
        {
            return message.Pastel(Color.White);
        }

        public static string Green(string message)
        {
            return message.Pastel(Color.LightGreen);
        }

        public static string GreenBg(string message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.Green);
        }

        public static string WhiteBg(string message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.White);
        }

        public static string RedBg(string message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.Red);
        }

        public static string CyanBg(string message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.Cyan);
        }
    }
}
