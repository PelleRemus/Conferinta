using System.Drawing;

namespace AgentsNecessities
{
    public class Place
    {
        public Point location;
        public float gain, cost;
        public Place(Point location, float gain, float cost)
        {
            this.location = location;
            this.gain = gain;
            this.cost = cost;
        }
    }

    public class Home: Place
    {
        public Home(Point location, float gain, float cost) : base(location, gain, cost)
        { }
    }
    public class Work : Place
    {
        public Work(Point location, float gain, float cost) : base(location, gain, cost)
        { }
    }
    public class Restaurant : Place
    {
        public Restaurant(Point location, float gain, float cost) : base(location, gain, cost)
        { }
    }
}
