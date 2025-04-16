public class MinigameConstants
{
    // Enemy settings
    public const float MINIGAME_ENEMY_BASE_SCORE = 100f;
    public const float MINIGAME_ENEMY_BASE_HEALTH = 100f;
    public const float MINIGAME_ENEMY_BASE_DAMAGE = 10f;
    public const float MINIGAME_ENEMY_BASE_MIN_SPEED = 6f;
    public const float MINIGAME_ENEMY_BASE_MAX_SPEED = 10f;
    public const float MINIGAME_ENEMY_BASE_MIN_SCALE = 1f;
    public const float MINIGAME_ENEMY_BASE_MAX_SCALE = 2f;
    public const float MINIGAME_ENEMY_DAILY_MAX_SCALE_INCREASE = 0.4f;
    public const float MINIGAME_ENEMY_BASE_SPAWN_DELAY = 0.4f;
    public const float MINIGAME_ENEMY_DAILY_SPAWN_DELAY_DECREASE = 0.05f;
    public const string MINIGAME_ENEMY_DESTROYED_SFX_AUDIO_SOURCE = "EnemyDestroyedSFXAudioSource";

    // Player settings
    public const float MINIGAME_PLAYER_BASE_DAMAGE = 10f;
    public const float MINIGAME_PLAYER_TERMINAL_SPEED = 20f;
    public const float MINIMGAME_PLAYER_MIN_SPEED_FACTOR = 0.25f;
    public const float MINIMGAME_PLAYER_MAX_SPEED_FACTOR = 1f;
    public const float MINIGAME_PLAYER_BASE_DEATH_PENALTY = 500f;

    // NPC score generation settings
    public const float MINIGAME_NPC_MIN_RANGE = 0.5f;
    public const float MINIGAME_NPC_MAX_RANGE = 1.5f;
}