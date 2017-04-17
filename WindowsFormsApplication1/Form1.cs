using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Boolean timerstate = false;
        private int timeticker = 0;
        private int timetotal = 540;

        public Form1()
        {
            InitializeComponent();
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info; //Shows the info icon so the user doesn't thing there is an error.
            this.notifyIcon.BalloonTipText = "[Jiggling mouse]";
            this.notifyIcon.BalloonTipTitle = "[Yeah, jiggling mouse]";
            this.notifyIcon.Text = "[Yeah, jiggling mouse]";
            this.Resize += ImportStatusForm_Resize;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timerstate;
            timerstate = timer1.Enabled;
            if (timerstate)
            {
                lbltimer.Text = "active";
            }
            else
            {
                lbltimer.Text = "not active";
                timeticker = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeticker++;
            var timeleft = timetotal - timeticker;
            if (timeleft <= 0)
            {
                DoJiggle();
                timeticker = 0;
            }
            TimeSpan span = new TimeSpan(0,0,timeleft);
            labeltimeleft.Text = span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString();
        }

        private void DoJiggle()
        {
            Jiggler.Jiggle(0,1);
            Application.DoEvents();
            Jiggler.Jiggle(0, -1);
            Application.DoEvents();
        }

        private void ImportStatusForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000);
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }
    }
}
