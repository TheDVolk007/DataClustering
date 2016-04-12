namespace ClusteringPresentation
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
            this.MainCanvas = new System.Windows.Forms.PictureBox();
            this.initialize = new System.Windows.Forms.Button();
            this.performKMeans = new System.Windows.Forms.Button();
            this.informer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // MainCanvas
            // 
            this.MainCanvas.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainCanvas.Location = new System.Drawing.Point(0, 0);
            this.MainCanvas.Name = "MainCanvas";
            this.MainCanvas.Size = new System.Drawing.Size(917, 600);
            this.MainCanvas.TabIndex = 0;
            this.MainCanvas.TabStop = false;
            this.MainCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.MainCanvas_Paint);
            // 
            // initialize
            // 
            this.initialize.Location = new System.Drawing.Point(12, 606);
            this.initialize.Name = "initialize";
            this.initialize.Size = new System.Drawing.Size(75, 23);
            this.initialize.TabIndex = 1;
            this.initialize.Text = "Initialization";
            this.initialize.UseVisualStyleBackColor = true;
            this.initialize.Click += new System.EventHandler(this.initialize_Click);
            // 
            // performKMeans
            // 
            this.performKMeans.Location = new System.Drawing.Point(93, 606);
            this.performKMeans.Name = "performKMeans";
            this.performKMeans.Size = new System.Drawing.Size(153, 23);
            this.performKMeans.TabIndex = 2;
            this.performKMeans.Text = "Perform K-means clustering";
            this.performKMeans.UseVisualStyleBackColor = true;
            this.performKMeans.Click += new System.EventHandler(this.performKMeans_Click);
            // 
            // informer
            // 
            this.informer.AutoSize = true;
            this.informer.Location = new System.Drawing.Point(252, 611);
            this.informer.Name = "informer";
            this.informer.Size = new System.Drawing.Size(0, 13);
            this.informer.TabIndex = 3;
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 641);
            this.Controls.Add(this.informer);
            this.Controls.Add(this.performKMeans);
            this.Controls.Add(this.initialize);
            this.Controls.Add(this.MainCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.MainCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainCanvas;
        private System.Windows.Forms.Button initialize;
        private System.Windows.Forms.Button performKMeans;
        private System.Windows.Forms.Label informer;
    }
}

