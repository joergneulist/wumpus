using System;
using System.Collections.Generic;
using System.Linq;
using Wumpus.Game;

namespace Wumpus.TextClient
{
    class Client
    {
        private Cave cave;

        public Client(int rooms, int tunnels, int bats, int pits, int arrows)
        {
            cave = new Cave(rooms, tunnels, bats, pits, arrows);
            Console.Error.WriteLine(cave.ToString());

            Console.WriteLine(Strings.GREETING,
                rooms, tunnels,
                bats, Strings.Plural(bats),
                pits, Strings.Plural(pits),
                arrows, Strings.Plural(arrows));
        }

        public bool Play()
        {
            var result = GameResult.Ongoing;

            while (result == GameResult.Ongoing)
            {
                DescribeRoom();
                result = ExecuteAction(GetUserAction());
            } 

            return result != GameResult.Defeat;
        }

        private void DescribeRoom()
        {
            var room = cave.DescribeCurrentRoom();

            Console.WriteLine(Strings.DESCRIBE_BASIC_SITUATION,
                room.Number, room.Arrows, Strings.Plural(room.Arrows));
            if (room.SenseBat)
                Console.WriteLine(Strings.DESCRIBE_BAT_SENSATION);
            if (room.SensePit)
                Console.WriteLine(Strings.DESCRIBE_PIT_SENSATION);
            if (room.SenseWumpus)
                Console.WriteLine(Strings.DESCRIBE_WUMPUS_SENSATION);

            Console.Write(Strings.DESCRIBE_NEIGHBOURS_START);
            foreach (var number in room.Neighbours.Take(room.Neighbours.Length - 1))
                Console.Write(Strings.DESCRIBE_NEIGHBOURS_TUNNEL_ANY, number);
            Console.WriteLine(Strings.DESCRIBE_NEIGHBOURS_TUNNEL_LAST, room.Neighbours.Last());
        }

        private UserAction GetUserAction()
        {
            Console.Write(Strings.REQUEST_ACTION);
            var action = new UserAction();

            while (action.Action == ActionType.None)
                action.ParseFrom(Console.ReadLine());

            return action;
        }

        private GameResult ExecuteAction(UserAction action)
        {
            var result = GameResult.Ongoing;

            switch (action.Action)
            {
                case ActionType.Move:
                    result = ExecuteMove(action.Parameters.First());
                    break;

                case ActionType.Shoot:
                    result = ExecuteShot(action.Parameters);
                    break;

                default:
                    result = GameResult.Abort;
                    break;
            }

            return result;
        }

        private GameResult ExecuteMove(int room)
        {
            var actionResult = cave.MoveTo(room);

            for (int i = 0; i < actionResult.WumpusMovedCount; i++)
                Console.WriteLine(Strings.WUMPUS_MOVED);
            
            for (int i = 0; i < actionResult.MovedByBatsCount; i++)
                if (i == 0)
                    Console.WriteLine(Strings.MOVED_BY_BATS1);
                else
                    Console.WriteLine(Strings.MOVED_BY_BATS2);

            switch (actionResult.Outcome)
            {
                case ActionOutcome.DeathByPit:
                    Console.WriteLine(Strings.DEATH_BY_PIT);
                    break;

                case ActionOutcome.DeathByWumpus:
                    Console.WriteLine(Strings.DEATH_BY_WUMPUS);
                    break;

                case ActionOutcome.NoTunnelAvailable:
                    Console.WriteLine(Strings.MOVE_NO_TUNNEL);
                    break;

                case ActionOutcome.RoomNumberTooHigh:
                    Console.WriteLine(Strings.MOVE_NUMBER_HI);
                    break;

                case ActionOutcome.RoomNumberTooLow:
                    Console.WriteLine(Strings.MOVE_NUMBER_LO);
                    break;

                case ActionOutcome.SurvivedPit:
                    Console.WriteLine(Strings.SURVIVE_PIT);
                    break;
            }

            return actionResult.Is();
        }

        private GameResult ExecuteShot(IEnumerable<int> rooms)
        {
            var actionResult = cave.ShootAt(rooms);

            for (int i = 0; i < actionResult.RandomRoomShot; i++)
                Console.WriteLine(Strings.SHOT_RANDOM_ROOM);
          
            switch (actionResult.Outcome)
            {
                case ActionOutcome.BowstringBroke:
                    Console.WriteLine(Strings.SHOT_BOWSTRING_BROKE);
                    break;

                case ActionOutcome.DeathByLackOfWeaponry:
                    Console.WriteLine(Strings.SHOT_OUT_OF_ARROWS);
                    break;

                case ActionOutcome.DeathBySuicide:
                    Console.WriteLine(Strings.DEATH_ARROW);
                    break;

                case ActionOutcome.DeathByWumpus:
                    Console.WriteLine(Strings.DEATH_BY_WUMPUS);
                    break;

                case ActionOutcome.NoShotTarget:
                    Console.WriteLine(Strings.SHOT_NO_TARGET);
                    break;

                case ActionOutcome.TooManyShotTargets:
                    Console.WriteLine(Strings.SHOT_TOO_FAR);
                    break;

                case ActionOutcome.WumpusKilled:
                    Console.WriteLine(Strings.KILL_WUMPUS);
                    break;
            }

            return actionResult.Is();
        }

        #region private const

        #endregion
    }
}
