using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace PROG7312_POE_REDO
{
    public partial class ReplaceControl : UserControl
    {
        
        private List<string> callNumbers = new List<string>();//generate call numbers list
        private List<string> sortedNumbers = new List<string>();//store ordered list
        //-------------------------------------------------------------------------------------------------//
        //data passed between the controls to store user scores
        private List<string> scorea = new List<string>();
        private List<string> gameName = new List<string>();
        private List<DateTime> timeList = new List<DateTime>();
        private List<string> timerList = new List<string>();
        //-------------------------------------------------------------------------------------------------//
        private static int arrayCount = 10; // amount of book and call numbers to create
        private BookControl[] listItems = new BookControl[arrayCount];//book control object to display call numbers
        private TimeSpan elapsedTime = TimeSpan.Zero; //timer seconds holder
        private int songHolder = 0;

        private UserControl currentControl;
        private FlowLayoutPanel _draggedItem;

      
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public ReplaceControl()
        {
            InitializeComponent();
            GenerateCallNumbers();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 10;
            elapsedTime = TimeSpan.Zero;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region method to populate data passed between controls
        public void getArray(List<string> data, List<string> name, List<DateTime> time, List<string> timer, int songholder)
        {
            songHolder = songholder;
            foreach (string i in data)
            {
                scorea.Add(i);

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

        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region populate the random numbers and book object into the first panel
        private void populateItem()
        {
            for (int i = 0; i < listItems.Length; i++)
            {
                Random random = new Random();
                listItems[i] = new BookControl();
                listItems[i].Name = callNumbers[i].ToString();
                listItems[i].Code = callNumbers[i].ToString();

                //random colors, between 200,256 tro keep colors light
                int red = random.Next(200, 256);     
                int green = random.Next(200, 256);   
                int blue = random.Next(200, 256);    

                Color randomColor = Color.FromArgb(red, green, blue);

                listItems[i].BackColor = randomColor;

                //adjust size of the book object to add to flow panel
                int controlWidth = 200;  
                int controlHeight = 220;  
                listItems[i].Size = new Size(controlWidth, controlHeight);
                flowLayoutPanel1.Controls.Add(listItems[i]);

            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        
        #region method to generate random letters
        private string GenerateRandomLetters(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region generate the number and letter string
        private void GenerateCallNumbers()
        {
            Random random = new Random();

            // Generate call numbers in the format "XXX.YY"
            for (int i = 0; i < arrayCount; i++)
            {
                int numberPart = random.Next(0, 1000);  // Random number between 0 and 999
                string letterPart = GenerateRandomLetters(random, 3);  // Random three-letter combination
                string callNumber = $"{numberPart}.{letterPart}";
                callNumbers.Add(callNumber);
            }         
            populateItem();
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region gets the object under the mouse when the user clicks and holds
        private FlowLayoutPanel GetFlowLayoutPanelUnderMouse(FlowLayoutPanel flowLayoutPanel, Point mousePosition)
        {
            Control control = flowLayoutPanel.GetChildAtPoint(mousePosition);

            while (control != null)
            {
                if (control is FlowLayoutPanel panel)
                {
                    return panel;
                }

                control = control.Parent;
            }

            return null;
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region drag drop event for panel 1
        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            var name = e.Data.GetData(typeof(string)) as string;
            var control = this.Controls.Find(name, true).FirstOrDefault();
            int arr = flowLayoutPanel2.Controls.Count;

            if (flowLayoutPanel2.Controls.Count == 0)
            {
                MessageBox.Show("Wrong place to drag and drop");
            }
            else
            {
                progressBar1.Value = arr - 1;
                if (control != null)
                {
                    flowLayoutPanel2.Controls.Remove(control);  // Remove from panel 1
                    flowLayoutPanel1.Controls.Add(control);  // Add to panel 2
                }
            }
            
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region enable objects to be entered in panel 1
        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            var name = e.Data.GetData(typeof(string)) as string;
            var control = this.Controls.Find(name, true).FirstOrDefault();

            if (control != null)
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region enable objects to be entered in panel 2
        private void flowLayoutPanel2_DragEnter(object sender, DragEventArgs e)
        {
            var name = e.Data.GetData(typeof(string)) as string;
            var control = this.Controls.Find(name, true).FirstOrDefault();
            if (control != null)
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region mouse move event panel 2
        private void flowLayoutPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _draggedItem == null)
            {
                _draggedItem = GetFlowLayoutPanelUnderMouse(flowLayoutPanel2, e.Location);

                if (_draggedItem != null)
                {
                    string controlName = _draggedItem.Controls[0].Name; 
                    DoDragDrop(controlName, DragDropEffects.Move);
                    _draggedItem = null;
                }
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region mouse move event panel 1
        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _draggedItem == null)
            {
                _draggedItem = GetFlowLayoutPanelUnderMouse(flowLayoutPanel1, e.Location);

                if (_draggedItem != null)
                {
                    string controlName = _draggedItem.Controls[0].Name;  // Get the control's name
                    DoDragDrop(controlName, DragDropEffects.Move);
                    _draggedItem = null;
                }
            }
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region method that sorts, verifies the sorted list by user
        public void worker()
        {
          
            //sorts call numbers
            callNumbers.Sort((a, b) => {
                string[] partsA = a.Split('.');
                string[] partsB = b.Split('.');

                // Compare numerical parts
                int numComparison = int.Parse(partsA[0]).CompareTo(int.Parse(partsB[0]));

                if (numComparison != 0)
                {
                    // If numerical parts are different, return the numerical comparison result
                    return numComparison;
                }
                else
                {
                    // If numerical parts are equal, sort alphabetically on the remaining parts
                    return string.Compare(partsA[1], partsB[1]);
                }
            });

            //get the string in each control from the sorted list by user
            foreach (Control control in flowLayoutPanel2.Controls)
             {
                  BookControl bookControl = control as BookControl;
                  string parts = bookControl.Code.ToString();
                  sortedNumbers.Add(parts);
                
             }

            bool f = false;
            for (int count = 0; count < callNumbers.Count; count++)
            {
                if (callNumbers[count].ToLower().ToString() == sortedNumbers[count].ToLower().ToString())
                {

                    f = true;
                }

                else
                {
                    f = false;

                }
            }
           

            if (f == true)
            {
                timer1.Stop();
                

                if (elapsedTime.Seconds <= 20)
                {
                    scorea.Add("Gold");
                }
                else if (elapsedTime.Seconds <= 25)
                {
                    scorea.Add("Silver");
                }
                else
                {
                    scorea.Add("Bronze");
                }

                gameName.Add("Replacing Books");
                DateTime currenttime = DateTime.Now;
                timeList.Add(currenttime);
                timerList.Add(elapsedTime.Seconds.ToString());

                HomeControl hm = new HomeControl();
                hm.getArray(scorea, gameName, timeList, timerList,songHolder);
                this.Controls.Clear();
                SwitchUserControl(hm);

            }
            else
            {
                string data = "";

                foreach(string item in callNumbers)
                {
                    data = data + item + "\n";
                }

                MessageBox.Show("Numbers were not in ascending order" + $"\n{data}");
                HomeControl hm = new HomeControl();
                hm.getArray(scorea, gameName, timeList, timerList,songHolder);
                this.Controls.Clear();
                SwitchUserControl(hm);
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region drag drop event panel 2
        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            var name = e.Data.GetData(typeof(string)) as string;
            var control = this.Controls.Find(name, true).FirstOrDefault();
            int arr = flowLayoutPanel2.Controls.Count;
            progressBar1.Value = arr + 1;
            if (control != null)
            {
                timer1.Start();
                flowLayoutPanel1.Controls.Remove(control);  // Remove from panel 1
                flowLayoutPanel2.Controls.Add(control);  // Add to panel 2

            }

            //using progress bar to determine if user dragged all the books
            if (progressBar1.Value == 10)
            {
                worker();
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region method to switch current user control to home control
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
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region back button to take user back to home page
        private void btnBack_Click(object sender, EventArgs e)
        {
            HomeControl hm = new HomeControl();
            hm.getArray(scorea, gameName, timeList,timerList, songHolder);
            this.Controls.Clear();
            SwitchUserControl(hm);
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region
        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedTime += TimeSpan.FromMilliseconds(timer1.Interval);
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------------------------------------------EndofFile-------------------------------------------------------------------------------------------------//