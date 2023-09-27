using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG7312_POE_REDO
{
    public partial class BookControl : UserControl
    {

        //get and set for the label in the user control
        private string code;      
        public string Code
        {
            get { return code; }
            set { code = value; codeDisplay.Text = value; }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public BookControl()
        {
            InitializeComponent();
        }

      //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
      //have a mouse down event for the entire user control, so when user clicks on it it takes all objects including the label
        private void BookControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control control = sender as Control;
                string controlName = control.Name;
                control.DoDragDrop(controlName, DragDropEffects.Move);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//---------------------------------------------------------------------------------------Endoffile----------------------------------------------------------------------------------------------------------------//