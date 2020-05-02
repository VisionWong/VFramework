using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFramework
{
    /// <summary>
    /// 2D音频管理器，可播放BGM和音效
    /// </summary>
	public class AudioMgr : MonoSingleton<AudioMgr>
	{
        private AudioSource _bgm;   //负责播放背景音乐
        private AudioSource _sound; //负责播放音效

        private float _bgmVol = 1f;
        private float _soundVol = 1f;

        private void Awake()
        {
            GameObject audioRoot = new GameObject("AudioRoot");
            DontDestroyOnLoad(audioRoot);
            _bgm = audioRoot.AddComponent<AudioSource>();
            _sound = audioRoot.AddComponent<AudioSource>();
            InitBGMPlayer();
            InitSoundPlayer();
        }

        /// <summary>
        /// 异步加载背景音乐并播放
        /// </summary>
        public void PlayBGM(string path, bool isLoop = true)
        {
            if (_bgm != null)
            {
                ResourceMgr.Instance.LoadAsync<AudioClip>(path, clip =>
                {
                    _bgm.clip = clip;
                    _bgm.loop = isLoop;
                });
            }
        }

        /// <summary>
        /// 继续播放当前BGM
        /// </summary>
        public void PlayBGM()
        {
            if (_bgm != null)
            {
                if (_bgm.clip != null && !_bgm.isPlaying)
                {
                    _bgm.Play();
                }
            }
        }

        public void PauseBGM()
        {
            if (_bgm != null)
            {
                if (_bgm.clip != null && _bgm.isPlaying)
                {
                    _bgm.Pause();
                }
            }
        }

        public void StopBGM()
        {
            if (_bgm != null)
            {
                if (_bgm.clip != null && _bgm.isPlaying)
                {
                    _bgm.Stop();
                }
            }
        }

        /// <summary>
        /// 改变背景音乐音量
        /// </summary>
        /// <param name="volume"></param>
        public void SetBGMVolume(float volume)
        {
            if (_bgm != null)
            {
                _bgm.volume = volume;
            }
        }

        /// <summary>
        /// 设置背景音乐是否静音
        /// </summary>
        /// <param name="isMute"></param>
        public void SetBGMMute(bool isMute)
        {
            if (_bgm != null)
            {
                _bgm.mute = isMute;
            }
        }

        /// <summary>
        /// 异步加载音效并播放
        /// </summary>
        public void Play2DSound(string path, float volume)
        {
            if (_sound != null)
            {
                ResourceMgr.Instance.LoadAsync<AudioClip>(path, clip =>
                {
                    _sound.PlayOneShot(clip);
                });
            }
        }

        /// <summary>
        /// 设置音效播放器是否静音
        /// </summary>
        /// <param name="isMute"></param>
        public void SetSoundMute(bool isMute)
        {
            if (_sound != null)
            {
                _sound.mute = isMute;
            }
        }

        /// <summary>
        /// 改变音效的音量
        /// </summary>
        /// <param name="volume"></param>
        public void SetSoundVolume(float volume)
        {
            if (_sound != null)
            {
                _sound.volume = volume;
            }
        }

        /// <summary>
        /// 设置所有音量是否静音
        /// </summary>
        /// <param name="isMute"></param>
        public void SetGlobalMute(bool isMute)
        {
            SetBGMMute(isMute);
            SetSoundMute(isMute);
        }

        /// <summary>
        /// 初始化BGM播放器设置
        /// </summary>
        private void InitBGMPlayer()
        {
            if (_bgm != null)
            {
                _bgm.loop = true;
                _bgm.volume = _bgmVol;
                _bgm.playOnAwake = false;
            }
            else
            {
                Debug.LogError("BGM播放器初始化失败！");
            }
        }

        /// <summary>
        /// 初始化音效播放器设置
        /// </summary>
        private void InitSoundPlayer()
        {
            if (_sound != null)
            {
                _sound.loop = false;
                _sound.volume = _soundVol;
                _sound.playOnAwake = false;
            }
            else
            {
                Debug.LogError("音效播放器初始化失败！");
            }
        }
    }
}
