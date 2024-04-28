using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Audio_Manager
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField, Tooltip("音效播放器")] private AudioSource soundPlayer;
        [SerializeField, Tooltip("音乐播放器")] private AudioSource musicPlayer;

        [SerializeField] private List<Audio> musicList;
        [SerializeField] private List<Audio> soundList;
        
        //改变音调
        const float pitchMin = 0.9f;
        const float pitchMax = 1.1f;

        #region 音量设置
        //AudioMixer音量设置
        public void SetMasterVolume(float value)
        {
            var tmp = value * 100 - 80;
            audioMixer.SetFloat("vMaster", tmp);
        }
        public void SetMusicVolume(float value)
        {
            var tmp = value * 100 - 80;
            audioMixer.SetFloat("vMusic", tmp);
        }
        public void SetSfxVolume(float value)
        {
            var tmp = value * 100 - 80;
            audioMixer.SetFloat("vSound", tmp);
        }
        //AudioResource音量设置
        public void SetMusicSourceVolume(float value)
        {
            musicPlayer.volume = value;
        }
        public void SetSoundSourceVolume(float value)
        {
            soundPlayer.volume = value;
        }
        #endregion

        #region 音效管理
        public void PlaySound(string mName)
        {
            var clip = Find(mName, soundList);
            if (clip == null) { return; }
            PlaySound(clip);
        }
        private void PlaySound(AudioClip audioClip)
        {
            soundPlayer.Stop();
            soundPlayer.pitch = 1;
            soundPlayer.PlayOneShot(audioClip);
        }
     
        // 改变音调，主要用于重复播放的音效
        public void PlayRandomSound(string mName)
        {
            var clip = Find(mName, soundList);
            if (clip == null) { return; }
            PlayRandomSound(clip);
            Debug.LogWarning("AudioManager:播放音效" + mName);
        }
        private void PlayRandomSound(AudioClip audioClip)
        {
            soundPlayer.Stop();
            soundPlayer.pitch = Random.Range(pitchMin , pitchMax );
            soundPlayer.PlayOneShot(audioClip);
        }
        #endregion

        #region 音乐管理

        /// <summary>
        /// 循环播放音乐
        /// </summary>
        /// <param name="mName"></param>
        public void PlayMusic(string mName)
        {
            var clip = Find(mName, musicList);
            if (clip == null) { return; }
            PlayMusic(clip);
        }
        private void PlayMusic(AudioClip audioClip)
        {
            StopMusic();
            musicPlayer.clip = audioClip;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        /// <summary>
        /// 停止播放音乐
        /// </summary>
        public void StopMusic()
        {
            musicPlayer.Stop();
        }
        #endregion

        private AudioClip Find(string name, List<Audio> audioList)
        {
            foreach (var audio in audioList)
            {
                if (audio.name == name)
                {
                    return audio.audioClip;
                }
            }
            Debug.LogWarning("AudioManager:找不到对应音频");
            return null;
        }
    }

    [Serializable]
    class Audio
    {
        public string name;
        public AudioClip audioClip;
    }
}
