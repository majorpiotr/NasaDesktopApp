namespace Nasa
{
    class AudioFileList
    {
        private string _orig;
        private string _mp3;
        private string _metadata;

        public string orig
        {
            get { return this._orig; }
            set { this._orig = value; }
        }

        public string mp3
        {
            get { return this._mp3; }
            set { this._mp3 = value; }
        }
        public string metadata
        {
            get { return this._metadata; }
            set { this._metadata = value; }
        }
    }
}
