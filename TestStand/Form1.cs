using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreRobots;


namespace TestStand
{
    public partial class Form1 : Form
    {
        bool LefButton = false;
        const int SIZE_BUTTON = 30;
        List<List<Button>> field;
        LineTemplate asseblity;
        public Form1()
        {
            InitializeComponent();
            pField.MouseDown += PField_MouseDown;
            pField.MouseMove += PField_MouseMove;
            field = new List<List<Button>>();

            for (int i = 0; i < 6; i++)
            {
                field.Add(new List<Button>());
                for (int j = 0; j < 6; j++)
                {
                    Button b = new Button();
                    field[i].Add(b);
                    b.Parent = pField;
                    b.Location = new Point(i * SIZE_BUTTON, j * SIZE_BUTTON);
                    b.Size = new Size(SIZE_BUTTON, SIZE_BUTTON);
                    b.BackColor = Color.Red;
                    b.Enabled = false;
                }
            }
        }

        private void PField_MouseMove(object sender, MouseEventArgs e)
        {
            if (LefButton)
                 {
                     int x = ((e.Location.X - e.Location.X % SIZE_BUTTON) / SIZE_BUTTON);
                     int y = ((e.Location.Y - e.Location.Y % SIZE_BUTTON) / SIZE_BUTTON);
                    if(x >= 0 && x < field.Count && y >= 0 && y < field.Count)
                                                    field[x][y].BackColor = Color.Blue;
                 }
        }

        private void PField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                LefButton = true;
            else
                LefButton = false;
        }
        private void B_MouseEnter(object sender, EventArgs e)
        {
            
        }
        private void Test(object sender, EventArgs e)
        {
            
        }
    }
}
