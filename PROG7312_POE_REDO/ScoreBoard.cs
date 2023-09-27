using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PROG7312_POE_REDO
{
    public partial class ScoreBoard : UserControl
    {
        private UserControl currentControl;//object for the current control
        //-------------------------------------------------------------------------------------------------//
        //data passed between the controls to store user scores
        private List<string> scoredata = new List<string>();
        private List<string> gameName = new List<string>();
        private List<DateTime> timeList = new List<DateTime>();
        private List<string> timerList = new List<string>();
        //-------------------------------------------------------------------------------------------------//
        private int songHolder = 0;

        public ScoreBoard()
        {
            InitializeComponent();        
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region when this control is opened, this method is populated with the data 
        public void getArray(List<string> data, List<string> name, List<DateTime> time, List<string> timer, int songholder)
        {
            songHolder = songholder;
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
            fillList();
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region populates the list view with score data, and customizes each objects according to the score
        public void fillList()
        {
          
           for (int i = 0; i < scoredata.Count; i++)
            {
                if (scoredata[i].ToLower().Equals("gold"))
                {
                    string inp = "Game: " + gameName[i].ToString()   + "\nSeconds: " + timerList[i].ToString();
                    ListViewItem item = new ListViewItem(inp); 
                    item.ForeColor = Color.Gold;
                    listView1.Items.Add(item);
        
                }
                else if (scoredata[i].ToLower().Equals("silver"))
                {
                    string inp = "Game: " + gameName[i].ToString()  + "\nSeconds: " + timerList[i].ToString();
                    ListViewItem item = new ListViewItem(inp);
                    item.ForeColor = Color.White;                   
                    listView1.Items.Add(item);               
                }
                else if (scoredata[i].ToLower().Equals("bronze"))
                {
                    string inp = "Game: " + gameName[i].ToString() + "\nSeconds: " + timerList[i].ToString();
                    ListViewItem item = new ListViewItem(inp);
                    item.ForeColor = Color.Brown;
                    listView1.Items.Add(item);           
                }
            }
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region button to go back to home control
        private void btnClose_Click(object sender, EventArgs e)
        {
            HomeControl hc = new HomeControl();
            hc.getArray(scoredata,gameName,timeList,timerList,songHolder);
            this.Controls.Clear();
            SwitchUserControl(hc);
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
           
            this.Controls.Add(newControl);
            newControl.Dock = DockStyle.Fill;  // Dock to fill the parent
            currentControl = newControl;  
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
       
    }
}
//--------------------------------------------------------------------------------enfofFile-------------------------------------------------------------------------------------------//
