using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AgentsNecessities
{
    public class Person
    {
        public float money, food, relaxation, happiness;
        public int speed;
        public double left, top;
        public Home home;

        public Place currentLocation;
        public List<Place> destination = new List<Place>();
        public PictureBox pB;

        public Person(float money, float food, float relaxation, int speed, Home home)
        {
            this.money = money;
            this.food = food;
            this.relaxation = relaxation;
            this.speed = speed;
            left = 0; top = 0;
            this.home = home;
            currentLocation = home;

            pB = new PictureBox();
            pB.Parent = Engine.form.pictureBox1;
            pB.Size = new Size(Engine.size, Engine.size);
            pB.Location = new Point(home.location.X - 7, home.location.Y - 7);
        }

        public void Do()
        {
            //at every tick, the stats are lowering
            money -= 0.001f; food -= 0.001f; relaxation -= 0.001f;
            if (money < 0)
                money = 0;
            if (food < 0)
                food = 0;
            if (relaxation < 0)
                relaxation = 0;

            // the colour changes according to the person's happiness
            happiness = (money + food + relaxation) / 3;
            pB.BackColor = Engine.colors[(int)(happiness * 100)];

            //if the person reached its destination, change the stats accord to that destination
            //if that stat is filled up, the person selects a new destination
            if (destination.Count == 0)
            {
                if (currentLocation is Home)
                {
                    relaxation += currentLocation.gain;
                    food -= currentLocation.cost;

                    if (relaxation >= 1)
                    {
                        relaxation = 1;
                        GetNewDestination();
                    }
                    if (food < 0)
                        food = 0;
                }
                if (currentLocation is Work)
                {
                    money += currentLocation.gain;
                    relaxation -= currentLocation.cost;

                    if (money >= 1)
                    {
                        money = 1;
                        GetNewDestination();
                    }
                    if (relaxation < 0)
                        relaxation = 0;
                }
                if (currentLocation is Restaurant)
                {
                    food += currentLocation.gain;
                    money -= currentLocation.cost;

                    if (food >= 1)
                    {
                        food = 1;
                        GetNewDestination();
                    }
                    if (money < 0)
                        money = 0;
                }
            }
            else
            {
                double angle = Math.Atan2(destination[0].location.Y - currentLocation.location.Y,
                    destination[0].location.X - currentLocation.location.X);
                double x = left + speed * Math.Cos(angle);
                double y = top + speed * Math.Sin(angle);

                pB.Left += (int)x;
                pB.Top += (int)y;
                left = x - (int)x;
                top = y - (int)y;

                if (Engine.Distance(pB.Location, destination[0].location) < Engine.size)
                {
                    currentLocation = destination[0];
                    pB.Location = new Point(destination[0].location.X - 7, destination[0].location.Y - 7);
                    destination.RemoveAt(0);
                }
            }
        }

        public void GetNewDestination()
        {
            //find the indexes of all the places that would satisfy your biggest necesity
            List<int> indexes = new List<int>();
            if (money < food && money < relaxation)
                for (int i = 0; i < Engine.n; i++)
                    if (Engine.places[i] is Work)
                        indexes.Add(i);
            if (food < money && food < relaxation)
                for (int i = 0; i < Engine.n; i++)
                    if (Engine.places[i] is Restaurant)
                        indexes.Add(i);
            if (relaxation < food && relaxation < money)
                indexes.Add(Engine.places.FindIndex(x => x.Equals(home)));

            //to find the closest one to you, we use Dijkstra's algorithm and find the index of the shortest distance.
            int minIndex = 0;
            float min = Int32.MaxValue;
            if (indexes.Count > 1)
            {
                float[] distances = Engine.Dijkstra(Engine.places.FindIndex(x => x.Equals(currentLocation)));
                for (int i = 0; i < indexes.Count; i++)
                {
                    float efficiency = Engine.places[indexes[i]].gain / Engine.places[indexes[i]].cost;
                    if (distances[indexes[i]] * efficiency < min)
                    {
                        min = distances[indexes[i]] * efficiency;
                        minIndex = i;
                    }
                }
            }

            //we find all the possible routes from the current location to the destination location
            //and find the shortest one of them. That route is the current destination.
            
            Engine.DFS(Engine.places.FindIndex(x => x.Equals(currentLocation)), indexes[minIndex], new List<Place>(), new bool[Engine.n]);
            minIndex = 0;
            min = Int32.MaxValue;
            
            for (int i = 0; i < Engine.paths.Count; i++)
            {
                float distance = 0;
                for (int j = 1; j < Engine.paths[i].Count; j++)
                    distance += Engine.Distance(Engine.paths[i][j - 1].location, Engine.paths[i][j].location);
                if (distance <= min)
                {
                    min = distance;
                    minIndex = i;
                }
            }
            destination = Engine.paths[minIndex];
            Engine.paths.Clear();
        }
    }
}
