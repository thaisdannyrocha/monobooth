using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace PhotoBooth
{
    public partial class frmMain : Form
    {


        private Capture _capture;
        private int counter = 0;
        private bool capturing = false;
        private int frameNumber;
        private string insText = "";
        public string lastFilmStrip;


        //Set up a public config object
        VAkos.Xmlconfig xcfg = new VAkos.Xmlconfig("config.xml", true);

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            setupInterface();
            startPreview();

        }

        private void startPreview()
        {
            #region Create capture object if it is not already created

            if (_capture == null)
            {
                try
                {
                    _capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            #endregion

            #region Start the capture process and display in the preview window

            if (_capture != null)
            {
                //start the capture
                Application.Idle += ProcessFrame;

            }

            #endregion
        }

        private void frmMain_MaximumSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void setupInterface()
        {
            //Set up the front end interface and any other settings. 
            //If any setting is not already set in the xml, add the default
            //to the xml file.

            //Check to see if this is a new config file. If it is, then write
            //the default values.
            bool SimgStrip1 = false;
            bool SimgStrip2 = false;
            bool SimgStrip3 = false;
            bool SimgStrip4 = false;
            if (xcfg.Settings.ChildCount(false) != 0)
            {
                foreach (VAkos.ConfigSetting child in xcfg.Settings.Children())
                {
                    if (child.Name == "imgStrip1")
                        SimgStrip1 = true;
                    if (child.Name == "imgStrip2")
                        SimgStrip2 = true;
                    if (child.Name == "imgStrip3")
                        SimgStrip3 = true;
                    if (child.Name == "imgStrip4")
                        SimgStrip4 = true;
                }
            }
            


            //Window Width
            if (xcfg.Settings["window"]["width"].intValue > 0)
                this.Width = xcfg.Settings["window"]["width"].intValue;
            else
                xcfg.Settings["window"]["width"].intValue = this.Width;

            //Window Height
            if (xcfg.Settings["window"]["height"].intValue > 0)
                this.Height = xcfg.Settings["window"]["height"].intValue;
            else
                xcfg.Settings["window"]["height"].intValue = this.Height;

            //image1 box settings
            if (SimgStrip1)
            {
                imgStrip1.Visible = xcfg.Settings["imgStrip1"]["enabled"].boolValue;
                Point imgStrip1Location = new Point(xcfg.Settings["imgStrip1"]["locationX"].intValue, xcfg.Settings["imgStrip1"]["locationY"].intValue);
                imgStrip1.Location = imgStrip1Location;
                imgStrip1.BackColor = System.Drawing.Color.FromName(xcfg.Settings["imgStrip1"]["backgroundcolor"].Value);
            }
            else
            {
                xcfg.Settings["imgStrip1"]["enabled"].boolValue = imgStrip1.Visible;
                xcfg.Settings["imgStrip1"]["locationX"].intValue = imgStrip1.Location.X;
                xcfg.Settings["imgStrip1"]["locationY"].intValue = imgStrip1.Location.Y;
                xcfg.Settings["imgStrip1"]["backgroundcolor"].Value = imgStrip1.BackColor.Name.ToString();
            }

            //image2 box settings
            if (SimgStrip2)
            {
                imgStrip2.Visible = xcfg.Settings["imgStrip2"]["enabled"].boolValue;
                Point imgStrip2Location = new Point(xcfg.Settings["imgStrip2"]["locationX"].intValue, xcfg.Settings["imgStrip2"]["locationY"].intValue);
                imgStrip2.Location = imgStrip2Location;
                imgStrip2.BackColor = System.Drawing.Color.FromName(xcfg.Settings["imgStrip2"]["backgroundcolor"].Value);
            }
            else
            {
                xcfg.Settings["imgStrip2"]["enabled"].boolValue = imgStrip2.Visible;
                xcfg.Settings["imgStrip2"]["locationX"].intValue = imgStrip2.Location.X;
                xcfg.Settings["imgStrip2"]["locationY"].intValue = imgStrip2.Location.Y;
                xcfg.Settings["imgStrip2"]["backgroundcolor"].Value = imgStrip2.BackColor.Name.ToString();
            }

            

            //image3 box settings
            if (SimgStrip3)
            {
                imgStrip3.Visible = xcfg.Settings["imgStrip3"]["enabled"].boolValue;
                Point imgStrip3Location = new Point(xcfg.Settings["imgStrip3"]["locationX"].intValue, xcfg.Settings["imgStrip3"]["locationY"].intValue);
                imgStrip3.Location = imgStrip3Location;
                imgStrip3.BackColor = System.Drawing.Color.FromName(xcfg.Settings["imgStrip3"]["backgroundcolor"].Value);
            }
            else
            {
                xcfg.Settings["imgStrip3"]["enabled"].boolValue = imgStrip3.Visible;
                xcfg.Settings["imgStrip3"]["locationX"].intValue = imgStrip3.Location.X;
                xcfg.Settings["imgStrip3"]["locationY"].intValue = imgStrip3.Location.Y;
                xcfg.Settings["imgStrip3"]["backgroundcolor"].Value = imgStrip3.BackColor.Name.ToString();
            }

            //image4 box settings
            if (SimgStrip4)
            {
                imgStrip4.Visible = xcfg.Settings["imgStrip4"]["enabled"].boolValue;
                Point imgStrip4Location = new Point(xcfg.Settings["imgStrip4"]["locationX"].intValue, xcfg.Settings["imgStrip4"]["locationY"].intValue);
                imgStrip4.Location = imgStrip4Location;
                imgStrip4.BackColor = System.Drawing.Color.FromName(xcfg.Settings["imgStrip4"]["backgroundcolor"].Value);
            }
            else
            {
                xcfg.Settings["imgStrip4"]["enabled"].boolValue = imgStrip4.Visible;
                xcfg.Settings["imgStrip4"]["locationX"].intValue = imgStrip4.Location.X;
                xcfg.Settings["imgStrip4"]["locationY"].intValue = imgStrip4.Location.Y;
                xcfg.Settings["imgStrip4"]["backgroundcolor"].Value = imgStrip4.BackColor.Name.ToString();
            }

            


            //Save settings incase anything was not specified and defaults were added
            xcfg.Save("config.xml");
            this.Location = new Point(20, 20);
        }

        

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == char.Parse("x"))
                Application.Exit();
        }

        private void cmdStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == char.Parse("x"))
                Application.Exit();
        }



        private void cmdStart_Click(object sender, EventArgs e)
        {
            counter = 0;
            tmrCommon.Enabled = true;
            tmrCommon.Start();
            capturing = true;

            imgStrip1.Image = null;
            imgStrip2.Image = null;
            imgStrip3.Image = null;
            imgStrip4.Image = null;
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> frame = _capture.QueryFrame();

            if (insText != "")
            {
                MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_TRIPLEX, 2.0, 2.0);

                frame.Draw(insText, ref font, new System.Drawing.Point(150, 450), new Bgr(255, 255, 255));

            }

            imgPreview.Image = frame;


        }

        private void tmrCommon_Tick(object sender, EventArgs e)
        {
            counter += 100;
            if (capturing)
            {
                if (frameNumber < 4)
                {
                    captureFrame();
                }
                else
                {
                    saveFilmStrip();
                    printFilmStrip();
                    frameNumber = 0;
                    counter = 0;
                    capturing = false;
                    tmrCommon.Stop();
                }
            }

        }

        private void captureFrame()
        {
            #region Countdown
            if (counter == 100)
            {
                insText = "Get Ready";
            }
            if (counter == 1500)
            {
                insText = "    3";
            }
            if (counter == 2500)
            {
                insText = "    2";
            }
            if (counter == 3500)
            {
                insText = "    1";
            }
            if (counter == 4500)
            {
                insText = "  Smile!";
            }

            #endregion

            #region Take, save and display the picture

            if (counter == 5500)
            {
                string newcapturename = Guid.NewGuid().ToString() + ".jpg";
                Console.WriteLine(newcapturename);
                Image<Bgr, Byte> tempImage = _capture.QueryFrame();
                tempImage.Save(newcapturename);
                frameNumber++;
                counter = 0;
                insText = "";

                switch (frameNumber)
                {
                    case 1:
                        imgStrip1.Image = tempImage.ToBitmap();
                        break;
                    case 2:
                        imgStrip2.Image = tempImage.ToBitmap();
                        break;
                    case 3:
                        imgStrip3.Image = tempImage.ToBitmap();
                        break;
                    case 4:
                        imgStrip4.Image = tempImage.ToBitmap();
                        break;
                    default:
                        break;

                }
            }

            #endregion
        }

        public Image AppendBorder(Image original, int borderWidth)
        {
            var borderColor = Color.Black;

            var newSize = new Size(
                original.Width + borderWidth * 2,
                original.Height + borderWidth * 2);

            var img = new Bitmap(newSize.Width, newSize.Height);
            var g = Graphics.FromImage(img);

            g.Clear(borderColor);
            g.DrawImage(original, new Point(borderWidth, borderWidth));
            g.Dispose();

            return img;
        }

        public void saveFilmStrip()
        {

            Image filmStrip = imgStrip1.Image;
            using (filmStrip)
            {
                Image i1 = AppendBorder(imgStrip1.Image, 10);
                Image i2 = AppendBorder(imgStrip2.Image, 10);
                Image i3 = AppendBorder(imgStrip3.Image, 10);
                Image i4 = AppendBorder(imgStrip4.Image, 10);
                    using (var bitmap = new Bitmap(i1.Width, i1.Height + i2.Height + i3.Height + i4.Height))
                    {
                        using (var canvas = Graphics.FromImage(bitmap))
                        {
                            canvas.DrawImage(i1, new Point(0, 0));
                            canvas.DrawImage(i2, new Point(0, 0 + i1.Height));
                            canvas.DrawImage(i3, new Point(0, 0 + i1.Height + i2.Height));
                            canvas.DrawImage(i4, new Point(0, 0 + i1.Height + i2.Height + i3.Height));
                            canvas.Save();
                        }
                        string filmStripName = Guid.NewGuid().ToString() + "-fs.jpg";
                        bitmap.Save(filmStripName);
                        lastFilmStrip = filmStripName;
                        
                    }
            }
            


            

        }

        public void printFilmStrip()
        {
            System.Drawing.Printing.PrintDocument filmStripDoc = new System.Drawing.Printing.PrintDocument();
            filmStripDoc.DocumentName = "filmStrip";
            filmStripDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(filmStripDoc_PrintPage);
            filmStripDoc.Print();
        }

        void filmStripDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image filmStrip = Image.FromFile(lastFilmStrip);
            float scaledWidth = (float)(filmStrip.Size.Width * .30);
            float scaledHeight = (float)(filmStrip.Size.Height * .30);

            e.Graphics.DrawImage(filmStrip, 10, 10, scaledWidth, scaledHeight);
            e.Graphics.DrawImage(filmStrip, 20 + scaledWidth, 10, scaledWidth, scaledHeight);
            
            
        }

    }
}
