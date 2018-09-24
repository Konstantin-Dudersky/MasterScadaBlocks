using System;
using System.Windows.Media;

namespace MasterScadaBlocksCommon
{
    public static class CommonClass
    {
        public static string ColorPowerBi0 = "#FF01B8AA";
        public static string ColorPowerBi1 = "#FF374649";
        public static string ColorPowerBi2 = "#FFFD625E";
        public static string ColorPowerBi3 = "#FFF2C80F";
        public static string ColorPowerBi4 = "#FF5F6B6D";
        public static string ColorPowerBi5 = "#FF8AD4EB";
        public static string ColorPowerBi6 = "#FFFE9666";
        public static string ColorPowerBi7 = "#FFA66999";
        public static string ColorPowerBi8 = "#FF3599B8";
        public static string ColorPowerBi9 = "#FFDFBFBF";

        public static Color ChangeLuminosity(Color color, double inc)
        {
            HSL hsl1 = RGBToHSL(new RGB(color.R, color.G, color.B));
            HSL hsl2 = new HSL(hsl1.H, hsl1.S, (float)(hsl1.L + inc));
            RGB rgb = HSLToRGB(hsl2);
            return Color.FromRgb(rgb.R, rgb.G, rgb.B);
        }

        public static RGB HSLToRGB(HSL hsl)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if (hsl.S == 0)
            {
                r = g = b = (byte)(hsl.L * 255);
            }
            else
            {
                float v1, v2;
                float hue = (float)hsl.H / 360;

                v2 = (hsl.L < 0.5) ? (hsl.L * (1 + hsl.S)) : ((hsl.L + hsl.S) - (hsl.L * hsl.S));
                v1 = 2 * hsl.L - v2;

                r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0f / 3)));
                g = (byte)(255 * HueToRGB(v1, v2, hue));
                b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0f / 3)));
            }

            return new RGB(r, g, b);
        }

        public static HSL RGBToHSL(RGB rgb)
        {
            HSL hsl = new HSL();

            float r = (rgb.R / 255.0f);
            float g = (rgb.G / 255.0f);
            float b = (rgb.B / 255.0f);

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);
            float delta = max - min;

            hsl.L = (max + min) / 2;

            if (delta == 0)
            {
                hsl.H = 0;
                hsl.S = 0.0f;
            }
            else
            {
                hsl.S = (hsl.L <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

                float hue;

                if (r == max)
                {
                    hue = ((g - b) / 6) / delta;
                }
                else if (g == max)
                {
                    hue = (1.0f / 3) + ((b - r) / 6) / delta;
                }
                else
                {
                    hue = (2.0f / 3) + ((r - g) / 6) / delta;
                }

                if (hue < 0)
                {
                    hue += 1;
                }

                if (hue > 1)
                {
                    hue -= 1;
                }

                hsl.H = (int)(hue * 360);
            }

            return hsl;
        }

        private static float HueToRGB(float v1, float v2, float vH)
        {
            if (vH < 0)
            {
                vH += 1;
            }

            if (vH > 1)
            {
                vH -= 1;
            }

            if ((6 * vH) < 1)
            {
                return (v1 + (v2 - v1) * 6 * vH);
            }

            if ((2 * vH) < 1)
            {
                return v2;
            }

            if ((3 * vH) < 2)
            {
                return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);
            }

            return v1;
        }

        public struct HSL
        {
            private int _h;
            private float _l;
            private float _s;

            public HSL(int h, float s, float l)
            {
                _h = h;
                _s = s;
                _l = l;
            }

            public int H {
                get => _h;
                set => _h = value;
            }

            public float L {
                get => _l;
                set => _l = value;
            }

            public float S {
                get => _s;
                set => _s = value;
            }

            public bool Equals(HSL hsl)
            {
                return (H == hsl.H) && (S == hsl.S) && (L == hsl.L);
            }
        }

        public struct RGB
        {
            private byte _b;
            private byte _g;
            private byte _r;

            public RGB(byte r, byte g, byte b)
            {
                _r = r;
                _g = g;
                _b = b;
            }

            public byte B {
                get => _b;
                set => _b = value;
            }

            public byte G {
                get => _g;
                set => _g = value;
            }

            public byte R {
                get => _r;
                set => _r = value;
            }

            public bool Equals(RGB rgb)
            {
                return (R == rgb.R) && (G == rgb.G) && (B == rgb.B);
            }
        }
    }
}