using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoBooth
{
    public partial class frmMain : Form
    {
        //Set up some public var
        public string completeMessage = "";
        public bool completeMessageFlash = false;
        public bool completeMessageShowQR = false;
        

        //Set up a public config object
        VAkos.Xmlconfig xcfg = new VAkos.Xmlconfig("config.xml", true);

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            setupInterface();

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
            bool SWindow = false;
            bool SimgStrip1 = false;
            bool SimgStrip2 = false;
            bool SimgStrip3 = false;
            bool SimgStrip4 = false;
            bool ScompleteMsg = false;
            if (xcfg.Settings.ChildCount(false) != 0)
            {
                foreach (VAkos.ConfigSetting child in xcfg.Settings.Children())
                {
                    if (child.Name == "window")
                        SWindow = true;
                    if (child.Name == "imgStrip1")
                        SimgStrip1 = true;
                    if (child.Name == "imgStrip2")
                        SimgStrip2 = true;
                    if (child.Name == "imgStrip3")
                        SimgStrip3 = true;
                    if (child.Name == "imgStrip4")
                        SimgStrip4 = true;
                    if (child.Name == "completeMsg")
                        ScompleteMsg = true;
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

            //Complete Message
            if (ScompleteMsg)
            {
                completeMessage = xcfg.Settings["completeMsg"]["text"].Value;
                completeMessageFlash = xcfg.Settings["completeMsg"]["flash"].boolValue;
                completeMessageShowQR = xcfg.Settings["completeMsg"]["showQRcode"].boolValue;

                Point lblCompleteLocation = new Point(xcfg.Settings["completeMsg"]["X"].intValue, xcfg.Settings["completeMsd"]["Y"].intValue);
                lblCompleteMsg.Location = lblCompleteLocation;
                lblCompleteMsg.Text = completeMessage;
                lblCompleteMsg.ForeColor = System.Drawing.Color.FromName(xcfg.Settings["completeMsg"]["color"].Value);

                Point imgQRCodeLocation = new Point(xcfg.Settings["completeMsg"]["QRcode"]["X"].intValue, xcfg.Settings["completeMsg"]["QRcode"]["Y"].intValue);
                imgQR.Location = imgQRCodeLocation;
                imgQR.Height = xcfg.Settings["completeMsg"]["QRcode"]["height"].intValue;
                imgQR.Width = xcfg.Settings["completeMsg"]["QRcode"]["width"].intValue;
            }
            else
            {
                xcfg.Settings["completeMsg"]["text"].Value = lblCompleteMsg.Text; 
                xcfg.Settings["completeMsg"]["flash"].boolValue = completeMessageFlash;
                xcfg.Settings["completeMsg"]["showQRcode"].boolValue = true;
                xcfg.Settings["completeMsg"]["X"].intValue = lblCompleteMsg.Location.X;
                xcfg.Settings["completeMsd"]["Y"].intValue = lblCompleteMsg.Location.Y;
                xcfg.Settings["completeMsg"]["color"].Value = lblCompleteMsg.ForeColor.Name.ToString() ;

            }


            //Save settings incase anything was not specified and defaults were added
            xcfg.Save("config.xml");
            this.Location = new Point(20, 20);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Get Ready";
            Application.DoEvents();
            string img1 = Guid.NewGuid().ToString();
            string img2 = Guid.NewGuid().ToString();
            string img3 = Guid.NewGuid().ToString();
            string img4 = Guid.NewGuid().ToString();

            lblStatus.Text = "Smile!";
            Application.DoEvents();

            System.Diagnostics.Process capProcess = new System.Diagnostics.Process();
            capProcess.StartInfo.FileName = "gphoto2";
            capProcess.StartInfo.Arguments = string.Format(" --capture-image-and-download --filename {0}", img1);
            capProcess.Start();
            lblStatus.Text = "Processing Image";
            Application.DoEvents();
            capProcess.WaitForExit();
            imgStrip1.Image = System.Drawing.Image.FromFile(img1);

            lblStatus.Text = "Smile!";
            Application.DoEvents();
            capProcess.StartInfo.Arguments = string.Format(" --capture-image-and-download --filename {0}", img2);
            capProcess.Start();
            lblStatus.Text = "Processing Image";
            Application.DoEvents();
            capProcess.WaitForExit();
            imgStrip2.Image = System.Drawing.Image.FromFile(img2);
            
            lblStatus.Text = "Smile!";
            Application.DoEvents();
            capProcess.StartInfo.Arguments = string.Format(" --capture-image-and-download --filename {0}", img3);
            capProcess.Start();
            lblStatus.Text = "Processing Image";
            Application.DoEvents();
            capProcess.WaitForExit();
            imgStrip3.Image = System.Drawing.Image.FromFile(img3);

            lblStatus.Text = "Smile!";
            Application.DoEvents();
            capProcess.StartInfo.Arguments = string.Format(" --capture-image-and-download --filename {0}", img4);
            capProcess.Start();
            lblStatus.Text = "Processing Image";
            Application.DoEvents();
            capProcess.WaitForExit();
            imgStrip4.Image = System.Drawing.Image.FromFile(img4);
            lblStatus.Text = "Done!";


        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(e.KeyChar);
        }

        private void cmdStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == char.Parse("x"))
                Application.Exit();
        }


    }
}
