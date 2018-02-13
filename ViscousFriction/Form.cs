using System;
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
            linkLabel2.TabStop  = false;
            linkLabel3.TabStop  = false;
            linkLabel4.TabStop  = false;
            linkLabel5.TabStop  = false;
            linkLabel6.TabStop  = false;
            linkLabel7.TabStop  = false;
            linkLabel8.TabStop  = false;
            linkLabel9.TabStop  = false;
            linkLabel10.TabStop = false;
            linkLabel11.TabStop = false;
            linkLabel12.TabStop = false;
            linkLabel13.TabStop = false;
            linkLabel14.TabStop = false;
            linkLabel15.TabStop = false;

            //Инициализация графиков
            gr[0] = new CGraph(mainGr);
            gr[1] = new CGraph(topGr);
            gr[2] = new CGraph(botGr);            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Points.Add(new MaterialPoint((double)mVal.Value, (double)v0Val.Value, (double)aVal.Value, (double)bVal.Value));
            
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            foreach (MaterialPoint p in Points)
            {
                p.StopAction();
            }
        }

        private void RebuildButton_Click(object sender, EventArgs e)
        {
            RebuildButton.Text = "Перестроить";
            StartButton.Enabled = true;
            StopButton.Enabled = true;
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
    }
}
