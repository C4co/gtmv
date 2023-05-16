using System.Drawing;
using Pastel;

namespace View
{
    class Txt
    {
        public static String Cyan(String message)
        {
            return message.Pastel(Color.Cyan);
        }

        public static String Yellow(String message)
        {
            return message.Pastel(Color.Yellow);
        }

        public static String White(String message)
        {
            return message.Pastel(Color.White);
        }

        public static String Green(String message)
        {
            return message.Pastel(Color.Green);
        }

        public static String GreenBg(String message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.Green);
        }

        public static String WhiteBg(String message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.White);
        }

        public static String RedBg(String message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.Red);
        }

        public static String CyanBg(String message)
        {
            return message.Pastel(Color.Black).PastelBg(Color.Cyan);
        }
    }
}
