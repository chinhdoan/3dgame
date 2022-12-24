using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	public float clipLength;
	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}


		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.outputAudioMixerGroup = s.mixer;
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
    private void Start()
	{ 
	}
    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Play();
		clipLength = s.clip.length;


	}
	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Stop();
	}
	public void PlayOnce(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.PlayOneShot(s.clip);


	}
}