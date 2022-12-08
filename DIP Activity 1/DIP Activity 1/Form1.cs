using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIP_Activity_1
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        Bitmap imageA, imageB, resultImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Simply copy the pixels from loaded to processed
        /// </summary>
        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for(int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Get the average of the R, G, B and set it as the new RGB
        /// </summary>
        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    processed.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }
            pictureBox2.Image = processed;
        }

        /// <summary>
        /// Traverse to each pixels on the image and subtract 255 from each of the R,G,B
        /// Set the result of the substraction as the new R,G,B of the image
        /// </summary>
        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    processed.SetPixel(x, y, Color.FromArgb(255-pixel.R, 255-pixel.G, 255-pixel.B));
                }
            }
            pictureBox2.Image = processed;
        }

        /// <summary>
        /// Converts the image into greyscale first
        /// Place the Red of each pixels into the hisdata
        /// Set the processed variable into white in order to have 
        /// a white background of histogram and sets a certain pixels to black
        /// </summary>
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    loaded.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }

            int[] hisdata = new int[256];
            for(int x = 0; x < loaded.Width; x++)
            {
                for(int y = 0; y< loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    hisdata[pixel.R]++;
                }
            }

            processed = new Bitmap(256, 800);
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    processed.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(hisdata[x] / 5, processed.Height - 1); y++)
                {
                    processed.SetPixel(x, (processed.Height - 1) - y, Color.Black);
                }
            }

            pictureBox2.Image = processed;
        }

        /// <summary>
        /// Apply Sepia filter to the image
        /// Multiply the R,G,B of each pixels with a specific decimals
        /// and the result would be the nre R,G,B of the pixels
        /// </summary>
        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    int r = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int g = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int b = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    if (r > 255)
                    {
                        r = 255;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    
                    if (b > 255)
                    {
                        b = 255;
                    }

                    processed.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = processed;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Image img = (Image)processed;
            img.Save(saveFileDialog1.FileName);

        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imageB;
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image = imageA;
        }

        private void LoadImage_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void LoadBackground_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }
        private void Subtract_Click(object sender, EventArgs e)
        {
            resultImage = new Bitmap(imageB.Width, imageB.Height);
            Color myGreen = Color.FromArgb(0, 255, 0);
            int greyGreen = (myGreen.R + myGreen.G + myGreen.B) / 3;
            int threshold = 5;

            for(int x = 0; x < imageB.Width; x++)
            {
                for(int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backPixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractValue = Math.Abs(grey - greyGreen);
                    if(subtractValue > threshold)
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, backPixel);
                    }
                }
            }
            pictureBox3.Image = resultImage;
        }

    }
}
