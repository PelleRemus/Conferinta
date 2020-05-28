using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AgentsNecessities
{
    public static class Engine
    {
        public static int n;
        public static int size = 15;
        public static Random rnd = new Random();
        public static List<Person> people = new List<Person>();
        public static List<Place> places = new List<Place>();
        public static float[,] matrix;

        public static Graphics grp;
        public static Bitmap bmp;
        public static Color[] colors;
        public static Form1 form;

        public static void Init(Form1 f)
        {
            form = f;
            ReadFromFile(@"..\..\TextFile1.txt");
            Populate();
            InitializeColours();
            
            bmp = new Bitmap(f.pictureBox1.Width, f.pictureBox1.Height);
            grp = Graphics.FromImage(bmp);
            grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            DrawStreets();
            DrawPlaces();
            f.pictureBox1.Image = bmp;
        }
        public static void ReadFromFile(string file)
        {
            TextReader dataLoad = new StreamReader(file);
            string buffer = dataLoad.ReadLine();
            n = int.Parse(buffer);

            for (int i = 0; i < n; i++)
            {
                buffer = dataLoad.ReadLine();
                string[] data = buffer.Split(' ');

                Point location = new Point(int.Parse(data[1]), int.Parse(data[2]));
                float gain = float.Parse(data[3]);
                float cost = float.Parse(data[4]);

                switch (data[0])
                {
                    case "W": places.Add(new Work(location, gain, cost)); break;
                    case "R": places.Add(new Restaurant(location, gain, cost)); break;
                    default: places.Add(new Home(location, gain, cost)); break;
                }
            }

            matrix = new float[n, n];
            while ((buffer = dataLoad.ReadLine()) != null)
            {
                string[] s = buffer.Split(' ');
                int i = int.Parse(s[0]), j = int.Parse(s[1]);
                matrix[i, j] = Distance(places[i].location, places[j].location);
                matrix[j, i] = Distance(places[j].location, places[i].location);
            }
        }
        public static void Populate()
        {
            people.Add(new Person((float)(rnd.NextDouble() / 3), (float)(rnd.NextDouble() / 2 + 0.3),
                (float)(rnd.NextDouble() / 2 + 0.3), rnd.Next(5, 8), places[1] as Home));
            people.Add(new Person((float)(rnd.NextDouble() / 3), (float)(rnd.NextDouble() / 2 + 0.3),
                (float)(rnd.NextDouble() / 2 + 0.3), rnd.Next(5, 8), places[2] as Home));
            people.Add(new Person((float)(rnd.NextDouble() / 3), (float)(rnd.NextDouble() / 2 + 0.3),
                (float)(rnd.NextDouble() / 2 + 0.3), rnd.Next(5, 8), places[4] as Home));
            people.Add(new Person((float)(rnd.NextDouble() / 3), (float)(rnd.NextDouble() / 2 + 0.3),
                (float)(rnd.NextDouble() / 2 + 0.3), rnd.Next(5, 8), places[8] as Home));
            people.Add(new Person((float)(rnd.NextDouble() / 3), (float)(rnd.NextDouble() / 2 + 0.3),
                (float)(rnd.NextDouble() / 2 + 0.3), rnd.Next(5, 8), places[9] as Home));
        }
        public static void InitializeColours()
        {
            colors = new Color[100];
            int r = 250, g = 0, b = 0;

            for (int i = 0; i < 50; i++)
                colors[i] = Color.FromArgb(r, g += 5, b);

            for (int i = 50; i < 100; i++)
                colors[i] = Color.FromArgb(r -= 5, g, b);
        }

        public static void DrawStreets()
        {
            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                    if (matrix[i, j] > 0)
                        grp.DrawLine(Pens.Black, places[i].location, places[j].location);
        }
        public static void DrawPlaces()
        {
            for (int i = 0; i < n; i++)
            {
                Point tempPoint = new Point(places[i].location.X - 7, places[i].location.Y - 7);
                grp.FillEllipse(new SolidBrush(Color.White), tempPoint.X, tempPoint.Y, 15, 15);
                grp.DrawEllipse(Pens.Black, tempPoint.X, tempPoint.Y, 15, 15);

                string s = "H";
                if (places[i] is Work)
                    s = "W";
                if (places[i] is Restaurant)
                    s = "R";
                grp.DrawString(s, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), tempPoint);
            }
        }

        public static void Tick()
        {
            foreach(Person person in people)
                person.Do();
            UpdateStats();
        }

        private static void UpdateStats()
        {
            form.listBox1.Items.Clear();
            for(int i=0; i<people.Count;i++)
            {
                form.listBox1.Items.Add("Person" + (i + 1));
                form.listBox1.Items.Add("money: " + people[i].money.ToString("0.000"));
                form.listBox1.Items.Add("food: " + people[i].food.ToString("0.000"));
                form.listBox1.Items.Add("relaxation: " + people[i].relaxation.ToString("0.000"));
                form.listBox1.Items.Add("");
            }
        }

        public static float[] Dijkstra(int start)
        {
            Queue<int> stack = new Queue<int>();
            bool[] visited = new bool[n];
            float[] distances = new float[n];
            for (int i = 0; i < n; i++)
                distances[i] = Int32.MaxValue;

            visited[start] = true;
            stack.Enqueue(start);
            distances[start] = 0;

            while (stack.Count != 0)
            {
                int t = stack.Dequeue();
                for (int i = 0; i < n; i++)
                    if (matrix[t, i] != 0)
                    {
                        if (!visited[i])
                        {
                            stack.Enqueue(i);
                            visited[i] = true;
                        }
                        if (distances[t] + matrix[t, i] < distances[i])
                            distances[i] = distances[t] + matrix[t, i];

                        if (distances[i] + matrix[t, i] < distances[t])
                            distances[t] = distances[i] + matrix[t, i];
                    }
            }
            return distances;
        }

        public static List<List<Place>> paths = new List<List<Place>>();
        public static void DFS(int start, int end, List<Place> list, bool[] visited)
        {
            visited[start] = true;
            list.Add(places[start]);
            if (start == end)
            {
                paths.Add(list);
                visited[end] = false;
                return;
            }
            for (int i = 0; i < n; i++)
                if (matrix[start, i] > 0 && !visited[i])
                {
                    List<Place> t = new List<Place>(list);
                    DFS(i, end, t, visited);
                    visited[i] = false;
                }
        }

        public static float Distance(Point p1, Point p2)
        {
            int x = (p2.X - p1.X) * (p2.X - p1.X);
            int y = (p2.Y - p1.Y) * (p2.Y - p1.Y);
            return (float)Math.Sqrt(x + y);
        }
    }
}
