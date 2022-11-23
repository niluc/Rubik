using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Rubik
{
    public partial class Time : Form
    {

        public Time()
        {
            InitializeComponent();
            step = stepDo(0);
        }

        private int step = 0;
        private Stopwatch stopWatch;
        private Timer countDown;
        private TimeSpan timeSpan = new TimeSpan(15);
        private TimeSpan aTick = new TimeSpan(0, 0, 0, 0, 10);
        private TimeSpan zeroSecond = new TimeSpan(0);

        private string makeScramble(int length)
        {
            string[] options = { "F", "F2", "F'", "R", "R2", "R'", "U", "U2", "U'", "B", "B2", "B'", "L", "L2", "L'", "D", "D2", "D'" };
            int[] numOptions = { 0, 1, 2, 3, 4, 5 };
            int[] scramble = new int[length];
            string scrambleMoves = "";
            int randomInt;
            TimeSpan timeSpan = new TimeSpan(15);
            Random r = new Random();

            scramble[0] = r.Next(0, 6);
            for (int i = 1; i < length; i++)
            {
                randomInt = r.Next(0, 6);
                while (randomInt == scramble[i - 1])
                {
                    randomInt = r.Next(0, 6);
                }
                scramble[i] = randomInt;
            }
            for (var i = 0; i < length; i++)
            {
                switch (scramble[i])
                {
                    case 0:
                        scrambleMoves += options[r.Next(1, 3)] + " ";
                        break;
                    case 1:
                        scrambleMoves += options[r.Next(3, 6)] + " ";
                        break;
                    case 2:
                        scrambleMoves += options[r.Next(6, 9)] + " ";
                        break;
                    case 3:
                        scrambleMoves += options[r.Next(9, 12)] + " ";
                        break;
                    case 4:
                        scrambleMoves += options[r.Next(12, 15)] + " ";
                        break;
                    case 5:
                        scrambleMoves += options[r.Next(15, 18)] + " ";
                        break;
                }
            }
            return scrambleMoves;
        }




        private void Time_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (step != 3)
                {
                    step = stepDo(step);
                }
            }
        }

        /// <summary>
        /// make steps
        /// </summary>
        private int stepDo(int step)
        {
            switch (step)
            {
                case 0:
                    scrambleMovesLabel.Text = makeScramble(20);
                    timer.Text = "";
                    message.Text = "PRESS SPACE TO START INSPECT";
                    return 1;
                case 1:
                    scrambleMovesLabel.Text = "INSPECTION TIME:";
                    message.Text = "HOLD SPACE WHEN READY";
                    countDown = new Timer();
                    stopWatch = new Stopwatch();
                    stopWatch.Start();
                    timeSpan = new TimeSpan(0, 0, 15);
                    countDown.Interval = 10;
                    countDown.Start();
                    countDown.Tick += countDown_TickDown;
                    return 2;
                case 2:
                    message.Text = "RELEASE SPACE TO START";
                    return 3;
                case 3:
                    scrambleMovesLabel.Text = "";
                    message.Text = "PRESS SPACE TO STOP";
                    countDown.Stop();
                    countDown = new Timer();
                    stopWatch = new Stopwatch();
                    stopWatch.Start();
                    timeSpan = new TimeSpan(0, 0, 0);
                    countDown.Interval = 10;
                    countDown.Start();
                    countDown.Tick += countDown_TickUp;
                    return 4;
                case 4:
                    countDown.Stop();
                    message.Text = "PRESS SPACE TO SCRAMBLE";
                    return 0;
            }
            return -1;
        }

        private void countDown_TickDown(object sender, EventArgs e)
        {
            timer.Text = (timeSpan - stopWatch.Elapsed).Seconds.ToString() + '.' + ((timeSpan - stopWatch.Elapsed).Milliseconds / 10).ToString();
            if ((timeSpan - stopWatch.Elapsed) <= zeroSecond)
            {
                step = stepDo(3);
            }
        }

        private void countDown_TickUp(object sender, EventArgs e)
        {
            timer.Text = stopWatch.Elapsed.Seconds.ToString() + '.' + (stopWatch.Elapsed.Milliseconds / 10).ToString();
        }
        private void Time_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (step == 3)
                {
                    step = stepDo(step);
                }
            }
        }

    }
}
