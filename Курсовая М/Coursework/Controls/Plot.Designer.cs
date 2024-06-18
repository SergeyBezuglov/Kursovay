namespace OstapenkoAE.Controls
{
    partial class Plot
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelYunits = new System.Windows.Forms.Label();
            this.labelXname = new System.Windows.Forms.Label();
            this.labelYvalue = new System.Windows.Forms.Label();
            this.labelXvalue = new System.Windows.Forms.Label();
            this.labelYname = new System.Windows.Forms.Label();
            this.labelXunits = new System.Windows.Forms.Label();
            this.pictureBoxPlot = new System.Windows.Forms.PictureBox();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.dataGridViewLegend = new System.Windows.Forms.DataGridView();
            this.labelLegend = new System.Windows.Forms.Label();
            this.buttonLegend = new System.Windows.Forms.Button();
            this.ChartLegend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChartColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlot)).BeginInit();
            this.panelLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLegend)).BeginInit();
            this.SuspendLayout();
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.buttonLegend);
            this.panelInfo.Controls.Add(this.labelYunits);
            this.panelInfo.Controls.Add(this.labelXname);
            this.panelInfo.Controls.Add(this.labelYvalue);
            this.panelInfo.Controls.Add(this.labelXvalue);
            this.panelInfo.Controls.Add(this.labelYname);
            this.panelInfo.Controls.Add(this.labelXunits);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInfo.Location = new System.Drawing.Point(0, 180);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(320, 20);
            this.panelInfo.TabIndex = 0;
            // 
            // labelYunits
            // 
            this.labelYunits.AutoSize = true;
            this.labelYunits.Location = new System.Drawing.Point(251, 3);
            this.labelYunits.Name = "labelYunits";
            this.labelYunits.Size = new System.Drawing.Size(35, 13);
            this.labelYunits.TabIndex = 6;
            this.labelYunits.Text = "[units]";
            // 
            // labelXname
            // 
            this.labelXname.AutoSize = true;
            this.labelXname.Location = new System.Drawing.Point(3, 3);
            this.labelXname.Name = "labelXname";
            this.labelXname.Size = new System.Drawing.Size(23, 13);
            this.labelXname.TabIndex = 0;
            this.labelXname.Text = "X =";
            // 
            // labelYvalue
            // 
            this.labelYvalue.AutoSize = true;
            this.labelYvalue.Location = new System.Drawing.Point(190, 3);
            this.labelYvalue.Name = "labelYvalue";
            this.labelYvalue.Size = new System.Drawing.Size(55, 13);
            this.labelYvalue.TabIndex = 5;
            this.labelYvalue.Text = "00000000";
            // 
            // labelXvalue
            // 
            this.labelXvalue.AutoSize = true;
            this.labelXvalue.Location = new System.Drawing.Point(42, 3);
            this.labelXvalue.Name = "labelXvalue";
            this.labelXvalue.Size = new System.Drawing.Size(55, 13);
            this.labelXvalue.TabIndex = 2;
            this.labelXvalue.Text = "00000000";
            // 
            // labelYname
            // 
            this.labelYname.AutoSize = true;
            this.labelYname.Location = new System.Drawing.Point(147, 3);
            this.labelYname.Name = "labelYname";
            this.labelYname.Size = new System.Drawing.Size(23, 13);
            this.labelYname.TabIndex = 4;
            this.labelYname.Text = "Y =";
            // 
            // labelXunits
            // 
            this.labelXunits.AutoSize = true;
            this.labelXunits.Location = new System.Drawing.Point(103, 3);
            this.labelXunits.Name = "labelXunits";
            this.labelXunits.Size = new System.Drawing.Size(35, 13);
            this.labelXunits.TabIndex = 3;
            this.labelXunits.Text = "[units]";
            // 
            // pictureBoxPlot
            // 
            this.pictureBoxPlot.BackColor = System.Drawing.Color.White;
            this.pictureBoxPlot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPlot.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPlot.Name = "pictureBoxPlot";
            this.pictureBoxPlot.Size = new System.Drawing.Size(320, 180);
            this.pictureBoxPlot.TabIndex = 1;
            this.pictureBoxPlot.TabStop = false;
            this.pictureBoxPlot.SizeChanged += new System.EventHandler(this.pictureBoxPlot_SizeChanged);
            this.pictureBoxPlot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlot_MouseDown);
            this.pictureBoxPlot.MouseEnter += new System.EventHandler(this.pictureBoxPlot_MouseEnter);
            this.pictureBoxPlot.MouseLeave += new System.EventHandler(this.pictureBoxPlot_MouseLeave);
            this.pictureBoxPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlot_MouseMove);
            this.pictureBoxPlot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlot_MouseUp);
            // 
            // panelLegend
            // 
            this.panelLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegend.Controls.Add(this.dataGridViewLegend);
            this.panelLegend.Controls.Add(this.labelLegend);
            this.panelLegend.Location = new System.Drawing.Point(167, 74);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(150, 100);
            this.panelLegend.TabIndex = 2;
            // 
            // dataGridViewLegend
            // 
            this.dataGridViewLegend.AllowUserToAddRows = false;
            this.dataGridViewLegend.AllowUserToDeleteRows = false;
            this.dataGridViewLegend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLegend.ColumnHeadersVisible = false;
            this.dataGridViewLegend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChartLegend,
            this.ChartColor});
            this.dataGridViewLegend.Location = new System.Drawing.Point(-1, 20);
            this.dataGridViewLegend.Name = "dataGridViewLegend";
            this.dataGridViewLegend.RowHeadersVisible = false;
            this.dataGridViewLegend.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewLegend.Size = new System.Drawing.Size(150, 80);
            this.dataGridViewLegend.TabIndex = 1;
            this.dataGridViewLegend.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLegend_CellClick);
            // 
            // labelLegend
            // 
            this.labelLegend.AutoSize = true;
            this.labelLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLegend.Location = new System.Drawing.Point(3, 4);
            this.labelLegend.Name = "labelLegend";
            this.labelLegend.Size = new System.Drawing.Size(57, 13);
            this.labelLegend.TabIndex = 0;
            this.labelLegend.Text = "Легенда";
            // 
            // buttonLegend
            // 
            this.buttonLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLegend.Location = new System.Drawing.Point(285, 0);
            this.buttonLegend.Name = "buttonLegend";
            this.buttonLegend.Size = new System.Drawing.Size(35, 20);
            this.buttonLegend.TabIndex = 7;
            this.buttonLegend.Text = "\\/";
            this.buttonLegend.UseVisualStyleBackColor = true;
            this.buttonLegend.Click += new System.EventHandler(this.buttonLegend_Click);
            // 
            // ChartLegend
            // 
            this.ChartLegend.HeaderText = "Legend";
            this.ChartLegend.Name = "ChartLegend";
            this.ChartLegend.ReadOnly = true;
            this.ChartLegend.Width = 128;
            // 
            // ChartColor
            // 
            this.ChartColor.HeaderText = "Color";
            this.ChartColor.Name = "ChartColor";
            this.ChartColor.ReadOnly = true;
            this.ChartColor.Width = 20;
            // 
            // Plot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.pictureBoxPlot);
            this.Controls.Add(this.panelInfo);
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "Plot";
            this.Size = new System.Drawing.Size(320, 200);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlot)).EndInit();
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLegend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.PictureBox pictureBoxPlot;
        private System.Windows.Forms.Label labelYunits;
        private System.Windows.Forms.Label labelXname;
        private System.Windows.Forms.Label labelYvalue;
        private System.Windows.Forms.Label labelXvalue;
        private System.Windows.Forms.Label labelYname;
        private System.Windows.Forms.Label labelXunits;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.DataGridView dataGridViewLegend;
        private System.Windows.Forms.Label labelLegend;
        private System.Windows.Forms.Button buttonLegend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChartLegend;
        private System.Windows.Forms.DataGridViewImageColumn ChartColor;
    }
}
