public class GameConstants
{
    public const int TOTAL_NUMBER_OF_DAYS = 10;
    public const bool USE_SKILL_SYSTEM = true;
    public const string LEADERBOARD_JSON_PATH = "/leaderboard.json";
    public const string GAME_DATA_JSON_PATH = "/gamedata.json";
    public const string NEW_GAME_START_TIME = "08:00";
    public const string CLASS_START_TIME = "14:00";
    public const string CLASS_LATE_TIME = "14:30";
    public const string CLASS_END_TIME = "16:00";
    public const string INTERACTABLE_LAYER = "INTERACTABLE_LAYER";
    public const string INTERACTABLE_TAG_CLASS = "INTERACTABLE_TAG_CLASS";
    public const string INTERACTABLE_TAG_WORK = "INTERACTABLE_TAG_WORK";
    public const string INTERACTABLE_TAG_PLAY = "INTERACTABLE_TAG_PLAY";
    public const string INTERACTABLE_TAG_SLEEP = "INTERACTABLE_TAG_SLEEP";
    public const string INTERACTABLE_TAG_HOMEWORK = "INTERACTABLE_TAG_HOMEWORK";
    public const string INTERACTABLE_TAG_JOB = "INTERACTABLE_TAG_JOB";
    public const string INTERACTABLE_TAG_SHOP = "INTERACTABLE_TAG_SHOP";
    public const string INTERACTABLE_TAG_NPC = "INTERACTABLE_TAG_NPC";
    public const string TRIGGER_TAG_CLASS = "TRIGGER_TAG_CLASS";
    public const string TRIGGER_TAG_DORM = "TRIGGER_TAG_DORM";
    public const string TRIGGER_TAG_SHOP = "TRIGGER_TAG_SHOP";
    public const string TRIGGER_TAG_PLAYROOM = "TRIGGER_TAG_PLAYROOM";
    public const string TRIGGER_TAG_WORK = "TRIGGER_TAG_WORK";
    public const string LAYER_GROUND = "LAYER_GROUND";
    public const string TAG_GRASS = "TAG_GRASS";
    public const string TAG_INDOOR = "TAG_INDOOR";
    public const string SCENE_OVERWORLD = "Overworld";
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_MINIGAME = "Minigame";
    public const string TAG_BOSS = "TAG_BOSS";

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

    public const string GAME_DATA_KEY = "GameData";

    public const string ENEMY_DESTROYED_SFX_AUDIO_SOURCE = "EnemyDestroyedSFXAudioSource";

    public const string BGM_AUDIO_SOURCE = "BGMAudioSource";

    public const int MINIGAME_BASE_DAMAGE = 10;
    public const int MINIGAME_ENEMY_BASE_SCORE = 100;
}