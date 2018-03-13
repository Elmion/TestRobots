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
        public static readonly Random rnd = new Random();
       
        bool LefButton = false;
        const int SIZE_BUTTON = 40;
        List<List<Button>> field;
        List<Button> Tail;
        LineTemplate assebler;
        Button Prev = null;
        Button Current = null;
        Graphics g;
        Panel LinePanel;
        public Form1()
        {
            InitializeComponent();
            InitTemplates();
            g = pField.CreateGraphics();
            pField.MouseDown += PField_MouseDown;
            pField.MouseUp += PField_MouseUp;
            pField.MouseMove += PField_MouseMove;
            field = new List<List<Button>>();
            Tail = new List<Button>();
            for (int i = 0; i < 6; i++)
            {
                field.Add(new List<Button>());
                for (int j = 0; j < 6; j++)
                {
                    field[i].Add(CreateButton(i,j,rnd.Next(3)));
                }
            }
        }
        private void InitTemplates()
        {
            assebler = new LineTemplate();
            assebler.AddTemplate("ore/ore");
            assebler.AddTemplate("ore/cuprum");
            assebler.AddTemplate("ore/cuprum/ore/cuprum/oil");
            assebler.AddTemplate("ore/cuprum/ore/tail");
        }
        private void PField_MouseMove(object sender, MouseEventArgs e)
        {
            if (LefButton )
                 {
                     int x = ((e.Location.X - e.Location.X % SIZE_BUTTON) / SIZE_BUTTON);
                     int y = ((e.Location.Y - e.Location.Y % SIZE_BUTTON) / SIZE_BUTTON);

                      
                        if (x >= 0 && x < field.Count && y >= 0 && y < field.Count)
                        {
                            if (Tail.Last() != field[x][y])
                            {
                                if (Tail.Count > 1 && field[x][y] == Tail[Tail.Count - 2])
                                {
                                    Tail.Last().BackColor = Color.Gray;
                                    Tail.Remove(Tail.Last());
                                    assebler.Preverios();
                                }
                                else
                                {
                                    field[x][y].BackColor = Color.Red;
                                    Tail.Add(field[x][y]);
                                    switch ((int)field[x][y].Tag)
                                    {
                                        case 0: assebler.Next("ore"); break;
                                        case 1: assebler.Next("cupper"); ; break;
                                        case 2: assebler.Next("oil"); break;
                                    }
                           
                                }
                            }
                            PrintTail();
                        }
                 }
        }
        private void PField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LefButton = true;
                int x = ((e.Location.X - e.Location.X % SIZE_BUTTON) / SIZE_BUTTON);
                int y = ((e.Location.Y - e.Location.Y % SIZE_BUTTON) / SIZE_BUTTON);
                Current = field[x][y];
                field[x][y].BackColor = Color.Red;
                Tail.Add(field[x][y]);
                switch ((int)field[x][y].Tag)
                {
                    case 0: assebler.Next("ore"); break;
                    case 1: assebler.Next("cupper"); ; break;
                    case 2: assebler.Next("oil"); break;
                }
            }
        }
        private void PField_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LefButton = false;
                assebler.EndLine();
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        field[i][j].BackColor = Color.Gray;
                    }
                }
                Console.Clear();
                Tail.Clear();
            }
        }
        private void B_MouseEnter(object sender, EventArgs e)
        {
            
        }
        private void Test(object sender, EventArgs e)
        {
            
        }
        void InputOUT(string s)
        {
            assebler.Next(s);
            Console.Clear();
            Console.WriteLine(assebler.Cicles);
        }
        void PrintTail()
        {
            Console.Clear();
            foreach (var item in Tail)
            {
                switch ((int)item.Tag)
                    {
                        case 0: Console.Write("Ore/"); break;
                        case 1: Console.Write("Cupper/"); break;
                        case 2: Console.Write("Oil/"); break;
                    }
            }
            Console.WriteLine();
            Console.WriteLine(assebler.Cicles);
        }



    Button CreateButton(int x,int y,int TypeButton)
        {
            Button b = new Button();

            b.Parent = pField;
            b.Location = new Point(x * SIZE_BUTTON, y* SIZE_BUTTON);
            b.Size = new Size(SIZE_BUTTON, SIZE_BUTTON);
            b.BackColor = Color.Gray;
            b.Enabled = false;
            b.Tag = TypeButton;
            Panel p = new Panel();
            p.Parent = b;
            p.Size = new Size(b.Size.Height - 12, b.Size.Width - 12);
            p.BackgroundImageLayout = ImageLayout.Stretch;
            p.Location = new Point(6, 6);


            if (TypeButton == 0) p.BackgroundImage = Properties.Resources._1;
            if (TypeButton == 1) p.BackgroundImage = Properties.Resources._2;
            if (TypeButton == 2) p.BackgroundImage = Properties.Resources._3;

            return b;
        }
    }
}
