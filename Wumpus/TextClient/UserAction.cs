using System;
using System.Collections.Generic;
using System.Linq;

namespace Wumpus.TextClient
{
    public enum ActionType { None, Exit, Move, Shoot };

    public class UserAction
    {
        public ActionType Action { get; set; } = ActionType.None;
        public List<int> Parameters { get; set; } = new List<int>();

        public void ParseFrom(string input)
        {
                Action = ActionType.None;

            if (input.Length > 0)
                switch (Char.ToLower(input[0]))
            {
                case 'm':
                    Action = ActionType.Move;
                    break;
                case 's':
                    Action = ActionType.Shoot;
                    break;
                case 'q':
                case 'x':
                    Action = ActionType.Exit;
                    break;
            }

            if ((Action == ActionType.Move) || (Action == ActionType.Shoot))
            {
                Parameters = new List<int>();
                foreach (var token in input.Substring(1).Split(" \t\n"))
                {
                    int result;
                    if (int.TryParse(token, out result))
                        Parameters.Add(result);
                    else
                        Parameters.Add(-1);
                }
            }

            if (Action == ActionType.None)
            {
                if (new Math().IsOneInNChance(15))
                    Console.WriteLine(Strings.REQUEST_ERROR1);
                else
                    Console.WriteLine(Strings.REQUEST_ERROR2);
            }
        }
    }

}
