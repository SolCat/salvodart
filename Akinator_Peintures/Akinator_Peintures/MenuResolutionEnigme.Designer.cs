namespace Akinator_Peintures
{
    partial class MenuResolutionEnigme
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuResolutionEnigme));
            this.OeuvreLbl = new System.Windows.Forms.Label();
            this.JouerNvllePartieLbl = new System.Windows.Forms.Label();
            this.OuiBtn = new System.Windows.Forms.Button();
            this.NonBtn = new System.Windows.Forms.Button();
            this.PeintureLbl = new System.Windows.Forms.Label();
            this.PeintreLbl = new System.Windows.Forms.Label();
            this.GenreLbl = new System.Windows.Forms.Label();
            this.MotsClefsLbl = new System.Windows.Forms.Label();
            this.MotClef1TBox = new System.Windows.Forms.TextBox();
            this.oeuvreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.akina_peinturesDataSet = new Akinator_Peintures.Akina_peinturesDataSet();
            this.InfosGBox = new System.Windows.Forms.GroupBox();
            this.GenreCbox = new System.Windows.Forms.ComboBox();
            this.QuestionCBox = new System.Windows.Forms.ComboBox();
            this.PeintreCBox = new System.Windows.Forms.ComboBox();
            this.OeuvreTBox = new System.Windows.Forms.TextBox();
            this.MotsPBox = new System.Windows.Forms.PictureBox();
            this.GenrePBox = new System.Windows.Forms.PictureBox();
            this.PeintrePBox = new System.Windows.Forms.PictureBox();
            this.OeuvrePBox = new System.Windows.Forms.PictureBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.oeuvreTableAdapter = new Akinator_Peintures.Akina_peinturesDataSetTableAdapters.OeuvreTableAdapter();
            this.DaliPanel = new System.Windows.Forms.Panel();
            this.PeinturePBox = new System.Windows.Forms.PictureBox();
            this.fKConnaissanceOeuvreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.connaissanceTableAdapter = new Akinator_Peintures.Akina_peinturesDataSetTableAdapters.ConnaissanceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.oeuvreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akina_peinturesDataSet)).BeginInit();
            this.InfosGBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MotsPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenrePBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeintrePBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OeuvrePBox)).BeginInit();
            this.DaliPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeinturePBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKConnaissanceOeuvreBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // OeuvreLbl
            // 
            this.OeuvreLbl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.OeuvreLbl.Font = new System.Drawing.Font("Segoe Print", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OeuvreLbl.Location = new System.Drawing.Point(30, 33);
            this.OeuvreLbl.Name = "OeuvreLbl";
            this.OeuvreLbl.Size = new System.Drawing.Size(540, 127);
            this.OeuvreLbl.TabIndex = 0;
            this.OeuvreLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JouerNvllePartieLbl
            // 
            this.JouerNvllePartieLbl.BackColor = System.Drawing.Color.Transparent;
            this.JouerNvllePartieLbl.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JouerNvllePartieLbl.Location = new System.Drawing.Point(132, 366);
            this.JouerNvllePartieLbl.Name = "JouerNvllePartieLbl";
            this.JouerNvllePartieLbl.Size = new System.Drawing.Size(323, 30);
            this.JouerNvllePartieLbl.TabIndex = 1;
            this.JouerNvllePartieLbl.Text = "On joue une nouvelle partie ?";
            this.JouerNvllePartieLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OuiBtn
            // 
            this.OuiBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.OuiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OuiBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OuiBtn.Location = new System.Drawing.Point(120, 593);
            this.OuiBtn.Name = "OuiBtn";
            this.OuiBtn.Size = new System.Drawing.Size(147, 72);
            this.OuiBtn.TabIndex = 2;
            this.OuiBtn.Text = "Oui";
            this.OuiBtn.UseVisualStyleBackColor = false;
            this.OuiBtn.Click += new System.EventHandler(this.OuiBtn_Click);
            // 
            // NonBtn
            // 
            this.NonBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NonBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NonBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NonBtn.Location = new System.Drawing.Point(315, 593);
            this.NonBtn.Name = "NonBtn";
            this.NonBtn.Size = new System.Drawing.Size(147, 72);
            this.NonBtn.TabIndex = 3;
            this.NonBtn.Text = "Non";
            this.NonBtn.UseVisualStyleBackColor = false;
            // 
            // PeintureLbl
            // 
            this.PeintureLbl.BackColor = System.Drawing.Color.Transparent;
            this.PeintureLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PeintureLbl.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeintureLbl.Location = new System.Drawing.Point(74, 26);
            this.PeintureLbl.Name = "PeintureLbl";
            this.PeintureLbl.Size = new System.Drawing.Size(100, 36);
            this.PeintureLbl.TabIndex = 5;
            this.PeintureLbl.Text = "Oeuvre";
            this.PeintureLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PeintreLbl
            // 
            this.PeintreLbl.BackColor = System.Drawing.Color.Transparent;
            this.PeintreLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PeintreLbl.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeintreLbl.Location = new System.Drawing.Point(75, 73);
            this.PeintreLbl.Name = "PeintreLbl";
            this.PeintreLbl.Size = new System.Drawing.Size(100, 34);
            this.PeintreLbl.TabIndex = 7;
            this.PeintreLbl.Text = "Peintre";
            this.PeintreLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GenreLbl
            // 
            this.GenreLbl.BackColor = System.Drawing.Color.Transparent;
            this.GenreLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenreLbl.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenreLbl.Location = new System.Drawing.Point(71, 118);
            this.GenreLbl.Name = "GenreLbl";
            this.GenreLbl.Size = new System.Drawing.Size(100, 36);
            this.GenreLbl.TabIndex = 8;
            this.GenreLbl.Text = "Genre";
            this.GenreLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MotsClefsLbl
            // 
            this.MotsClefsLbl.BackColor = System.Drawing.Color.Transparent;
            this.MotsClefsLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MotsClefsLbl.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MotsClefsLbl.Location = new System.Drawing.Point(85, 165);
            this.MotsClefsLbl.Name = "MotsClefsLbl";
            this.MotsClefsLbl.Size = new System.Drawing.Size(100, 56);
            this.MotsClefsLbl.TabIndex = 10;
            this.MotsClefsLbl.Text = "Mots clefs\r\n(facultatif)";
            this.MotsClefsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MotClef1TBox
            // 
            this.MotClef1TBox.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MotClef1TBox.Location = new System.Drawing.Point(243, 210);
            this.MotClef1TBox.Name = "MotClef1TBox";
            this.MotClef1TBox.Size = new System.Drawing.Size(170, 29);
            this.MotClef1TBox.TabIndex = 11;
            this.MotClef1TBox.Tag = "4";
            // 
            // oeuvreBindingSource
            // 
            this.oeuvreBindingSource.DataMember = "Oeuvre";
            this.oeuvreBindingSource.DataSource = this.akina_peinturesDataSet;
            // 
            // akina_peinturesDataSet
            // 
            this.akina_peinturesDataSet.DataSetName = "Akina_peinturesDataSet";
            this.akina_peinturesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // InfosGBox
            // 
            this.InfosGBox.Controls.Add(this.GenreCbox);
            this.InfosGBox.Controls.Add(this.QuestionCBox);
            this.InfosGBox.Controls.Add(this.PeintreCBox);
            this.InfosGBox.Controls.Add(this.OeuvreTBox);
            this.InfosGBox.Controls.Add(this.MotsPBox);
            this.InfosGBox.Controls.Add(this.GenrePBox);
            this.InfosGBox.Controls.Add(this.PeintrePBox);
            this.InfosGBox.Controls.Add(this.OeuvrePBox);
            this.InfosGBox.Controls.Add(this.PeintureLbl);
            this.InfosGBox.Controls.Add(this.PeintreLbl);
            this.InfosGBox.Controls.Add(this.MotClef1TBox);
            this.InfosGBox.Controls.Add(this.GenreLbl);
            this.InfosGBox.Controls.Add(this.MotsClefsLbl);
            this.InfosGBox.Location = new System.Drawing.Point(30, 195);
            this.InfosGBox.Name = "InfosGBox";
            this.InfosGBox.Size = new System.Drawing.Size(540, 251);
            this.InfosGBox.TabIndex = 16;
            this.InfosGBox.TabStop = false;
            // 
            // GenreCbox
            // 
            this.GenreCbox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.oeuvreBindingSource, "Artiste", true));
            this.GenreCbox.DataSource = this.oeuvreBindingSource;
            this.GenreCbox.DisplayMember = "Artiste";
            this.GenreCbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenreCbox.FormattingEnabled = true;
            this.GenreCbox.Location = new System.Drawing.Point(243, 122);
            this.GenreCbox.Name = "GenreCbox";
            this.GenreCbox.Size = new System.Drawing.Size(225, 29);
            this.GenreCbox.TabIndex = 23;
            this.GenreCbox.Tag = "2";
            this.GenreCbox.ValueMember = "Artiste";
            // 
            // QuestionCBox
            // 
            this.QuestionCBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.oeuvreBindingSource, "Genre", true));
            this.QuestionCBox.DataSource = this.oeuvreBindingSource;
            this.QuestionCBox.DisplayMember = "Genre";
            this.QuestionCBox.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestionCBox.FormattingEnabled = true;
            this.QuestionCBox.ItemHeight = 21;
            this.QuestionCBox.Location = new System.Drawing.Point(243, 170);
            this.QuestionCBox.Name = "QuestionCBox";
            this.QuestionCBox.Size = new System.Drawing.Size(225, 29);
            this.QuestionCBox.TabIndex = 22;
            this.QuestionCBox.Tag = "3";
            this.QuestionCBox.ValueMember = "Genre";
            // 
            // PeintreCBox
            // 
            this.PeintreCBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.oeuvreBindingSource, "Artiste", true));
            this.PeintreCBox.DataSource = this.oeuvreBindingSource;
            this.PeintreCBox.DisplayMember = "Artiste";
            this.PeintreCBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeintreCBox.FormattingEnabled = true;
            this.PeintreCBox.Location = new System.Drawing.Point(243, 77);
            this.PeintreCBox.Name = "PeintreCBox";
            this.PeintreCBox.Size = new System.Drawing.Size(225, 29);
            this.PeintreCBox.TabIndex = 18;
            this.PeintreCBox.Tag = "2";
            this.PeintreCBox.ValueMember = "Artiste";
            // 
            // OeuvreTBox
            // 
            this.OeuvreTBox.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OeuvreTBox.Location = new System.Drawing.Point(243, 32);
            this.OeuvreTBox.Name = "OeuvreTBox";
            this.OeuvreTBox.Size = new System.Drawing.Size(225, 29);
            this.OeuvreTBox.TabIndex = 21;
            this.OeuvreTBox.Tag = "1";
            this.OeuvreTBox.TextChanged += new System.EventHandler(this.OeuvreTBox_TextChanged);
            // 
            // MotsPBox
            // 
            this.MotsPBox.Image = ((System.Drawing.Image)(resources.GetObject("MotsPBox.Image")));
            this.MotsPBox.Location = new System.Drawing.Point(37, 168);
            this.MotsPBox.Name = "MotsPBox";
            this.MotsPBox.Size = new System.Drawing.Size(32, 31);
            this.MotsPBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MotsPBox.TabIndex = 19;
            this.MotsPBox.TabStop = false;
            // 
            // GenrePBox
            // 
            this.GenrePBox.Image = ((System.Drawing.Image)(resources.GetObject("GenrePBox.Image")));
            this.GenrePBox.Location = new System.Drawing.Point(37, 123);
            this.GenrePBox.Name = "GenrePBox";
            this.GenrePBox.Size = new System.Drawing.Size(32, 33);
            this.GenrePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GenrePBox.TabIndex = 18;
            this.GenrePBox.TabStop = false;
            // 
            // PeintrePBox
            // 
            this.PeintrePBox.Image = ((System.Drawing.Image)(resources.GetObject("PeintrePBox.Image")));
            this.PeintrePBox.Location = new System.Drawing.Point(36, 77);
            this.PeintrePBox.Name = "PeintrePBox";
            this.PeintrePBox.Size = new System.Drawing.Size(32, 33);
            this.PeintrePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PeintrePBox.TabIndex = 17;
            this.PeintrePBox.TabStop = false;
            // 
            // OeuvrePBox
            // 
            this.OeuvrePBox.Image = ((System.Drawing.Image)(resources.GetObject("OeuvrePBox.Image")));
            this.OeuvrePBox.Location = new System.Drawing.Point(36, 32);
            this.OeuvrePBox.Name = "OeuvrePBox";
            this.OeuvrePBox.Size = new System.Drawing.Size(33, 30);
            this.OeuvrePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OeuvrePBox.TabIndex = 16;
            this.OeuvrePBox.TabStop = false;
            // 
            // OkBtn
            // 
            this.OkBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.OkBtn.Enabled = false;
            this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkBtn.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkBtn.Location = new System.Drawing.Point(177, 505);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(241, 97);
            this.OkBtn.TabIndex = 17;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = false;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // oeuvreTableAdapter
            // 
            this.oeuvreTableAdapter.ClearBeforeFill = true;
            // 
            // DaliPanel
            // 
            this.DaliPanel.BackColor = System.Drawing.Color.Transparent;
            this.DaliPanel.Controls.Add(this.PeinturePBox);
            this.DaliPanel.Controls.Add(this.JouerNvllePartieLbl);
            this.DaliPanel.Location = new System.Drawing.Point(7, 176);
            this.DaliPanel.Name = "DaliPanel";
            this.DaliPanel.Size = new System.Drawing.Size(581, 405);
            this.DaliPanel.TabIndex = 18;
            // 
            // PeinturePBox
            // 
            this.PeinturePBox.Location = new System.Drawing.Point(76, 1);
            this.PeinturePBox.Name = "PeinturePBox";
            this.PeinturePBox.Size = new System.Drawing.Size(429, 350);
            this.PeinturePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PeinturePBox.TabIndex = 2;
            this.PeinturePBox.TabStop = false;
            this.PeinturePBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AfficherInformations);
            this.PeinturePBox.MouseLeave += new System.EventHandler(this.PeinturePBox_MouseLeave);
            this.PeinturePBox.MouseHover += new System.EventHandler(this.ObtenirInformationsTableau);
            // 
            // fKConnaissanceOeuvreBindingSource
            // 
            this.fKConnaissanceOeuvreBindingSource.DataMember = "FK_Connaissance_Oeuvre";
            this.fKConnaissanceOeuvreBindingSource.DataSource = this.oeuvreBindingSource;
            // 
            // connaissanceTableAdapter
            // 
            this.connaissanceTableAdapter.ClearBeforeFill = true;
            // 
            // MenuResolutionEnigme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(596, 687);
            this.Controls.Add(this.DaliPanel);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.InfosGBox);
            this.Controls.Add(this.NonBtn);
            this.Controls.Add(this.OuiBtn);
            this.Controls.Add(this.OeuvreLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MenuResolutionEnigme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MenuResolutionEnigme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.oeuvreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akina_peinturesDataSet)).EndInit();
            this.InfosGBox.ResumeLayout(false);
            this.InfosGBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MotsPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenrePBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeintrePBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OeuvrePBox)).EndInit();
            this.DaliPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PeinturePBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKConnaissanceOeuvreBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label OeuvreLbl;
        private System.Windows.Forms.Label JouerNvllePartieLbl;
        private System.Windows.Forms.Button OuiBtn;
        private System.Windows.Forms.Button NonBtn;
        private System.Windows.Forms.Label PeintureLbl;
        private System.Windows.Forms.Label PeintreLbl;
        private System.Windows.Forms.Label GenreLbl;
        private System.Windows.Forms.Label MotsClefsLbl;
        private System.Windows.Forms.TextBox MotClef1TBox;
        private System.Windows.Forms.GroupBox InfosGBox;
        private System.Windows.Forms.PictureBox MotsPBox;
        private System.Windows.Forms.PictureBox GenrePBox;
        private System.Windows.Forms.PictureBox PeintrePBox;
        private System.Windows.Forms.PictureBox OeuvrePBox;
        private System.Windows.Forms.Button OkBtn;
        private Akina_peinturesDataSet akina_peinturesDataSet;
        private System.Windows.Forms.BindingSource oeuvreBindingSource;
        private Akina_peinturesDataSetTableAdapters.OeuvreTableAdapter oeuvreTableAdapter;
        private System.Windows.Forms.TextBox OeuvreTBox;
        private System.Windows.Forms.ComboBox PeintreCBox;
        private System.Windows.Forms.ComboBox QuestionCBox;
        private System.Windows.Forms.Panel DaliPanel;
        private System.Windows.Forms.BindingSource fKConnaissanceOeuvreBindingSource;
        private Akina_peinturesDataSetTableAdapters.ConnaissanceTableAdapter connaissanceTableAdapter;
        private System.Windows.Forms.ComboBox GenreCbox;
        private System.Windows.Forms.PictureBox PeinturePBox;
    }
}