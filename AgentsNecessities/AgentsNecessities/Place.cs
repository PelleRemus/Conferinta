using System.Drawing;

namespace AgentsNecessities
{
    public class Place
    {
        public string type;
        public Point location;
        public float gain, cost;

        public Place(string data)
        {
            string[] dataSplit = data.Split(' ');
            type = dataSplit[0];
            location = new Point(int.Parse(dataSplit[1]), int.Parse(dataSplit[2]));
            gain = float.Parse(dataSplit[3]);
            cost = float.Parse(dataSplit[4]);
        }
    }
}
