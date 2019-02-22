using System;
using System.Collections.Generic;
using System.Linq;

namespace Wumpus.Game
{
    public class RoomDescription
    {
        public int[] Neighbours { get; set; }
        public int Number { get; set; }
        public int Arrows { get; set; }
        public bool SenseBat { get; set; }
        public bool SensePit { get; set; }
        public bool SenseWumpus { get; set; }
    }

    public class Cave
    {
        private Math math = new Math();
        private int arrowCount;
        private int posOfWumpus;
        private int posOfPlayer;
        private OneBasedArray<Room> rooms;

        public Cave(int caveSize, int tunnels, int bats, int pits, int arrows)
        {
            InitBaseCave(caveSize, tunnels);
            InitRandomTunnels();
            SortTunnels();
            DistributeBats(bats);
            DistributePits(pits);
            PlaceWumpus();
            PlacePlayer(arrows);
        }

        public MoveResult MoveTo(int room)
        {
            var result = new MoveResult();

            if (CheckMoveRequest(result, room))
                EnterRoom(result);

            return result;
        }

        public ShootResult ShootAt(IEnumerable<int> roomList)
        {
            var result = new ShootResult();
            int roomsTravelled = 0;
            int posOfArrow = posOfPlayer;


            foreach (var room in roomList)
            {
                if (!CheckArrowRange(result, ++roomsTravelled))
                    break;

                posOfArrow = MoveArrow(result, rooms[posOfArrow], room);

                if (CheckArrowHit(result, posOfArrow))
                    break;
            }

            if (roomsTravelled == 0)
                result.Outcome = ActionOutcome.NoShotTarget;

            return result;
        }

        public RoomDescription DescribeCurrentRoom()
        {
            var room = rooms[posOfPlayer];

            return new RoomDescription
            {
                Number = room.Number,
                Arrows = arrowCount,
                Neighbours = (int[])room.Neighbours.Clone(),
                SenseBat = room.Neighbours.Any((number) => rooms[number].HasBat),
                SensePit = room.Neighbours.Any((number) => rooms[number].HasPit),
                SenseWumpus = IsWumpusNear(room)
            };
        }

        public override string ToString()
        {
            var description = rooms.Select((room) => ("<" + room.ToString() + ">"));
            description.Append(string.Format("[Wumpus: {0}]", posOfWumpus));
            description.Append(string.Format("[Player: {0}]", posOfPlayer));
            description.Append(string.Format("[Arrows: {0}]", arrowCount));

            return string.Join('\n', description);
        }

        private void InitBaseCave(int caveSize, int tunnels)
        {
            rooms = new OneBasedArray<Room>(caveSize);

            // Chain all rooms up like beads on a string. For obfuscation
            // we do not use the order 1-2-3-etc.
            // Therefore we need a step width. To reach all rooms, gcd of
            // the step width and the cave size needs to be 1.
            var stepWidth = RandomRoom((n) => math.GreatestCommonDivisor(n, rooms.Length) == 1);

            for (int i = 1; i <= rooms.Length; i++)
            {
                var room = new Room(i, tunnels);
                room.Neighbours[0] = ClampRoom(room.Number - stepWidth);
                room.Neighbours[1] = ClampRoom(room.Number + stepWidth);
                rooms[i] = room;
            }
        }

        private void InitRandomTunnels()
        {
            // Create random tunnels where there are none so far.
            foreach (var room in rooms)
            {
                int tunnel;
                while ((tunnel = room.FindFirstUnlinkedTunnel()) >= 0)
                    CreateRandomTunnel(room, tunnel);
            }
        }

        private void CreateRandomTunnel(Room room, int linkNumber)
        {
            // Create a random tunnel. Take care to not duplicate existing
            // tunnels. Make about half of the tunnels two-way, if possible.
            var targetRoom = RandomRoom((n) => !room.Neighbours.Contains(n));
            room.Neighbours[linkNumber] = targetRoom;

            // Link back in some cases & if possible
            if (!rooms[targetRoom].Neighbours.Contains(room.Number)
                && math.IsOneInNChance(2))
            {
                int backlink = rooms[targetRoom].FindFirstUnlinkedTunnel();
                if (backlink >= 0)
                    rooms[targetRoom].Neighbours[backlink] = room.Number;
            }
        }

        private void SortTunnels()
        {
            foreach (var room in rooms)
                Array.Sort(room.Neighbours);
        }

        private void DistributeBats(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var roomNumber = RandomRoom((n) => !rooms[n].HasBat);
                rooms[roomNumber].HasBat = true;
            }
        }

        private void DistributePits(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var roomNumber = RandomRoom((n) => !rooms[n].HasPit);
                rooms[roomNumber].HasPit = true;
            }
        }

        private void PlaceWumpus()
        {
            posOfWumpus = RandomRoom();
        }

        private void PlacePlayer(int arrows)
        {
            posOfPlayer = RandomRoom((n) => n != posOfWumpus);
            arrowCount = arrows;
        }

        private bool CheckMoveRequest(MoveResult result, int room)
        {
            if (room < 0)
                result.Outcome = ActionOutcome.RoomNumberTooLow;
            else if (room >= rooms.Length)
                result.Outcome = ActionOutcome.RoomNumberTooHigh;
            else if (!rooms[posOfPlayer].Neighbours.Contains(room))
            {
                if (DoesWumpusMove(6))
                    result.WumpusMovedCount++;

                if (posOfPlayer == posOfWumpus)
                    result.Outcome = ActionOutcome.DeathByWumpus;
                else
                    result.Outcome = ActionOutcome.NoTunnelAvailable;
            }
            else
                posOfPlayer = room;

            return result.IsOk();
        }

        private void EnterRoom(MoveResult result)
        {
            if (posOfPlayer == posOfWumpus)
                result.Outcome = ActionOutcome.DeathByWumpus;
            else if (rooms[posOfPlayer].HasPit)
            {
                if (math.IsOneInNChance(6))
                    result.Outcome = ActionOutcome.SurvivedPit;
                else
                    result.Outcome = ActionOutcome.DeathByPit;
            }
            else if (rooms[posOfPlayer].HasBat)
            {
                posOfPlayer = RandomRoom();
                result.MovedByBatsCount++;
                EnterRoom(result);
            }
        }

        private bool CheckArrowRange(ShootResult result, int roomsTravelled)
        {
            switch (roomsTravelled)
            {
                case 3:
                    if (math.IsOneInNChance(5))
                        result.Outcome = ActionOutcome.BowstringBroke;
                    break;

                case 4:
                    if (math.IsOneInNChance(2))
                        result.Outcome = ActionOutcome.TooManyShotTargets;
                    break;

                case 5:
                    result.Outcome = ActionOutcome.TooManyShotTargets;
                    break;
            }

            return result.IsOk();
        }

        private int MoveArrow(ShootResult result, Room room, int target)
        {
            if (room.Neighbours.Contains(target))
                return target;

            int link = math.Roll(room.Neighbours.Length);
            result.RandomRoomShot++;
            return room.Neighbours[link];
        }

        private bool CheckArrowHit(ShootResult result, int posOfArrow)
        {
            if (posOfArrow == posOfWumpus)
                result.Outcome = ActionOutcome.WumpusKilled;
            else if (posOfArrow == posOfPlayer)
                result.Outcome = ActionOutcome.DeathBySuicide;
            else if (--arrowCount == 0)
                result.Outcome = ActionOutcome.DeathByLackOfWeaponry;

            if (DoesWumpusMove(4) && (posOfPlayer == posOfWumpus))
                result.Outcome = ActionOutcome.DeathByWumpus;

            return !result.IsOk();
        }
    
        private bool DoesWumpusMove(int oneInNChance)
        {
            bool move = math.IsOneInNChance(oneInNChance);

            if (move)
                posOfWumpus = RandomRoom();

            return move;
        }

        private bool IsWumpusNear(Room room)
            // Check if Wumpus is any of the neighbouring rooms OR in the
            // rooms neighbouring those (i.e. two tunnels away)
            => room.Neighbours.Contains(posOfWumpus)
                || room.Neighbours.Any((number) 
                    => rooms[number].Neighbours.Contains(posOfWumpus));

        private int RandomRoom()
        {
            return math.Roll(rooms.Length) + 1;
        }

        private int RandomRoom(Func<int,bool> criterion)
        {
            int room;
            do
            {
                room = RandomRoom();
            }
            while (!criterion(room));

            return room;
        }

        private int ClampRoom(int rawRoom)
        {
            while (rawRoom <= 0)
                rawRoom += rooms.Length;

            while (rawRoom > rooms.Length)
                rawRoom -= rooms.Length;

            return rawRoom;
        }
    }
}
