using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ViscousFriction
{
    public partial class Form : System.Windows.Forms.Form
    {
        public static CGraph[] gr = new CGraph[3]; // Переменные для рисования
        private List<MaterialPoint> Points = new List<MaterialPoint>(); //Переменные для материальных точек
        

        public Form()
        {
            InitializeComponent();

            //Отключение TabStop для ссылок
            linkSetB_1.TabStop  = false;
            linkSetB_2.TabStop  = false;
            linkSetB_3.TabStop  = false;
            linkSetB_4.TabStop  = false;
            linkSetB_5.TabStop  = false;
            linkSetB_6.TabStop  = false;
            linkSetB_7.TabStop  = false;
            linkSetB_8.TabStop  = false;
            linkSetA0_10.TabStop = false;
            linkSetA0_30.TabStop = false;
            linkSetA0_45.TabStop = false;
            linkSetA0_60.TabStop = false;
            linkSetA0_75.TabStop = false;
            linkSetA0_90.TabStop = false;

            //Инициализация графиков
            gr[0] = new CGraph(mainGr);
            gr[1] = new CGraph(topGr);
            gr[2] = new CGraph(botGr);            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (MaterialPoint p in Points)
                if (!Program.isPaused) p.Step();
        }

        #region Top buttons
        private void StartButton_Click(object sender, EventArgs e)
        {
            RebuildButton.Enabled = false;
            ClearButton.Enabled = false;
            StopButton.Enabled = true;

            MaterialPoint tMP = new MaterialPoint((double)mVal.Value, (double)v0Val.Value, (double)aVal.Value, (double)bVal.Value);
            double[] Y0 = { 0,0, (double)v0Val.Value * Math.Cos((double)aVal.Value * Math.PI / 180), (double)v0Val.Value * Math.Sin((double)aVal.Value * Math.PI / 180) };
            tMP.SetInit(0, Y0);
            Points.Add(tMP);
            
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Points.Clear();

            RebuildButton.Enabled = true;
            ClearButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            switch (Program.isPaused)
            {
                case true:
                    Program.isPaused = false;
                    PauseButton.Text = "Пауза";
                    break;
                case false:
                    Program.isPaused = true;
                    PauseButton.Text = "Плей";
                    break;
            }
        }
        #endregion

        #region Bottom buttons
        private void RebuildButton_Click(object sender, EventArgs e)
        {
            RebuildButton.Text = "Перестроить";
            StartButton.Enabled = true;
            PauseButton.Enabled = true;
            ClearButton.Enabled = true;

            gr[0].Setup(CGraph.graphCenter.BOTTOMLEFT, "X, m", (double)xMaxVal.Value, "Y, m", (double)yMaxVal.Value);
            gr[1].Setup(CGraph.graphCenter.MIDDLELEFT, "t, s", (double)tMaxVal.Value, "V, m/s",  (double)vMaxVal.Value);
            gr[2].Setup(CGraph.graphCenter.MIDDLELEFT, "t, s", (double)tMaxVal.Value, "a, m/s²", (double)aMaxVal.Value);

            gr[0].Clear(); gr[0].MakeGrid();
            gr[1].Clear(); gr[1].MakeGrid();
            gr[2].Clear(); gr[2].MakeGrid();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            gr[0].Clear(); gr[0].MakeGrid();
            gr[1].Clear(); gr[1].MakeGrid();
            gr[2].Clear(); gr[2].MakeGrid();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Links
        private void linkSetA0_10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aVal.Value = 10;
        }

        private void linkSetA0_30_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aVal.Value = 30;
        }

        private void linkSetA0_45_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aVal.Value = 45;
        }

        private void linkSetA0_60_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aVal.Value = 60;
        }

        private void linkSetA0_75_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aVal.Value = 75;
        }

        private void linkSetA0_90_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aVal.Value = 90;
        }

        private void linkSetB_1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 18.6M;
        }

        private void linkSetB_2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 20.7M;
        }

        private void linkSetB_3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 11.3M;
        }

        private void linkSetB_4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 15.0M;
        }

        private void linkSetB_5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 9.0M;
        }

        private void linkSetB_6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 17.9M;
        }

        private void linkSetB_7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 20.0M;
        }

        private void linkSetB_8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bVal.Value = 1010000M;
        }

        private void SetDefaults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        #endregion
    }
}
