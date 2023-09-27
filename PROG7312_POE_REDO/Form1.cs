using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG7312_POE_REDO
{
    public partial class Form1 : Form
    {
        
        private SoundPlayer play;//object to play the music
        private List<string> songNames = new List<string> { "Untitled.wav" ,"happy.wav","friday.wav"};//list of the music files name to play, which is in the debug folder
       
       

        public Form1()
        {
            InitializeComponent();
          
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region //method to play song when button pressed in homecontrol
        public int playSong(int holder)
        {
            play = new SoundPlayer(songNames[holder]);
            play.Play();

            return holder;

        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region //method to stop song when button pressed in homecontrol
        public void stopSong()
        {
            play = new SoundPlayer();
            play.Stop();
            
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region //method to play next song when button pressed in homecontrol, holder is the current song index
        public int nextSong(int holder)
        {
            if (holder == 3)
            {
                holder = 0;
            }

            play = new SoundPlayer(songNames[holder]);
                play.Play();
                holder = holder +1 ;
        
            return holder;
          
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------------------------------------------------Endoffile--------------------------------------------------------------------------------------------------------//