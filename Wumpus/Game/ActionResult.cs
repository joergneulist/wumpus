using System;
using System.Collections.Generic;
using System.Text;

namespace Wumpus.Game
{
    public enum GameResult { Ongoing, Defeat, Victory, Abort };

    public enum ActionOutcome
    {
        BowstringBroke,
        DeathByLackOfWeaponry,
        DeathByPit,
        DeathBySuicide,
        DeathByWumpus,
        WumpusKilled,
        NoShotTarget,
        NoTunnelAvailable,
        RoomNumberTooLow,
        RoomNumberTooHigh,
        SurvivedPit,
        TooManyShotTargets,
        Ok
    }

    public class ActionResult
    {
        public ActionOutcome Outcome { get; set; } = ActionOutcome.Ok;

        public bool IsOk()
            => Outcome == ActionOutcome.Ok;

        public GameResult Is()
        {
            switch (Outcome)
            {
                case ActionOutcome.DeathByLackOfWeaponry:
                case ActionOutcome.DeathByPit:
                case ActionOutcome.DeathBySuicide:
                case ActionOutcome.DeathByWumpus:
                    return GameResult.Defeat;

                case ActionOutcome.WumpusKilled:
                    return GameResult.Victory;

                default:
                    return GameResult.Ongoing;
            }
        }
    }

    public class MoveResult : ActionResult
    {
        public int MovedByBatsCount { get; set; } = 0;
        public int WumpusMovedCount { get; set; } = 0;
    }

    public class ShootResult : ActionResult
    {
        public int RandomRoomShot { get; set; } = 0;
    }
}
