using System;
using System.Collections.Generic;
using System.Linq;
using RMC.Audio.DesignPatterns.Creational.Singleton.CustomSingletonMonobehaviour;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace RMC.Audio
{
	/// <summary>
	/// Maintain a list of AudioSources and play the next 
	/// AudioClip on the first available AudioSource.
	/// </summary>
	public class AudioManager : SingletonMonobehaviour<AudioManager>
	{
		// Properties -------------------------------------
		public List<AudioClip> AudioClips { get { return _audioManagerConfiguration.AudioClips; } }
		
		/// <summary>
		/// LIMITATION: The current implementation of <see cref="AudioManager"/> puts all audio
		/// into one master group. If you want separate groups (e.g. SFX vs Music), update the
		/// implementation.
		/// </summary>
		public AudioMixerGroup MasterAudioMixerGroup { get { return _audioManagerConfiguration.AudioMixer.FindMatchingGroups("Master")[0]; } }
		
		public Volume MasterVolume { get { return _masterVolume;}}
		
		// Fields -----------------------------------------
		[Header("References (Project)")]
		[SerializeField]
		private AudioManagerConfiguration _audioManagerConfiguration = null;

		[SerializeField]
		private List<AudioSource> _audioSources = new List<AudioSource>();
		private const string _MasterVolume = "MasterVolume"; //Must match the AudioMixer assets settings
		private Volume _masterVolume;
		
		// Unity Methods ----------------------------------
		protected void Start()
		{
			//_masterVolume = new Volume(MasterAudioMixerGroup, "MasterVolume", 1);
			
			// Data
			Assert.IsNotNull(_audioManagerConfiguration, "Must have AudioManagerConfiguration instance.");
			Assert.IsNotNull(_audioManagerConfiguration.AudioClips, "Must have AudioClips.");
			Assert.IsTrue(_audioManagerConfiguration.AudioClips.Count > 0, "Must have AudioClips.");
			Assert.IsNotNull(_audioManagerConfiguration.AudioMixer, "Must have AudioMixer.");
			
			// Structure	
			Assert.IsTrue(_audioSources.Count > 0, "Must have AudioSources.");
			AssignAudioMixerToAllAudioSources();
		}
		

		// General Methods --------------------------------
        /// <summary>
        /// This is not called, except through context menu.
        ///
        /// </summary>
        [ContextMenu("Assign All AudioSources From Children")]
        public void AssignAllAllAudioSourcesFromChildren()
        {
            //Clear
            List<AudioSource> audioSources = GetComponentsInChildren<AudioSource>().ToList();
            _audioSources = audioSources;
        }


		/// <summary>
		/// This is called on start.
		///
		/// Optional: You can also call it with the right click menu
		///				per ContextMenu at edit time and bake this into the prefab.
		/// </summary>
		[ContextMenu("Assign AudioMixer To All AudioSources")]
		public void AssignAudioMixerToAllAudioSources()
		{
			// Put all AudioSources into one group (Thus, same volume/pitch). 
			bool isPlayOnAwake = false;
			foreach (AudioSource audioSource in _audioSources)
			{
				audioSource.outputAudioMixerGroup = MasterAudioMixerGroup;
				if (audioSource.playOnAwake)
				{
					isPlayOnAwake = true;
				}
			}

			
			if (isPlayOnAwake)
			{
				Debug.LogWarning("Must manually set each AudioSource.playOnAwake = false in Unity Inspector.");
			}
		}


		
		/// <summary>
		/// Play the AudioClip by index.
		/// </summary>
		public void PlayAudioClip(string audioClipName,
			AudioManagerPlayParameters audioManagerPlayParameters = default(AudioManagerPlayParameters))
		{
			int index = AudioClips.FindIndex (
				(audioClip) => audioClip.name == audioClipName);

			if (index == -1)
			{
				throw new Exception($"PlayAudioClip() failed for audioClipName = {audioClipName}.");
			}
			
			PlayAudioClip(index, audioManagerPlayParameters);
		}


		/// <summary>
		/// Play the AudioClip by index.
		/// </summary>
		public void PlayAudioClip(int audioClipIndex, 
			AudioManagerPlayParameters audioManagerPlayParameters = default(AudioManagerPlayParameters))
		{
			AudioClip audioClip = null;
			try
			{
				audioClip = AudioClips[audioClipIndex];
			}
			catch
			{
				throw new ArgumentException($"PlayAudioClip() failed for index = {audioClipIndex}.");
			}
			
			PlayAudioClip(audioClip, audioManagerPlayParameters);
		}

		
		/// <summary>
		/// Play the AudioClip by reference.
		/// If all sources are occupied, nothing will play.
		/// </summary>
		public AudioSource PlayAudioClip(AudioClip audioClip, 
			AudioManagerPlayParameters audioManagerPlayParameters = default(AudioManagerPlayParameters))
		{
			if (audioClip == null)
			{
				throw new ArgumentException($"PlayAudioClip() failed. AudioClip is null.");
			}

			if (audioManagerPlayParameters.Equals(default(AudioManagerPlayParameters)))
			{
				audioManagerPlayParameters = new AudioManagerPlayParameters();
			}
			
			audioManagerPlayParameters.Validate();
			
			foreach (AudioSource audioSource in _audioSources)
			{
				if (!audioSource.isPlaying)
				{
					audioSource.clip = audioClip;
					// Struct default is ok
					if (!audioManagerPlayParameters.HasDelay)
					{
						audioSource.PlayDelayed(audioManagerPlayParameters.DelayInSeconds);
					}
					else
					{
						audioSource.Play();
					}
					
					return audioSource;
				}
			}

			return null;
		}
		
		
		// Event Handlers ---------------------------------
	}
}