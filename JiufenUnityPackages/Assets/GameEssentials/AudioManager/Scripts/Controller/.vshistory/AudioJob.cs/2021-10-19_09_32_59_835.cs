namespace Jiufen.Audio
{
    internal class AudioJob
    {
        private AudioAction audioAction;
        private AudioType audioType;

        public AudioJob(AudioAction audioAction, AudioType audioType)
        {
            this.audioAction = audioAction ;
            this.audioType = audioType;
        }
    }
}