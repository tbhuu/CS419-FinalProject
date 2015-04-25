namespace Homework6
{
    partial class CurveDrawer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.prCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.prCurve)).BeginInit();
            this.SuspendLayout();
            // 
            // prCurve
            // 
            chartArea1.AxisX.Interval = 0.2D;
            chartArea1.AxisX.Maximum = 1D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.Interval = 0.2D;
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.prCurve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.prCurve.Legends.Add(legend1);
            this.prCurve.Location = new System.Drawing.Point(-1, 0);
            this.prCurve.Name = "prCurve";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Precision-Recall";
            this.prCurve.Series.Add(series1);
            this.prCurve.Size = new System.Drawing.Size(438, 312);
            this.prCurve.TabIndex = 0;
            this.prCurve.Text = "chart1";
            // 
            // CurveDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prCurve);
            this.Name = "CurveDrawer";
            this.Size = new System.Drawing.Size(440, 315);
            ((System.ComponentModel.ISupportInitialize)(this.prCurve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart prCurve;
    }
}
