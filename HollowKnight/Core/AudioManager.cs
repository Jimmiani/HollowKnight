using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollowKnight.Core
{
    internal class AudioManager
    {
        // ─────────────── VOLUME LEVELS ───────────────
        private static float _masterVolume = 1f;
        private static float _musicVolume = 1f;
        private static float _sfxVolume = 1f;

        // Multiplier for temporary effects (e.g. "player hit" volume duck)
        private static float _volumeScale = 1f;

        // ─────────────── MUSIC CONTROL ───────────────
        private static Song _currentSong;

        // Optional collection for tracking currently playing SFX
        private static List<SoundEffectInstance> _activeSfx = new();

        // ─────────────── INITIALIZATION ───────────────
        public static void Initialize()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = _masterVolume * _musicVolume;
        }

        // ─────────────── MUSIC METHODS ───────────────
        public static void PlayMusic(Song song, bool loop = true)
        {
            if (_currentSong == song) return;
            _currentSong = song;
            MediaPlayer.IsRepeating = loop;
            MediaPlayer.Play(song);
            UpdateMusicVolume();
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

        private static void UpdateMusicVolume()
        {
            MediaPlayer.Volume = _masterVolume * _musicVolume * _volumeScale;
        }

        // ─────────────── SFX METHODS ───────────────
        public static void PlaySfx(SoundEffect sfx, float volume = 1f, float pitch = 0f, float pan = 0f, bool bypassScale = false)
        {
            var instance = sfx.CreateInstance();

            float finalVolume = _masterVolume * _sfxVolume * volume;

            // If this SFX should not be affected by the global volume scale (e.g. hit sounds)
            if (!bypassScale)
                finalVolume *= _volumeScale;

            instance.Volume = finalVolume;
            instance.Pitch = pitch;
            instance.Pan = pan;

            instance.Play();
            _activeSfx.Add(instance);
        }

        // Call this to clean up finished sounds (optional, but helps)
        public static void Update()
        {
            _activeSfx.RemoveAll(s => s.State == SoundState.Stopped);
        }

        // ─────────────── GLOBAL VOLUME CONTROL ───────────────
        public static void SetMasterVolume(float volume)
        {
            _masterVolume = MathHelper.Clamp(volume, 0f, 1f);
            UpdateVolumes();
        }

        public static void SetMusicVolume(float volume)
        {
            _musicVolume = MathHelper.Clamp(volume, 0f, 1f);
            UpdateMusicVolume();
        }

        public static void SetSfxVolume(float volume)
        {
            _sfxVolume = MathHelper.Clamp(volume, 0f, 1f);
            UpdateSfxVolumes();
        }

        private static void UpdateSfxVolumes()
        {
            foreach (var sfx in _activeSfx)
            {
                if (sfx.State == SoundState.Playing)
                    sfx.Volume = _masterVolume * _sfxVolume * _volumeScale;
            }
        }

        private static void UpdateVolumes()
        {
            UpdateMusicVolume();
            UpdateSfxVolumes();
        }

        // ─────────────── SPECIAL EFFECTS (VOLUME DUCKING) ───────────────
        public static void SetVolumeScale(float scale)
        {
            _volumeScale = MathHelper.Clamp(scale, 0f, 1f);
            UpdateVolumes();
        }
    }
}
