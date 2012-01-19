using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monobooth
{
    class Sharing
    {
        public void uploadFtp(string filename, string host, string username, string password, string path)
        {
            FtpLib.FtpConnection ftpConn = new FtpLib.FtpConnection(host, username, password);
            ftpConn.Open();
            ftpConn.Login();
            ftpConn.SetCurrentDirectory(path);
            ftpConn.PutFile(filename);
            ftpConn.Close();
        }
    }
}
