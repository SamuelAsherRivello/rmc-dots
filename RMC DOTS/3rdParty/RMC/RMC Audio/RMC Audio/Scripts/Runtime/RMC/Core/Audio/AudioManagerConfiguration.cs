using System.Collections.Generic;
using RMC.Audio.Data.Types;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace RMC.Audio
{
	/// <summary>
	/// Maintain a list of AudioSources and play the next 
	/// AudioClip on the first available AudioSource.
	/// </summary>
	[CreateAssetMenu( menuName = AudioConstants.PathCoreCreateAssetMenu + Title,  fileName = Title, order = AudioConstants.PriorityTools_Primary)]
	public class AudioManagerConfiguration: ScriptableObject
	{
	 	// Properties -------------------------------------
		public List<AudioClip> AudioClips { get { return _audioClips; } }
		public AudioMixer AudioMixer { get { return _audioMixer; } }
		
		//  Fields ----------------------------------------
		private const string Title = "AudioManagerConfiguration";

		[SerializeField] 
		private AudioMixer _audioMixer;

		[SerializeField]
		private List<AudioClip> _audioClips = new List<AudioClip>();

	}
}