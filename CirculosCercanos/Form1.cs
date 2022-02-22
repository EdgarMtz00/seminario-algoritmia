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
        private Bitmap _bmpImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
                openFileDialog.ShowDialog();
                _bmpImage = new Bitmap(openFileDialog.FileName);
                pictureBoxImage.Image = _bmpImage;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_bmpImage != null)
            {
                CircleDetector detector = new CircleDetector(_bmpImage, openFileDialog.FileName);
                List<Circle> data = detector.CircleSearch();
                if (data.Count > 0) 
                {
                    circlesListBox.DataSource = data;   
                    pictureBoxImage.Refresh(); 
                }
                else
                {
                    List<String> noData = new List<String>();
                    noData.Add("No se encontraron circulos");
                    circlesListBox.DataSource = noData;
                }
                
            }
            else
            {
                List<String> noData = new List<string>();
                noData.Add("No se ha seleccionado una imagen");
                circlesListBox.DataSource = noData; 
            }
        }
    }
}