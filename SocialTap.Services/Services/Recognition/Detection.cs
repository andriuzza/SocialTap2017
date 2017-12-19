using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Web;

namespace SocialTap.Services.Services.Recognition
{
   public static class Detection
   {
        public static int CreateDetection(HttpPostedFileBase file)
        {
            //  Image beerImage = Image.FromFile(path, true);
            Image beerImage= Image.FromStream(file.InputStream, true, true);
            Bitmap beerImageBitMap = new Bitmap(beerImage);
            Bitmap beerImageBitMapUntouched = new Bitmap(Image.FromStream(file.InputStream, true, true));
            

            int beerImageHeight = beerImageBitMap.Height;
            int beerImageWidth = beerImageBitMap.Width;

            Bitmap filteredBlackOrWhiteImage = GetFilteredBeerColors(beerImageBitMap);
 //           filteredBlackOrWhiteImage = Blur(filteredBlackOrWhiteImage, 30);
          //  (filteredBlackOrWhiteImage as Image).Save("C:/Users/PC/Desktop/ConsoleApp1/beerEditedBlackWhite.jpg");
            
            Bitmap croppedOriginalImage = cropImage(beerImageBitMapUntouched, filteredBlackOrWhiteImage);
            Image<Bgr, Byte> edgeImage = new Image<Bgr, Byte>(croppedOriginalImage);
            Image<Gray, Byte> blackWhiteEdgeImage = edgeImage.Convert<Gray, Byte>();
            Image<Gray, Single> finalEdgeImage = blackWhiteEdgeImage.Sobel(2, 0, 3);
         //   finalEdgeImage.Save("C:/Users/Dokumentai/source/repos/ConsoleApp1/ConsoleApp1/beerEdgeImage.jpg");
            Image beerImageEdited = croppedOriginalImage;
          //  beerImageEdited.Save("C:/Users/Dokumentai/source/repos/ConsoleApp1/ConsoleApp1/beerEdited.jpg");
            Bitmap filteredBlackWhiteCroppedImage = GetFilteredBeerColors(croppedOriginalImage);
            int percentageOfBeer = GetBeerPercentage(filteredBlackWhiteCroppedImage);
            Console.WriteLine(percentageOfBeer);

            return percentageOfBeer;
        }

        public static int GetBeerPercentage(Bitmap filteredImageOfBeer)
        {
            int beerPixels = 0;
            List<Coordinate> coordinateList = getLongestHorizontalLineCoordinates(filteredImageOfBeer);
            Coordinate leftCoordinate = coordinateList.First();
            Coordinate rightCoordinate = coordinateList.Last();
            List<int> verticalCoordinateList = getLongestVerticalLineCoordinates(filteredImageOfBeer);
            int top = verticalCoordinateList.First();
            int bottom = verticalCoordinateList.Last();
            int topToHeight = filteredImageOfBeer.Height - top;
            int verticalLength = top - bottom;
            int totalPixels = filteredImageOfBeer.Height * (rightCoordinate.getX() - leftCoordinate.getX());
            for (int a = leftCoordinate.getX(); a < rightCoordinate.getX(); a++)
            {
                for (int b = 0; b < filteredImageOfBeer.Height; b++)
                {
                    if (filteredImageOfBeer.GetPixel(a, b).ToArgb().Equals((Color.White).ToArgb()))
                    {
                        beerPixels++;
                    }
                }
            }
            return ((verticalLength * 100) / (verticalLength + topToHeight));
        }

        public static Bitmap cropImage(Bitmap originalImage, Bitmap filteredImage)
        {
            List<Coordinate> coordinateList = getLongestHorizontalLineCoordinates(filteredImage);
            Coordinate leftCoordinate = coordinateList.First();
            Coordinate rightCoordinate = coordinateList.Last();
            Rectangle rectangle = new Rectangle(leftCoordinate.getX() - 15, 0,
                rightCoordinate.getX() - leftCoordinate.getX() + 15, originalImage.Height);
            return cropAtRect(originalImage, rectangle);
        }

        public static Bitmap cropAtRect(this Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        public static List<int> getLongestVerticalLineCoordinates(Bitmap blackWhiteImage)
        {
            int top = -1;
            int bottom = int.MaxValue;
            int mostTop = -1;
            int mostBottom = int.MaxValue;
            int length = 0;
            int maxLength = 0;

            for (int a = 0; a < blackWhiteImage.Width; a++)
            {
                length = 0;
                bottom = int.MaxValue;
                top = -1;
                for (int b = 0; b < blackWhiteImage.Height; b++)
                {
                    if (blackWhiteImage.GetPixel(a, b).ToArgb().Equals((Color.White).ToArgb()))
                    {
                        if (length == 0)
                        {
                            bottom = b;
                            length++;
                        }
                        else
                        {
                            top = b;
                            length++;
                        }
                        if (length > maxLength)
                        {
                            maxLength = length;
                            mostBottom = bottom;
                            mostTop = top;
                        }
                    }
                    else
                    {
                        length = 0;
                        bottom = b;
                    }

                }
            }
            List<int> resultList = new List<int>();
            resultList.Add(mostTop);
            resultList.Add(mostBottom);
            return resultList;
        }

        public static List<Coordinate> getLongestHorizontalLineCoordinates(Bitmap blackWhiteImage)
        {
            int left = int.MaxValue;
            int right = -1;
            int mostLeft = int.MaxValue;
            int mostRight = -1;
            int length = 0;
            int maxLength = 0;


            int verticalCoordinate = determineOnWhichVerticalLineIsLongest(blackWhiteImage);

            for (int a = 0; a < blackWhiteImage.Width; a++)
            {
                if (blackWhiteImage.GetPixel(a, verticalCoordinate).ToArgb().Equals((Color.White).ToArgb()))
                {
                    if (length == 0)
                    {
                        left = a;
                        length++;
                    }
                    else
                    {
                        right = a;
                        length++;
                    }
                    if (length > maxLength)
                    {
                        maxLength = length;
                        mostLeft = left;
                        mostRight = right;
                    }
                }
                else
                {
                    length = 0;
                }

            }

            Coordinate leftCoordinate = new Coordinate(mostLeft, verticalCoordinate);
            Coordinate rightCoordinate = new Coordinate(mostRight, verticalCoordinate);
            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(leftCoordinate);
            coordinateList.Add(rightCoordinate);
            return coordinateList;

        }

        public static int determineOnWhichVerticalLineIsLongest(Bitmap blackWhiteImage)
        {
            int maxCount = 0;
            int yCoordinate = -1;
            int count = 0;
            for (int a = 0; a < blackWhiteImage.Height; a++)
            {
                count = 0;
                for (int b = 0; b < blackWhiteImage.Width; b++)
                {
                    if (blackWhiteImage.GetPixel(b, a).ToArgb().Equals((Color.White).ToArgb()))
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count > maxCount)
                    {
                        maxCount = count;
                        yCoordinate = a;
                    }
                }
            }

            return yCoordinate;
        }


        public static Bitmap GetFilteredBeerColors(Bitmap beerImageBitMapp)
        {
            Bitmap beerImageBitMap = beerImageBitMapp;
            Color pixelColor;
            HsvColor hsvPixelColor;
            for (int a = 0; a < beerImageBitMap.Height; a++)
            {
                for (int b = 0; b < beerImageBitMap.Width; b++)
                {
                    pixelColor = beerImageBitMap.GetPixel(b, a);
                    hsvPixelColor = HsvColor.convertFromRgbToHsv(pixelColor);
                    if (CouldBeBeerColor(hsvPixelColor))
                    {
                        beerImageBitMap.SetPixel(b, a, Color.White);
                    }
                    else
                    {
                        beerImageBitMap.SetPixel(b, a, Color.Black);
                    }
                }
            }
            return beerImageBitMap;
        }

        public static Boolean CouldBeBeerColor(HsvColor color)
        {
            if (color.getHue() > 30 && color.getHue() < 70 && color.getSaturation() > 40 && color.getBrightness() > 40)
            {
                return true;
            }
            return false;
        }

        private static Bitmap Blur(Bitmap image, Int32 blurSize)
        {
            return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
        }

        private static Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return blurred;
        }
    }
}
