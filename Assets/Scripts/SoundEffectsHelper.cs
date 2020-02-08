using System.Collections;
using UnityEngine;

/// <summary>
/// Creating instance of sounds
/// </summary>
public class SoundEffectsHelper : MonoBehaviour {

    //  sounds need to be triggered at the right time in the game and be called from everywhere
    public static SoundEffectsHelper Instance;

	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;

	void Awake () {
		// Register the singleton
		if (Instance != null) {
			Debug.LogError ("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}

	public void MakeExplosionSound () {
		MakeSound (explosionSound);
	}

	public void MakePlayerShotSound () {
		MakeSound (playerShotSound);
	}

	public void MakeEnemyShotSound () {
		MakeSound (enemyShotSound);
	}

    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="originalClip"></param>
    // playClipAtPipoint : This function creates an audio source but automatically disposes of it once the clip has finished playing.
    private void MakeSound (AudioClip originalClip) {
		AudioSource.PlayClipAtPoint (originalClip, new Vector2(50, 0));
	}
}