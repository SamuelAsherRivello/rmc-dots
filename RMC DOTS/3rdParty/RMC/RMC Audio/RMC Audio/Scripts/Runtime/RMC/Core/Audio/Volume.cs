
using UnityEngine;
using UnityEngine.Audio;

namespace RMC.Audio
{
    /// <summary>
    /// Wrapper so a type can be observable via events
    /// </summary>
    public class Volume 
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public float Value
        {
            get
            {
                float volume = 1;
				
                // Convert with logarithmic sound formula
                bool isFound = _audioMixerGroup.audioMixer.GetFloat(_volumeParameterName, out volume);
                if (!isFound)
                {
                    Debug.LogError("MasterVolume () failed.");
                }
                volume = Mathf.Pow(10,volume/20);
                if (Mathf.Approximately(volume ,MinimumVolume))
                {
                    //The log math does not allow for 0 for muted, but the user expects to see "0" when muted
                    volume = 0;
                }
                return volume;
            }
            set
            {
                float nextVolume = value;
                if (nextVolume < MinimumVolume)
                {
                    nextVolume = MinimumVolume;
                    Debug.LogWarning($"MasterVolume corrected to MinimumVolume value of 0."); // keep "0"
                }
                else if (nextVolume > MaximumVolume)
                {
                    nextVolume = MaximumVolume;
                    Debug.LogWarning($"MasterVolume corrected to MaximumVolume value of {nextVolume}");
                }
                
                // Convert with logarithmic sound formula
                _audioMixerGroup.audioMixer.SetFloat(_volumeParameterName, Mathf.Log10(nextVolume) * 20);
            }
        }

        public float MinimumVolume = .0001f;
        public float MaximumVolume = 1;

        //  Fields ----------------------------------------
        private readonly AudioMixerGroup _audioMixerGroup;
        private readonly string _volumeParameterName;

        //  Constructor Methods ---------------------------
        public Volume(AudioMixerGroup audioMixerGroup, string volumeParameterName, float initialVolume)
        {
            _audioMixerGroup = audioMixerGroup;
            _volumeParameterName = volumeParameterName;
            Value = initialVolume;
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }

}
   