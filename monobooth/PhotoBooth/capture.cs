using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBooth
{
    class capture
    {
        public bool capturing = false;

        public System.Drawing.Image captureImage(string destFileName)
        {
            System.Diagnostics.Process capProcess = new System.Diagnostics.Process();
            capProcess.StartInfo.FileName = "gphoto2";
            capProcess.StartInfo.Arguments = string.Format(" --capture-image-and-download --filename {0}", destFileName);
            capProcess.Exited += new EventHandler(capProcess_Exited);
            capProcess.Start();
            capturing = true;
            return System.Drawing.Image.FromFile(destFileName);
        }

        void capProcess_Exited(object sender, EventArgs e)
        {
            capturing = false;

        }

        
    }
}
