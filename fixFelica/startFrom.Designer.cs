namespace fixFelica
{
    partial class startFrom
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
            this.startBut = new System.Windows.Forms.Button();
            this.textBoxExplanation = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // startBut
            // 
            this.startBut.Location = new System.Drawing.Point(274, 257);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(313, 116);
            this.startBut.TabIndex = 0;
            this.startBut.Text = "Start";
            this.startBut.UseVisualStyleBackColor = true;
            this.startBut.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxExplanation
            // 
            this.textBoxExplanation.AcceptsReturn = true;
            this.textBoxExplanation.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxExplanation.Location = new System.Drawing.Point(169, 38);
            this.textBoxExplanation.Multiline = true;
            this.textBoxExplanation.Name = "textBoxExplanation";
            this.textBoxExplanation.Size = new System.Drawing.Size(579, 185);
            this.textBoxExplanation.TabIndex = 6;
            this.textBoxExplanation.Text = "使い方を説明いたします。\r\nカードをタッチし、\r\n指紋チェックボタンを触。\r\n終わったらボタンをクリックしてください。\r\n1秒をお待ちください。\r\n";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // startFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(845, 425);
            this.Controls.Add(this.textBoxExplanation);
            this.Controls.Add(this.startBut);
            this.Name = "startFrom";
            this.Text = "StartFrom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.TextBox textBoxExplanation;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Timer timer1;
    }
}