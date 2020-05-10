namespace MySQL_Taak_Week2_V2
{
    partial class Form2
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
            this.VoegOrderToeBtn = new System.Windows.Forms.Button();
            this.AantalNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.ProductenComboBox = new System.Windows.Forms.ComboBox();
            this.KlantenComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.AantalNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // VoegOrderToeBtn
            // 
            this.VoegOrderToeBtn.Location = new System.Drawing.Point(277, 281);
            this.VoegOrderToeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.VoegOrderToeBtn.Name = "VoegOrderToeBtn";
            this.VoegOrderToeBtn.Size = new System.Drawing.Size(367, 47);
            this.VoegOrderToeBtn.TabIndex = 11;
            this.VoegOrderToeBtn.Text = "Voeg Order Toe";
            this.VoegOrderToeBtn.UseVisualStyleBackColor = true;
            this.VoegOrderToeBtn.Click += new System.EventHandler(this.VoegOrderToeBtn_Click);
            // 
            // AantalNumUpDown
            // 
            this.AantalNumUpDown.Location = new System.Drawing.Point(318, 231);
            this.AantalNumUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.AantalNumUpDown.Name = "AantalNumUpDown";
            this.AantalNumUpDown.Size = new System.Drawing.Size(96, 22);
            this.AantalNumUpDown.TabIndex = 10;
            this.AantalNumUpDown.ValueChanged += new System.EventHandler(this.AantalNumUpDown_ValueChanged);
            // 
            // ProductenComboBox
            // 
            this.ProductenComboBox.FormattingEnabled = true;
            this.ProductenComboBox.Location = new System.Drawing.Point(318, 172);
            this.ProductenComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.ProductenComboBox.Name = "ProductenComboBox";
            this.ProductenComboBox.Size = new System.Drawing.Size(264, 24);
            this.ProductenComboBox.TabIndex = 9;
            this.ProductenComboBox.SelectedIndexChanged += new System.EventHandler(this.ProductenComboBox_SelectedIndexChanged);
            // 
            // KlantenComboBox
            // 
            this.KlantenComboBox.FormattingEnabled = true;
            this.KlantenComboBox.Location = new System.Drawing.Point(318, 111);
            this.KlantenComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.KlantenComboBox.Name = "KlantenComboBox";
            this.KlantenComboBox.Size = new System.Drawing.Size(264, 24);
            this.KlantenComboBox.TabIndex = 8;
            this.KlantenComboBox.SelectedIndexChanged += new System.EventHandler(this.KlantenComboBox_SelectedIndexChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.VoegOrderToeBtn);
            this.Controls.Add(this.AantalNumUpDown);
            this.Controls.Add(this.ProductenComboBox);
            this.Controls.Add(this.KlantenComboBox);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.AantalNumUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button VoegOrderToeBtn;
        private System.Windows.Forms.NumericUpDown AantalNumUpDown;
        private System.Windows.Forms.ComboBox ProductenComboBox;
        private System.Windows.Forms.ComboBox KlantenComboBox;
    }
}