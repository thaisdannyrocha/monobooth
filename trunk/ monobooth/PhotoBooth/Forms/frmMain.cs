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

        #region public vars used all over

        private Capture _capture;
        private int counter = 0;
        private bool capturing = false;
        private bool showingQR = false;
        private bool sharing = false;
        private int frameNumber;
        private string insText = "";
        public string lastFilmStrip;
        private int countdownDelay = 100;
        private bool printingEnabled = false;
        private bool saveFilmStripEnabled = false;


        private string ftpServer = "localhost";
        private string ftpUser = "user";
        private string ftpPassword = "password";
        private string ftpPath = "/";


        #region Messages

        private string strInitialMessage = "Get Ready!";
        private string strCountdown1 = "    3";
        private string strCountdown2 = "    2";
        private string strCountdown3 = "    1";
        private string strFinalMessage = "  Smile!";

        #endregion


        //Set up a public config object
        VAkos.Xmlconfig xcfg = new VAkos.Xmlconfig("config.xml", true);
        #endregion


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

            if (showingQR)
            {
                ThoughtWorks.QRCode.Codec.QRCodeEncoder newQR = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
                frame = new Image<Bgr,byte>(newQR.Encode("http://www.amiapps.com/photobooth/" + lastFilmStrip));

            }
            else
            {
                frame = _capture.QueryFrame();
            }
            

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
            if (showingQR)
            {
                if (counter == 10000)
                {
                    counter = 0;
                    capturing = false;
                    tmrCommon.Stop();
                }
            }
            if (capturing)
            {
                if (frameNumber < 4)
                {
                    captureFrame();
                }
                else
                {
                    counter = 0;
                    capturing = false;
                    tmrCommon.Stop();

                    if (saveFilmStripEnabled)
                        saveFilmStrip();
                    
                    if (printingEnabled)
                        printFilmStrip();

                    frameNumber = 0;
                    
                }
            }

        }

        private void captureFrame()
        {
            #region Countdown
            if (counter == 100)
            {
                insText = strInitialMessage;
            }
            if (counter == 1500)
            {
                insText = strCountdown1;
            }
            if (counter == 2500)
            {
                insText = strCountdown2;
            }
            if (counter == 3500)
            {
                insText = strCountdown3;
            }
            if (counter == 4500)
            {
                insText = strFinalMessage;
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
                        if (sharing)
                        {
                            monobooth.Sharing shareTools = new monobooth.Sharing();
                            shareTools.uploadFtp(lastFilmStrip, ftpServer, ftpUser, ftpPassword, ftpPath);

                            showingQR = true;
                            tmrCommon.Start();
                        }
                        
                        
                    }
            }
            


            

        }

        #region Printing
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

        #endregion


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

            #region Window Settings
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

            #endregion

            #region previewWindow

            if (xcfg.Settings["preview"]["width"].intValue > 0)
                imgPreview.Width = xcfg.Settings["preview"]["width"].intValue;
            else
                xcfg.Settings["preview"]["width"].intValue = imgPreview.Width;


            if (xcfg.Settings["preview"]["height"].intValue > 0)
                imgPreview.Height = xcfg.Settings["preview"]["height"].intValue;
            else
                xcfg.Settings["preview"]["height"].intValue = imgPreview.Height;

            if (xcfg.Settings["preview"]["x"].intValue > 0 && xcfg.Settings["preview"]["y"].intValue > 0)
                imgPreview.Location = new Point(xcfg.Settings["preview"]["x"].intValue, xcfg.Settings["preview"]["y"].intValue);
            else
            {
                xcfg.Settings["preview"]["x"].intValue = imgPreview.Location.X;
                xcfg.Settings["preview"]["y"].intValue = imgPreview.Location.Y;
            }


            #endregion

            #region Image Box 1
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

            #endregion

            #region Image Box 2
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

            #endregion

            #region Image Box 3
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

            #endregion

            #region Image Box 4
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
            #endregion

            #region Countdown Message Settings

            if (xcfg.Settings["countdown"]["initialMessage"].Value.Length > 0)
                strInitialMessage = xcfg.Settings["countdown"]["initialMessage"].Value;
            else
                xcfg.Settings["countdown"]["initialMessage"].Value = strInitialMessage;


            if (xcfg.Settings["countdown"]["countdown1"].Value.Length > 0)
                strCountdown1 = xcfg.Settings["countdown"]["countdown1"].Value;
            else
                xcfg.Settings["countdown"]["countdown1"].Value = strCountdown1;


            if (xcfg.Settings["countdown"]["countdown2"].Value.Length > 0)
                strCountdown2 = xcfg.Settings["countdown"]["countdown2"].Value;
            else
                xcfg.Settings["countdown"]["countdown2"].Value = strCountdown2;


            if (xcfg.Settings["countdown"]["countdown3"].Value.Length > 0)
                strCountdown3 = xcfg.Settings["countdown"]["countdown3"].Value;
            else
                xcfg.Settings["countdown"]["countdown3"].Value = strCountdown3;


            if (xcfg.Settings["countdown"]["finalmessage"].Value.Length > 0)
                strFinalMessage = xcfg.Settings["countdown"]["finalmessage"].Value;
            else
                xcfg.Settings["countdown"]["finalmessage"].Value = strFinalMessage;




            #endregion

            #region Sharing

            if (xcfg.Settings["sharing"]["ftpServer"].Value.Length > 0)
                ftpServer = xcfg.Settings["sharing"]["ftpServer"].Value;
            else
                xcfg.Settings["sharing"]["ftpServer"].Value = ftpServer;


            if (xcfg.Settings["sharing"]["ftpUser"].Value.Length > 0)
                ftpUser = xcfg.Settings["sharing"]["ftpUser"].Value;
            else
                xcfg.Settings["sharing"]["ftpUser"].Value = ftpUser;


            if (xcfg.Settings["sharing"]["ftpPassword"].Value.Length > 0)
                ftpPassword = xcfg.Settings["sharing"]["ftpPassword"].Value;
            else
                xcfg.Settings["sharing"]["ftpPassword"].Value = ftpPassword;

            if (xcfg.Settings["sharing"]["ftpPath"].Value.Length > 0)
                ftpPath = xcfg.Settings["sharing"]["ftpPath"].Value;
            else
                xcfg.Settings["sharing"]["ftpPath"].Value = ftpPath;

            if (xcfg.Settings["sharing"]["ftpEnabled"].boolValue)
                sharing = true;
            else
                xcfg.Settings["sharing"]["ftpEnabled"].boolValue = sharing;
                
            #endregion

            #region Printing

            if (xcfg.Settings["printing"]["printingEnabled"].boolValue)
                printingEnabled = true;
            else
                xcfg.Settings["printing"]["printingEnabled"].boolValue = printingEnabled;

            #endregion

            #region Saving

            if (xcfg.Settings["saving"]["saveFilmStripEnabled"].boolValue)
                saveFilmStripEnabled = true;
            else
                xcfg.Settings["saving"]["saveFilmStripEnabled"].boolValue = saveFilmStripEnabled;

            #endregion




            //Save settings incase anything was not specified and defaults were added
            xcfg.Save("config.xml");
            this.Location = new Point(20, 20);
        }

    }
}
