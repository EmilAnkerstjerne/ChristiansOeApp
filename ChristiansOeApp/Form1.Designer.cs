namespace ChristiansOeApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mapButton = new System.Windows.Forms.Button();
            this.storiesButton = new System.Windows.Forms.Button();
            this.backToShipButton = new System.Windows.Forms.Button();
            this.mapPicture = new System.Windows.Forms.PictureBox();
            this.distToShip = new System.Windows.Forms.Label();
            this.timeToShip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // mapButton
            // 
            this.mapButton.Location = new System.Drawing.Point(12, 12);
            this.mapButton.Name = "mapButton";
            this.mapButton.Size = new System.Drawing.Size(90, 90);
            this.mapButton.TabIndex = 0;
            this.mapButton.Text = "Kort";
            this.mapButton.UseVisualStyleBackColor = true;
            this.mapButton.Click += new System.EventHandler(this.mapButton_Click);
            // 
            // storiesButton
            // 
            this.storiesButton.Location = new System.Drawing.Point(108, 12);
            this.storiesButton.Name = "storiesButton";
            this.storiesButton.Size = new System.Drawing.Size(90, 90);
            this.storiesButton.TabIndex = 1;
            this.storiesButton.Text = "Fortællinger";
            this.storiesButton.UseVisualStyleBackColor = true;
            this.storiesButton.Click += new System.EventHandler(this.stories_Click);
            // 
            // backToShipButton
            // 
            this.backToShipButton.Location = new System.Drawing.Point(204, 12);
            this.backToShipButton.Name = "backToShipButton";
            this.backToShipButton.Size = new System.Drawing.Size(90, 90);
            this.backToShipButton.TabIndex = 2;
            this.backToShipButton.Text = "Tilbage til færgen";
            this.backToShipButton.UseVisualStyleBackColor = true;
            this.backToShipButton.Click += new System.EventHandler(this.backToShipButton_Click);
            // 
            // mapPicture
            // 
            this.mapPicture.Image = ((System.Drawing.Image)(resources.GetObject("mapPicture.Image")));
            this.mapPicture.Location = new System.Drawing.Point(12, 108);
            this.mapPicture.Name = "mapPicture";
            this.mapPicture.Size = new System.Drawing.Size(1077, 776);
            this.mapPicture.TabIndex = 3;
            this.mapPicture.TabStop = false;
            this.mapPicture.Visible = false;
            // 
            // distToShip
            // 
            this.distToShip.AutoSize = true;
            this.distToShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distToShip.Location = new System.Drawing.Point(12, 108);
            this.distToShip.Name = "distToShip";
            this.distToShip.Size = new System.Drawing.Size(522, 108);
            this.distToShip.TabIndex = 4;
            this.distToShip.Text = "Ingen GPS";
            this.distToShip.Visible = false;
            // 
            // timeToShip
            // 
            this.timeToShip.AutoSize = true;
            this.timeToShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeToShip.Location = new System.Drawing.Point(12, 216);
            this.timeToShip.Name = "timeToShip";
            this.timeToShip.Size = new System.Drawing.Size(0, 108);
            this.timeToShip.TabIndex = 5;
            this.timeToShip.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 575);
            this.Controls.Add(this.timeToShip);
            this.Controls.Add(this.distToShip);
            this.Controls.Add(this.mapPicture);
            this.Controls.Add(this.backToShipButton);
            this.Controls.Add(this.storiesButton);
            this.Controls.Add(this.mapButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mapButton;
        private System.Windows.Forms.Button storiesButton;
        private System.Windows.Forms.Button backToShipButton;
        private System.Windows.Forms.PictureBox mapPicture;
        private System.Windows.Forms.Label distToShip;
        private System.Windows.Forms.Label timeToShip;
    }
}

