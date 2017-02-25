using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public static int musicPlayerCount = 0;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

	void Start () {
        musicPlayerCount++;

        if(musicPlayerCount >= 2)
        {
            Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
	}

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Music Player: loaded level " + level);
        music.Stop();
        if (level == 0)
        {
            music.clip = startClip;
        }

        if (level == 1)
        {
            music.clip = gameClip;
        }
        if(level == 2)
        {
            music.clip = endClip;
        }
        music.loop = true;
        music.Play();
    }
}
