namespace Maze_Generator
{
    partial class MazeForm
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
            this.MazePicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MazePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MazePicture
            // 
            this.MazePicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MazePicture.Location = new System.Drawing.Point(12, 12);
            this.MazePicture.Margin = new System.Windows.Forms.Padding(2);
            this.MazePicture.Name = "MazePicture";
            this.MazePicture.Size = new System.Drawing.Size(765, 800);
            this.MazePicture.TabIndex = 0;
            this.MazePicture.TabStop = false;
            this.MazePicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MazePicture_MouseClick);
            // 
            // MazeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(789, 830);
            this.Controls.Add(this.MazePicture);
            this.Name = "MazeForm";
            this.Text = "MazeForm";
            ((System.ComponentModel.ISupportInitialize)(this.MazePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox MazePicture;
    }
}