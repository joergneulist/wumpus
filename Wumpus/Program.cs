namespace Wumpus
{
    class Program
    {
        static int Main(string[] args)
        {
            // TODO Magic Tunnels & HARD/EASY

            var client = new TextClient.Client(ROOMS_IN_CAVE, TUNNELS_PER_ROOM, BAT_COUNT, PIT_COUNT, ARROW_COUNT);

            return client.Play() ? 0 : 1;
        }

        private const ushort ROOMS_IN_CAVE = 20;
        private const ushort BAT_COUNT = 3;
        private const ushort PIT_COUNT = 3;
        private const ushort TUNNELS_PER_ROOM = 3;
        private const ushort ARROW_COUNT = 5;
    }
}
