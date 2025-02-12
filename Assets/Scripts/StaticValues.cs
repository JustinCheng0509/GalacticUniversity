public class StaticValues
{
    public static readonly int TOTAL_NUMBER_OF_DAYS = 10;
    public static readonly bool USE_SKILL_SYSTEM = false;
    public static readonly string LEADERBOARD_JSON_PATH = "/leaderboard.json";
    public static readonly string GAME_DATA_JSON_PATH = "/gamedata.json";
    public static readonly string NEW_GAME_START_TIME = "08:00";
    public static readonly string CLASS_START_TIME = "14:00";
    public static readonly string CLASS_END_TIME = "16:00";
    public static readonly string INTERACTABLE_LAYER = "INTERACTABLE_LAYER";
    public static readonly string INTERACTABLE_TAG_CLASS = "INTERACTABLE_TAG_CLASS";
    public static readonly string INTERACTABLE_TAG_WORK = "INTERACTABLE_TAG_WORK";
    public static readonly string INTERACTABLE_TAG_PLAY = "INTERACTABLE_TAG_PLAY";
    public static readonly string INTERACTABLE_TAG_SLEEP = "INTERACTABLE_TAG_SLEEP";
    public static readonly string INTERACTABLE_TAG_HOMEWORK = "INTERACTABLE_TAG_HOMEWORK";
    public static readonly string INTERACTABLE_TAG_JOB = "INTERACTABLE_TAG_JOB";
    public static readonly string INTERACTABLE_TAG_SHOP = "INTERACTABLE_TAG_SHOP";
    public static readonly string INTERACTABLE_TAG_NPC = "INTERACTABLE_TAG_NPC";
    public static readonly string TRIGGER_TAG_CLASS = "TRIGGER_TAG_CLASS";
    public static readonly string TRIGGER_TAG_DORM = "TRIGGER_TAG_DORM";
    public static readonly string TRIGGER_TAG_SHOP = "TRIGGER_TAG_SHOP";
    public static readonly string TRIGGER_TAG_PLAYROOM = "TRIGGER_TAG_PLAYROOM";
    public static readonly string TRIGGER_TAG_WORK = "TRIGGER_TAG_WORK";
    public static readonly string LAYER_GROUND = "LAYER_GROUND";
    public static readonly string TAG_GRASS = "TAG_GRASS";
    public static readonly string SCENE_OVERWORLD = "Overworld";
    public static readonly string SCENE_MAIN_MENU = "MainMenu";
    public static readonly string SCENE_MINIGAME = "MiniGame";
    public static readonly string TAG_BOSS = "TAG_BOSS";

    public static readonly string[] ALIEN_NAMES = new string[] {
        // Original Alien Names
        "Zyphor", "Xeltran", "Quivex", "Blorvak", "Tynex", "Drizzen", "Vorpax", "Yxarian", "Glixor", "Zarnak",
        "Fyzzor", "Crenthos", "Vexlar", "Zygron", "Orvex", "Threxon", "Blyzor", "Garnok", "Xytrex", "Qorzil",
        "Draxil", "Juvex", "Morlak", "Plythar", "Yzlorn", "Grithos", "Zyphlon", "Vorgax", "Kelthor", "Xyphus",
        "Drallix", "Tynzok", "Blazark", "Norvex", "Thryzix", "Jorlak", "Klyzor", "Xantrox", "Vextar", "Glythox",
        "Zyvrax", "Quorzil", "Bythros", "Xelzak", "Gorthax", "Zyralon", "Thryzak", "Vorzen", "Krallax", "Jenthos",
        "Pyrax", "Xyndor", "Glorvak", "Blythar", "Vyxil", "Zygnar", "Krothos", "Nyzar", "Yzoth", "Gorthak",
        "Xalnok", "Threxil", "Jorvex", "Gralzar", "Zyquar", "Vryzor", "Blyzak", "Klynthor", "Xyzon", "Triznor",
        "Zynak", "Orzex", "Glarnok", "Blithar", "Qynex", "Torzil", "Xybor", "Drithor", "Yzlax", "Varkon",
        "Zorvax", "Grithor", "Blaznok", "Xalvex", "Jythos", "Tlyzak", "Vrynthor", "Zargoth", "Qirzak", "Klython",
        "Thyzor", "Xanvex", "Glythor", "Bryzok", "Zyphlox", "Xenthor", "Vorzak", "Nyzok", "Qalrax", "Jorzak",

        // Cartoonish Alien Names
        "Bloopzo", "Zizzle", "Worp", "Glubnub", "Floonix", "Snorbo", "Mibmop", "Zonkle", "Quibbit", "Tweep",
        "Blorp", "Sploinko", "Fizbot", "Wigglo", "Zargle", "Boingus", "Kribbit", "Yibber", "Flimflam", "Snizz",
        "Gloobix", "Wobbert", "Zibbly", "Mogwump", "Klunk", "Zibzab", "Plinko", "Blarp", "Squeezo", "Yoodle",
        "Gloopz", "Tizzy", "Dorp", "Bizzlo", "Xibber", "Slarpo", "Glimbo", "Twonk", "Fuzzle", "Quirkle",
        "Snorf", "Wibwub", "Zizzer", "Bozzlo", "Flarb", "Mizzle", "Twerpo", "Plibbit", "Grizzle", "Blibble",
        "Zoink", "Bebbo", "Wumpus", "Tibble", "Quazzle", "Drizzle", "Yabble", "Flurbo", "Splib", "Jibbles",
        "Klombo", "Wuzzle", "Glonko", "Snibbit", "Bozz", "Fazzbo", "Noodle", "Zloink", "Bompus", "Kleebo",
        "Fweep", "Squizzle", "Twerp", "Dweebo", "Snizzle", "Bloop", "Morko", "Zimzam", "Pibbit", "Gloobo",
        "Twerpix", "Blizzbit", "Flimpo", "Skizzle", "Dribblo", "Whibbit", "Znozz", "Ploob", "Quibbix", "Snorbo",
        "Fizzbin", "Wibber", "Jimblo", "Gabblo", "Zibbit", "Norb", "Sklarp", "Glibber", "Boffo", "Twizzle"
    };

    public static readonly string GAME_DATA_KEY = "GameData";

    // public static readonly string PLAYER_NAME_KEY = "PlayerName";
    // public static readonly string ENERGY_KEY = "Energy";
    // public static readonly string HUNGER_KEY = "Hunger";
    // public static readonly string MOOD = "Mood";
    // public static readonly string MANEUVERABILITY_KEY = "Maneuverability";
    // public static readonly string DESTRUCTION_KEY = "Destruction";
    // public static readonly string MECHANICS_KEY = "Mechanics";
    // public static readonly string CURRENT_TIME_KEY = "CurrentTime";
    // public static readonly string CURRENT_DAY_KEY = "CurrentDay";
    // public static readonly string TOTAL_DAYS_KEY = "TotalNumberOfDays";
    // public static readonly string TUTORIAL_KEY = "IsTutorialEnabled";
    // public static readonly string DAILY_GAME_DATA_LIST_KEY = "DaoilyGameDataList";
    // public static readonly string LEADERBOARD_KEY = "Leaderboard";

}