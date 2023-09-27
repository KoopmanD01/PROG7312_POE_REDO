using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace PROG7312_POE_REDO
{
    public partial class HomeControl : UserControl
    {
        private UserControl currentControl;//object for the current control
        //-------------------------------------------------------------------------------------------------//
        //data passed between the controls to store user scores
        private List<string> scoredata = new List<string>();
        private List<string> gameName = new List<string>();
        private List<DateTime> timeList = new List<DateTime>();
        private List<string> timerList = new List<string>();
        //-------------------------------------------------------------------------------------------------//
        private int songHolder;//current song index
        private List<string> songPhotos = new List<string> {"choosesong2.jpeg", "jazzbkg.jpeg","happbkg.jpeg","fridaybkg.jpeg" };//phots file name to display in music picture box, in debug folder



        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public HomeControl()
        {
            InitializeComponent();
            pictureBox2.Image = Image.FromFile(songPhotos[0]);
        }

      
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region when this control is opened, this method is populated with the data 
        public void getArray(List<string> data, List<string> name, List<DateTime> time, List<string> timer, int songholder)
        {

            //picture box of music
            songHolder = songholder;
           
            pictureBox2.Image = Image.FromFile(songPhotos[songholder]);
            //populate lists
            foreach (string i in data)
            {
                scoredata.Add(i);
                
            }
            foreach (string i in name)
            {
                gameName.Add(i);

            }
            foreach (var i in time)
            {
                timeList.Add(i);

            }
            foreach (var i in timer)
            {
                timerList.Add(i);

            }

            //method to display the lowwest time score of the user
            if (scoredata.Count > 0)
            {
                textBoxHighScore.Visible = true;
               
                    int smallestNum = int.Parse(timerList[0]);  
                    int index = 0;

                    for (int count = 1; count < timerList.Count; count++)
                    {
                        int currentNum = int.Parse(timerList[count]);

                        if (currentNum < smallestNum)
                        {
                            smallestNum = currentNum;
                            index = count;
                        }
                    }
               
                    if (scoredata[index].ToLower().Equals("gold"))
                    {
                        textBoxHighScore.Text = "Game: " + gameName[index].ToString() + "\nDate: " + timeList[index].ToString() + "\nTimer (seconds): " + timerList[index].ToString() + "\nGold" + "\n";
                        textBoxHighScore.ForeColor = Color.Gold;
                    }
                    if (scoredata[index].ToLower().Equals("silver"))
                    {
                        textBoxHighScore.Text = "Game: " + gameName[index].ToString() + "\nDate: " + timeList[index].ToString() + "\nTimer (seconds): " + timerList[index].ToString() + "\nSilver" + "\n";
                        textBoxHighScore.ForeColor = Color.White;
                    }
            }

        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region button click event for replace books activity
        private void btnReplace_Click(object sender, EventArgs e)
        {
            ReplaceControl rc = new ReplaceControl();
            rc.getArray(scoredata,gameName,timeList,timerList,songHolder);
            this.Controls.Clear();
            SwitchUserControl(rc);          
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region
        private void btnIdentify_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Upgrade to platinum", "Not Available");
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region
        private void btnFind_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Upgrade to platinum", "Not Available");
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region
        private void btnScoreBoard_Click(object sender, EventArgs e)
        {
            ScoreBoard sb = new ScoreBoard();
            sb.getArray(scoredata,gameName,timeList,timerList,songHolder);
            this.Controls.Clear();
            SwitchUserControl(sb);               
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region method to switch between the control, and fill form 1 with home controls
        private void SwitchUserControl(UserControl newControl)
        {
            if (currentControl != null)
            {
                // Hide the current control
                currentControl.Hide();
            }

            // Add the new control to the form
            this.Controls.Add(newControl);
            newControl.Dock = DockStyle.Fill;  // Dock to fill the parent
            currentControl = newControl;  
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region music play button
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (songHolder == 3)
            {
                songHolder = 0;
            }
            pictureBox2.Image = Image.FromFile(songPhotos[songHolder+1]);
            Form1 f1 = new Form1();
            int s = f1.playSong(songHolder);
            songHolder = s;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region music next button
        private void btnNext_Click(object sender, EventArgs e)
        {          
            if (songHolder == 3)
            {
                songHolder = 0;
            }
            pictureBox2.Image = Image.FromFile(songPhotos[songHolder+1]);
            Form1 f1 = new Form1();
            int s =  f1.nextSong(songHolder);         
            songHolder = s;         
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region music button stop
        private void btnStop_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.stopSong();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//-----------------------------------------------------------------------------------------------EndofFile-------------------------------------------------------------------------------------------------------------//
