using System.Media;
using System.Reflection;

namespace GameSelector
{
    internal class AudioPlayer
    {
        private SoundPlayer _selectionStartPlayer;
        private SoundPlayer _selectionCompletePlayer;
        private SoundPlayer _endSessionPlayer;

        public AudioPlayer()
        {
            Assembly a = Assembly.GetExecutingAssembly();

            var selectionStartFile = a.GetManifestResourceStream("GameSelector.audio.zoeken.wav");
            var selectionCompleteFile = a.GetManifestResourceStream("GameSelector.audio.spel_gevonden.wav");
            var endSessionFile = a.GetManifestResourceStream("GameSelector.audio.begin_spel.wav");

            _selectionStartPlayer = new SoundPlayer(selectionStartFile);
            _selectionCompletePlayer = new SoundPlayer(selectionCompleteFile);
            _endSessionPlayer = new SoundPlayer(endSessionFile);
        }

        public void PlaySelectionStart()
        {
            _selectionStartPlayer.Play();
        }

        public void PlaySelectionComplete()
        {
            _selectionCompletePlayer.Play();
        }

        public void PlayEndSession()
        {
            _endSessionPlayer.Play();
        }
    }
}
