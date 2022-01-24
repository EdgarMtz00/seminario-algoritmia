using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CirculosCercanos
{
    public partial class Form1 : Form
    {
        private Bitmap bmpImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Equals("Open"))
            {
                openFileDialog.ShowDialog();
                bmpImage = new Bitmap(openFileDialog.FileName);
                pictureBoxImage.Image = bmpImage;
            }
        }
    }
}