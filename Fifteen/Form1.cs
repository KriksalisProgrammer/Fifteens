using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Fifteen
{
    public partial class Form1 : Form
    {
        Point Empty;
        ArrayList image = new ArrayList();
        
        int Counters;
        public Form1()
        {
            Empty.X = 180;
            Empty.Y = 180;
            InitializeComponent();
        }

       

        private void AddImage(ArrayList images)
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7 }; 
            arr = Treatment(arr);
            foreach(Button b in panel1.Controls)
            {
                if(i<arr.Length)
                {
                    b.Image =(Image)image[arr[i]];
                    i++;
                }
            }
        }
        private int[] Treatment(int[]arr)
        {
            var rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray();
            return arr;
        }

        private void Crop(Image or, int x, int y)
        {
            Bitmap bitmap = new Bitmap(x, y);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(or, 0, 0, x,y);
            graphics.Dispose();
            int move = 0, movd = 0;
            for(int i=0;i<8;i++)
            {
                Bitmap p = new Bitmap(90, 90);
                for(int j=0;j<90;j++)
                {
                    for(int k=0;k<90;k++)
                    {
                        p.SetPixel(j, k, bitmap.GetPixel(j + move, k + movd));
                    }
                }
                image.Add(p);
                move += 90;
                if(move==270)
                {
                    move = 0;
                    movd += 90;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveButton((Button)sender);
           
        }

        private void MoveButton(Button btn)
        {
            if(((btn.Location.X==Empty.X-90||btn.Location.X==Empty.X+90)&& btn.Location.Y == Empty.Y)
                || (btn.Location.Y == Empty.Y - 90 || btn.Location.Y == Empty.Y + 90) && btn.Location.X == Empty.X)
            {
                Point swap = btn.Location;
                btn.Location = Empty;
                Empty = swap;
                Counters++;
                Сounter.Text = "Количество шагов:" + Counters;

            }
            if(Empty.X==180 && Empty.Y==180)
            {
                Check();
            }
            void Check()
            {
                int count = 0, index;
                foreach (Button btn1 in panel1.Controls)
                {
                    index = (btn1.Location.Y / 90) * 3 + btn1.Location.X / 90;
                    if (image[index] == btn.Image)
                    {
                        count++;
                    }
                    if (count == 8)
                    {
                        MessageBox.Show("You win!");
                    }
                }
            }


        }

        private void Start()
        {
            label1.Visible = false;
            panel1.Visible = true;
            pictureBox1.Visible = true;
            Сounter.Visible = true;
            Counters = 0;
            Сounter.Text = "Количество шагов:" + Counters;
            foreach (Button b in panel1.Controls)
            {
                b.Enabled = true;
                try
                {
                    Image orginal = Properties.Resources.Image;

                    Crop(orginal, 270, 270);

                    AddImage(image);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Exit();
                }
            }
            StartGame.Visible = false;
            Restart.Visible = true;


        }

        private void StartGame_Click_1(object sender, EventArgs e)
        {
            Start();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Counters = 0;
            Сounter.Text = "Количество шагов:" + Counters;
            foreach (Button b in panel1.Controls)
            {
                b.Enabled = true;

                Image orginal = Properties.Resources.Image;

                Crop(orginal, 270, 270);

                AddImage(image);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Restart.Visible = false;
            panel1.Visible = false;
            pictureBox1.Visible = false;
            Сounter.Visible = false;
        }
    }
}
