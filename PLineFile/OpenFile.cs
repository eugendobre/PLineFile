using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace PLineFile
{
    class OpenFile
    {
        private string fileName;

        public string SelectedFileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public void SelectFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "All files (*.*)|*.*";
            openFile.RestoreDirectory = true;


            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = openFile.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: I cannot find the file. Original error: " + ex.Message);
                }
            }
        }

        
        
    }
}
