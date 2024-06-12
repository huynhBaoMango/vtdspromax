using System;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public Sound[] sounds;

	void Start ()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		} else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

        Sound s1 = Array.Find(sounds, item => item.name == "theme1");
        s1.source.Play();
    }

	public void Play (string sound)
	{

		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Play();
	}
    public void PlayButWait(string sound)
    {

        Sound s = Array.Find(sounds, item => item.name == sound);
		if (!s.source.isPlaying)
		{
            s.source.Play();
        }
    }

    public void Stop(string sound)
	{

		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Stop(); 
	}

}
