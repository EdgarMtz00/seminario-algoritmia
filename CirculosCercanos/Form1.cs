using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CirculosCercanos
{
    public partial class Form1 : Form
    {
        private Bitmap _bmpImage;
        private Bitmap _bmpAnimation;
        private CircleDetector _detector;
        private CircleGraph _graph;
        private int _selectedIndex;
        private int _indexAgente = -1;
        private int _indexDestino = -1;
        private bool secondRun = false;
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            _bmpImage = new Bitmap(openFileDialog.FileName);
            _bmpAnimation = new Bitmap(_bmpImage.Width, _bmpImage.Height);
            pictureBoxImage.BackgroundImage = _bmpImage;
            pictureBoxImage.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxImage.Image = _bmpAnimation;
            
            infoLabel.Text = "Haga click en la pestaña run para encontrar los circulos";
            _indexAgente = -1;
            _indexDestino = -1;
            secondRun = false;
            circleTree.Nodes.Clear();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!secondRun)
            {
                SearchCircles();
                secondRun = true;
            }
            else
            {
                runAnimation();
            }
            
        }

        private void SearchCircles()
        {
            if (_bmpImage != null)
            {
                infoLabel.Text = "Procesando...";
                _detector = new CircleDetector(_bmpImage, _bmpAnimation, openFileDialog.FileName);
                List<Circle> data = _detector.CircleSearch();
                _graph = new CircleGraph(data, openFileDialog.FileName);
                _detector.DrawEdges();
                if (data.Count > 0) 
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        circleTree.Nodes.Add(data[i].ToString());
                        foreach (Circle adjacent in data[i].Adjacents)
                        {
                            circleTree.Nodes[i].Nodes.Add(adjacent.ToString());
                        }
                    }
                    pictureBoxImage.Refresh();
                    infoLabel.Text = "Seleccione circulos de la ventana derecha y asignelos como agente o destino"; 
                }
                else
                {
                    infoLabel.Text = "No se encontraron circulos. Puede abrir otra imagen en la pestaña 'File'";
                }
            }
        }

        private void runAnimation()
        {
            if (_indexAgente != -1 && _indexDestino != -1 && _detector != null)
            {
                Circle agente = null;
                List<Circle> destinationList = _graph.FindDestination();
                if (destinationList == null)
                {
                    infoLabel.Text = "No Existe un camino entre esos dos circulos"; 
                    return;
                }
                foreach (Circle nextInPath in destinationList)
                {
                    if (agente == null)
                    {
                        agente = nextInPath;
                    }
                    else
                    {
                        List<Point> path = agente.Paths[nextInPath.Id];
                        for (int i = 0; i < path.Count; i += _bmpImage.Width / 75)
                        {
                            _detector.DrawAgent(path[i]);
                            pictureBoxImage.Refresh();
                        }

                        agente = nextInPath;
                    }
                }

                _graph.Reset();
            }
        }

        private void btnAgente_Click(object sender, EventArgs e)
        {
            _graph.SetAgent(_selectedIndex + 1);
            circleTree.Nodes[_selectedIndex].Text += " (Agente)";
            if (_indexAgente != -1)
            {
                circleTree.Nodes[_indexAgente].Text = circleTree.Nodes[_indexAgente].Text
                    .Substring(0, circleTree.Nodes[_indexAgente].Text.Length - 9);
            }

            _indexAgente = _selectedIndex;
            btnAgente.Enabled = false;
            btnDestino.Enabled = false;
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {
            _graph.SetDestiny(_selectedIndex + 1);
            circleTree.Nodes[_selectedIndex].Text += " (Destino)";
            if (_indexDestino != -1)
            {
                circleTree.Nodes[_indexDestino].Text = circleTree.Nodes[_indexDestino].Text
                    .Substring(0, circleTree.Nodes[_indexDestino].Text.Length - 10);

            }

            _indexDestino = _selectedIndex;
            btnAgente.Enabled = false;
            btnDestino.Enabled = false;
        }

        private void circleTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnAgente.Enabled = true;
            btnDestino.Enabled = true;
            _selectedIndex = e.Node.Index;
        }
    }
}