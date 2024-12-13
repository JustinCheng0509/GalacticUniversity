using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class OverworldMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 moveDirection;

    public InputActionReference move;

    public AudioClip[] footstepGrass;
    public AudioClip[] footstepConcrete;

    [SerializeField]
    private Transform footstepPosition;

    [SerializeField]
    private GridLayout gridLayout;

    [SerializeField]
    private Tilemap[] groundTilemaps;

    [SerializeField]
    private AudioSource footstepAudioSource;

    private bool delayFootstep = false;

    [SerializeField]
    private PlayerInfo playerInfo;

    private void Update() {
        moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);     
        if (moveDirection != Vector2.zero) {
            PlayFootstepSound();
            if (playerInfo.IsBusy()) {
                playerInfo.CancelActions();
            }
        }
    }

    private void PlayFootstepSound() {
        if (footstepAudioSource.isPlaying || delayFootstep) {
            return;
        }

        AudioClip[] footstepSounds = footstepConcrete;

        string tileTag = GetTileTag(footstepPosition.position);

        if (tileTag == CustomString.TAG_GRASS) {
            footstepSounds = footstepGrass;
        }
        
        footstepAudioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        footstepAudioSource.volume = Random.Range(0.25f, 0.3f);
        footstepAudioSource.pitch = Random.Range(0.9f, 1.05f);
        footstepAudioSource.Play();

        delayFootstep = true;
        Invoke("ResetFootstepDelay", 0.3f);
    }

    private string GetTileTag(Vector2 footstepPosition) {
        Vector3Int cellPosition = gridLayout.WorldToCell(footstepPosition);
        // Check each tilemap for the tile at the given position
        foreach (Tilemap tilemap in groundTilemaps) {
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null) {
                return tilemap.tag;
            }
        }
        return null;
    }

    private void ResetFootstepDelay() {
        delayFootstep = false;
    }
}
