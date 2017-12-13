using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Services.Services.Recognition
{
    public class HsvColor
    {
        private double hue;
        private double saturation;
        private double brightness;

        public HsvColor()
        {
        }

        public HsvColor(double _hue, double _saturation, double _brightness)
        {
            this.hue = _hue;
            this.saturation = _saturation;
            this.brightness = _brightness;
        }

        public double getHue()
        {
            return hue;
        }

        public double getSaturation()
        {
            return saturation;
        }

        public double getBrightness()
        {
            return brightness;
        }

        public void setHue(double hue)
        {
            this.hue = hue;
        }

        public void setSaturation(double saturation)
        {
            this.saturation = saturation;
        }

        public void setBrightness(double brightness)
        {
            this.brightness = brightness;
        }


        public static HsvColor convertFromRgbToHsv(Color color)
        {
            HsvColor toReturn = new HsvColor();

            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            toReturn.setHue(Math.Round(color.GetHue(), 2));
            toReturn.setSaturation(((max == 0) ? 0 : 1d - (1d * min / max)) * 100);
            toReturn.setSaturation(Math.Round(toReturn.getSaturation(), 2));
            toReturn.setBrightness(Math.Round(((max / 255d) * 100), 2));

            return toReturn;
        }

    }
}
