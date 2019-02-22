namespace Wumpus.TextClient
{
    class Strings
    {
        public static string Plural(int count) => (count == 1) ? "" : "s";

        public const string NEWLINE = "\n";

        public const string DEATH_ARROW =
            "*Thwack!*  A sudden piercing feeling informs you that the ricochet" + NEWLINE +
            "of your wild arrow has resulted in it wedging in your side, causing" + NEWLINE +
            "extreme agony.  The evil Wumpus, with its psychic powers, realizes this" + NEWLINE +
            "and immediately rushes to your side, not to help, alas, but to EAT YOU!" + NEWLINE +
            "(*CHOMP*)" + NEWLINE;

        public const string DEATH_BY_PIT =
            "*AAAUUUUGGGGGHHHHHhhhhhhhhhh...*" + NEWLINE +
            "The whistling sound and updraft as you walked into this room of the" + NEWLINE +
            "cave apparently wasn't enough to clue you in to the presence of the" + NEWLINE +
            "bottomless pit.  You have a lot of time to reflect on this error as" + NEWLINE +
            "you fall many miles to the core of the earth.  Look on the bright side;" + NEWLINE +
            "you can at least find out if Jules Verne was right..." + NEWLINE;

        public const string DEATH_BY_WUMPUS =
            "*ROAR* *chomp* *snurfle* *chomp*!" + NEWLINE +
            "Much to the delight of the Wumpus, you walked right into his mouth," + NEWLINE +
            "making you one of the easiest dinners he's ever had!  For you, however," + NEWLINE +
            "it's a rather unpleasant death.  The only good thing is that it's been" + NEWLINE +
            "so long since the evil Wumpus cleaned his teeth that you immediately" + NEWLINE +
            "passed out from the stench!" + NEWLINE;

        public const string DESCRIBE_BASIC_SITUATION =
            "You are in room {0} of the cave, and have {1} arrow{2} left.";

        public const string DESCRIBE_BAT_SENSATION =
            "*rustle* *rustle* (must be bats nearby)";

        public const string DESCRIBE_PIT_SENSATION =
            "*whoosh* (I feel a draft from some pits).";

        public const string DESCRIBE_WUMPUS_SENSATION =
            "*sniff* (I can smell the evil Wumpus nearby!)";

        public const string DESCRIBE_NEIGHBOURS_START =
            "There are tunnels to rooms ";

        public const string DESCRIBE_NEIGHBOURS_TUNNEL_ANY =
            "{0}, ";

        public const string DESCRIBE_NEIGHBOURS_TUNNEL_LAST =
            "and {0}." + NEWLINE;

        public const string GREETING = NEWLINE +
            "You're in a cave with {0} rooms and {1} tunnels leading from each room." + NEWLINE +
            "There are {2} bat{3} and {4} pit{5} scattered throughout the cave, and your" + NEWLINE +
            "quiver holds {6} custom super anti-evil Wumpus arrows.  Good luck." + NEWLINE;

        public const string KILL_WUMPUS =
            "*thwock!* *groan* *crash*" + NEWLINE +
            "A horrible roar fills the cave, and you realize, with a smile, that you" + NEWLINE +
            "have slain the evil Wumpus and won the game!  You don't want to tarry for" + NEWLINE +
            "long, however, because not only is the Wumpus famous, but the stench of" + NEWLINE +
            "dead Wumpus is also quite well known, a stench plenty enough to slay the" + NEWLINE +
            "mightiest adventurer at a single whiff!!" + NEWLINE;

        public const string MOVE_NO_TUNNEL =
            "*Oof!*  (You hit the wall)" + NEWLINE;

        public const string MOVE_NUMBER_LO =
            "Sorry, but we're constrained to a semi-Euclidean cave!" + NEWLINE;

        public const string MOVE_NUMBER_HI =
            "What?  The cave surely isn't quite that big!" + NEWLINE;

        public const string OUT_OF_ARROWS =
            "You turn and look at your quiver, and realize with a sinking feeling" + NEWLINE +
            "that you've just shot your last arrow (figuratively, too).  Sensing this" + NEWLINE +
            "with its psychic powers, the evil Wumpus rampagees through the cave, finds" + NEWLINE +
            "you, and with a mighty *ROAR* eats you alive!";

        public const string REQUEST_ACTION =
            "Move or shoot? (m-s) ";

        public const string REQUEST_ERROR1 =
            "Que pasa?" + NEWLINE;

        public const string REQUEST_ERROR2 =
            "I don't understand!" + NEWLINE;

        public const string SURVIVE_PIT =
            "Without conscious thought you grab for the side of the cave and manage" + NEWLINE +
            "to grasp onto a rocky outcrop.  Beneath your feet stretches the limitless" + NEWLINE +
            "depths of a bottomless pit!  Rock crumbles beneath your feet!" + NEWLINE;

        public const string MOVED_BY_BATS1 =
            "*flap*  *flap*  *flap*  (humongous bats pick you up and move you!" + NEWLINE;

        public const string MOVED_BY_BATS2 =
            "*flap*  *flap*  *flap*  (humongous bats pick you up and move you again!" + NEWLINE;

        public const string WUMPUS_MOVED =
            "Your colorful comments awaken the wumpus!" + NEWLINE;

        public const string SHOT_BOWSTRING_BROKE =
            "Your bowstring breaks!  *twaaaaaang*" + NEWLINE +
            "The arrow is weakly shot and can go no further!" + NEWLINE;

        public const string SHOT_NO_TARGET =
            "The arrow falls to the ground at your feet!" + NEWLINE;

        public const string SHOT_OUT_OF_ARROWS =
            "You turn and look at your quiver, and realize with a sinking feeling" + NEWLINE +
            "that you've just shot your last arrow (figuratively, too).  Sensing this" + NEWLINE +
            "with its psychic powers, the evil Wumpus rampagees through the cave, finds" + NEWLINE +
            "you, and with a mighty *ROAR* eats you alive!" + NEWLINE;

        public const string SHOT_RANDOM_ROOM =
            "*thunk*  The arrow can't find a way from {0} to {1} and flys randomly" + NEWLINE +
            "into room {2}!" + NEWLINE;

        public const string SHOT_TOO_FAR =
            "The arrow wavers in its flight and and can go no further!" + NEWLINE;
    }
}
