namespace CirculosCercanos
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAgente = new System.Windows.Forms.Button();
            this.btnDestino = new System.Windows.Forms.Button();
            this.circleTree = new System.Windows.Forms.TreeView();
            this.infoLabel = new System.Windows.Forms.Label();
            this.miniToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.fileToolStripMenuItem, this.runToolStripMenuItem});
            this.miniToolStrip.Location = new System.Drawing.Point(0, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.miniToolStrip.Size = new System.Drawing.Size(1430, 24);
            this.miniToolStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.openToolStripMenuItem, this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 27);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(1104, 793);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 1;
            this.pictureBoxImage.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnAgente
            // 
            this.btnAgente.Enabled = false;
            this.btnAgente.Location = new System.Drawing.Point(1122, 800);
            this.btnAgente.Name = "btnAgente";
            this.btnAgente.Size = new System.Drawing.Size(141, 34);
            this.btnAgente.TabIndex = 3;
            this.btnAgente.Text = "Agente";
            this.btnAgente.UseVisualStyleBackColor = true;
            this.btnAgente.Click += new System.EventHandler(this.btnAgente_Click);
            // 
            // btnDestino
            // 
            this.btnDestino.Enabled = false;
            this.btnDestino.Location = new System.Drawing.Point(1277, 800);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(141, 34);
            this.btnDestino.TabIndex = 4;
            this.btnDestino.Text = "Destino";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // circleTree
            // 
            this.circleTree.Location = new System.Drawing.Point(1122, 27);
            this.circleTree.Name = "circleTree";
            this.circleTree.Size = new System.Drawing.Size(296, 767);
            this.circleTree.TabIndex = 5;
            this.circleTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.circleTree_AfterSelect);
            // 
            // infoLabel
            // 
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.infoLabel.Location = new System.Drawing.Point(12, 823);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(1104, 23);
            this.infoLabel.TabIndex = 6;
            this.infoLabel.Text = "Abre una imagen en la pestaña \"File\"";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1430, 846);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.circleTree);
            this.Controls.Add(this.btnDestino);
            this.Controls.Add(this.btnAgente);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.miniToolStrip);
            this.HelpButton = true;
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.miniToolStrip.ResumeLayout(false);
            this.miniToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label infoLabel;

        private System.Windows.Forms.TreeView circleTree;

        private System.Windows.Forms.Button btnAgente;
        private System.Windows.Forms.Button btnDestino;

        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;

        private System.Windows.Forms.OpenFileDialog openFileDialog;

        private System.Windows.Forms.PictureBox pictureBoxImage;

        private System.Windows.Forms.MenuStrip miniToolStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;

        #endregion
    }
}