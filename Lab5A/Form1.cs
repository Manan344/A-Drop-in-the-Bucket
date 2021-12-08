using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//I, Manan Patel, 000735153 certify that this material is my original work. No other person's work has been used without due acknowledgement.
//File Date - 05/12/2018

namespace Lab5A
{
    public partial class Form1 : Form
    {
        private Color clr = Color.White;    //White color is stored by default
        private int height;                 //to keep track of height
        private bool fill;                  //to check if bucket is filled or not 
        int[] flowRate = {1,180,160,140,120,100,80,60,40,20,10};    //to incerease or decrease speed of water form faucet 

        /// <summary>
        /// Main Form Class
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);       //Registers the Paint event handler
            trackBar1.Value = 0;
        }

        /// <summary>
        /// This method will draw lines for the bucket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.White), 100, 290, 100, 400);
            e.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.White), 100, 400, 300, 400);
            e.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.White), 300, 400, 300, 290);
        }

        /// <summary>
        /// Clicking on Color button opens the Color Dialog box so that user can 
        /// choose a new color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorButton_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)        //Display the actual dialogbox
            {
                clr = colorDialog1.Color;                           //save the color choice the user made
            }
        }

        /// <summary>
        /// User can adjust speed of water that is poured in bucket
        /// by this trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();              //creates a graphics object
            if (fill)                                 //if bucket is filled
            {
                g.FillRectangle(new SolidBrush(BackColor), 101, 200, 199, 200);    //this will graphically display a liquid filling a bucket 
                fill = false;
            }

            else
            {
                if(trackBar1.Value > 0)                //if value of trackbar is greater than zero 
                {
                    timer1.Interval = 300;
                    timer1.Interval = flowRate[trackBar1.Value];
                }

                else if(trackBar1.Value == 0)           //faucet will stop if trackbar value is 0
                {
                    timer1.Stop();                      //timer will stop
                    g.FillRectangle(new SolidBrush(BackColor), 110, 200, 15, 200 - height + 1);    //this will graphically display a rectangle
                }
            }

            timer1.Start();
        }

        /// <summary>
        /// User can fill the bucket wuth this timer after the bucket is full
        /// water flow will stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();               //creates a graphics object
            graphics.FillRectangle(new SolidBrush(clr), 101, 400 - height, 199, 1);        //this will draw water falling form faucet into bucket
            graphics.FillRectangle(new SolidBrush(clr), 110, 200, 15, 200 - height + 1);
            height += 1;

            if((395 - height) == 290)          //if height of bucket is reached
            {
                fill = true;
                height = 0;                    //counter set to 0 becase faucet was stopped
                timer1.Stop();                  //timer will stop
                trackBar1.Value = 0;            //setting value of trackbar to 0
                graphics.FillRectangle(new SolidBrush(BackColor), 110, 200, 15, 96);
            }
        }

        /// <summary>
        /// This will exit the window when user clicks on Close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();                       //exit the window
        }
    }
}

