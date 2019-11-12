namespace Nasa
{
    class MediaTypeQuery
    {
        private bool _image;
        private bool _audio;
        private bool _video;
        private bool _onlySpace;

        public bool image
        {
            get { return this._image; }
            set { this._image = value; }
        }
        public bool audio
        {
            get { return this._audio; }
            set { this._audio = value; }
        }
        public bool video
        {
            get { return this._video; }
            set { this._video = value; }
        }
        public bool onlySpace
        {
            get { return this._onlySpace; }
            set { this._onlySpace = value; }
        }
    }
}
