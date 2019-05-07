namespace Akinator_Peintures
{
    partial class MenuQuestion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuQuestion));
            this.QuestionLbl = new System.Windows.Forms.Label();
            this.BoutonsPnl = new System.Windows.Forms.Panel();
            this.OuiBtn = new System.Windows.Forms.Button();
            this.NonBtn = new System.Windows.Forms.Button();
            this.NspBtn = new System.Windows.Forms.Button();
            this.AkinatorPBox = new System.Windows.Forms.PictureBox();
            this.MenuLbl = new System.Windows.Forms.Label();
            this.BullePBox = new System.Windows.Forms.PictureBox();
            this.BoutonsPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AkinatorPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BullePBox)).BeginInit();
            this.SuspendLayout();
            // 
            // QuestionLbl
            // 
            this.QuestionLbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.QuestionLbl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.QuestionLbl.Font = new System.Drawing.Font("Segoe Print", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestionLbl.Location = new System.Drawing.Point(35, 88);
            this.QuestionLbl.Name = "QuestionLbl";
            this.QuestionLbl.Size = new System.Drawing.Size(253, 135);
            this.QuestionLbl.TabIndex = 0;
            this.QuestionLbl.Text = "l";
            this.QuestionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BoutonsPnl
            // 
            this.BoutonsPnl.BackColor = System.Drawing.Color.Transparent;
            this.BoutonsPnl.Controls.Add(this.OuiBtn);
            this.BoutonsPnl.Controls.Add(this.NonBtn);
            this.BoutonsPnl.Controls.Add(this.NspBtn);
            this.BoutonsPnl.Location = new System.Drawing.Point(12, 519);
            this.BoutonsPnl.Name = "BoutonsPnl";
            this.BoutonsPnl.Size = new System.Drawing.Size(567, 140);
            this.BoutonsPnl.TabIndex = 1;
            // 
            // OuiBtn
            // 
            this.OuiBtn.BackColor = System.Drawing.SystemColors.Window;
            this.OuiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OuiBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OuiBtn.Location = new System.Drawing.Point(15, 24);
            this.OuiBtn.Name = "OuiBtn";
            this.OuiBtn.Size = new System.Drawing.Size(153, 67);
            this.OuiBtn.TabIndex = 4;
            this.OuiBtn.TabStop = false;
            this.OuiBtn.Tag = "1";
            this.OuiBtn.Text = "Oui";
            this.OuiBtn.UseVisualStyleBackColor = false;
            this.OuiBtn.Click += new System.EventHandler(this.ChoisirReponse);
            // 
            // NonBtn
            // 
            this.NonBtn.BackColor = System.Drawing.SystemColors.Window;
            this.NonBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NonBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NonBtn.Location = new System.Drawing.Point(205, 24);
            this.NonBtn.Name = "NonBtn";
            this.NonBtn.Size = new System.Drawing.Size(153, 66);
            this.NonBtn.TabIndex = 3;
            this.NonBtn.TabStop = false;
            this.NonBtn.Tag = "2";
            this.NonBtn.Text = "Non";
            this.NonBtn.UseVisualStyleBackColor = false;
            this.NonBtn.Click += new System.EventHandler(this.ChoisirReponse);
            // 
            // NspBtn
            // 
            this.NspBtn.BackColor = System.Drawing.SystemColors.Window;
            this.NspBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NspBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NspBtn.Location = new System.Drawing.Point(393, 24);
            this.NspBtn.Name = "NspBtn";
            this.NspBtn.Size = new System.Drawing.Size(153, 66);
            this.NspBtn.TabIndex = 2;
            this.NspBtn.TabStop = false;
            this.NspBtn.Tag = "3";
            this.NspBtn.Text = "Je ne sais pas";
            this.NspBtn.UseVisualStyleBackColor = false;
            this.NspBtn.Click += new System.EventHandler(this.ChoisirReponse);
            // 
            // AkinatorPBox
            // 
            this.AkinatorPBox.Image = ((System.Drawing.Image)(resources.GetObject("AkinatorPBox.Image")));
            this.AkinatorPBox.Location = new System.Drawing.Point(340, 88);
            this.AkinatorPBox.Name = "AkinatorPBox";
            this.AkinatorPBox.Size = new System.Drawing.Size(227, 395);
            this.AkinatorPBox.TabIndex = 8;
            this.AkinatorPBox.TabStop = false;
            // 
            // MenuLbl
            // 
            this.MenuLbl.BackColor = System.Drawing.SystemColors.Window;
            this.MenuLbl.Font = new System.Drawing.Font("Segoe Print", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuLbl.Location = new System.Drawing.Point(11, 9);
            this.MenuLbl.Name = "MenuLbl";
            this.MenuLbl.Size = new System.Drawing.Size(568, 44);
            this.MenuLbl.TabIndex = 9;
            this.MenuLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BullePBox
            // 
            this.BullePBox.BackColor = System.Drawing.Color.Transparent;
            this.BullePBox.Image = ((System.Drawing.Image)(resources.GetObject("BullePBox.Image")));
            this.BullePBox.Location = new System.Drawing.Point(6, 79);
            this.BullePBox.Name = "BullePBox";
            this.BullePBox.Size = new System.Drawing.Size(343, 201);
            this.BullePBox.TabIndex = 10;
            this.BullePBox.TabStop = false;
            // 
            // MenuQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(592, 687);
            this.Controls.Add(this.BullePBox);
            this.Controls.Add(this.MenuLbl);
            this.Controls.Add(this.AkinatorPBox);
            this.Controls.Add(this.BoutonsPnl);
            this.Controls.Add(this.QuestionLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MenuQuestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MenuQuestion_Load);
            this.BoutonsPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AkinatorPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BullePBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label QuestionLbl;
        private System.Windows.Forms.Panel BoutonsPnl;
        private System.Windows.Forms.Button OuiBtn;
        private System.Windows.Forms.Button NonBtn;
        private System.Windows.Forms.Button NspBtn;
        private System.Windows.Forms.PictureBox AkinatorPBox;
        private System.Windows.Forms.Label MenuLbl;
        private System.Windows.Forms.PictureBox BullePBox;
    }
}