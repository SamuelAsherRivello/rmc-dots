using RMC.Audio.Data.Types;
using UnityEngine;

namespace RMC.Audio
{
	/// <summary>
	/// Must stay as struct so that DOTS systems can create it at runtime
	/// </summary>
	public struct AudioManagerPlayParameters
	{
		public float Volume { get; private set; }
		public float Pitch { get; private set; }
		public float DelayInSeconds { get; private set; }
		public bool IsLooping { get; private set; }
		public bool HasDelay { get 	{ return DelayInSeconds == 0; } }
			
		public AudioManagerPlayParameters(
			float volume = AudioConstants.VolumeDefault,
			float pitch = AudioConstants.PitchDefault,
			float delayInSeconds = AudioConstants.DelayInSecondsDefault,
			bool isLooping = AudioConstants.IsLoopingDefault
			)
		{
			Volume = volume;
			Pitch = pitch;
			DelayInSeconds = delayInSeconds;
			IsLooping = isLooping;
		}
		
		
		/// <summary>
		/// Structs offer less control than classes. This method is a workaround.
		/// </summary>
		public void Validate()
		{
			if (Volume == 0)
			{
				Volume = AudioConstants.VolumeDefault;
			}
			
			if (Pitch == 0)
			{
				Pitch = AudioConstants.PitchDefault;
			}
			
		}

		public override string ToString()
		{
			return $"[AudioManagerPlayParameters (Volume: {Volume}, Pitch: {Pitch}, " +
				$"DelayInSeconds: {DelayInSeconds}, IsLooping: {IsLooping})]";
		}


	}
}