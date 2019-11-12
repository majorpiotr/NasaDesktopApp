namespace Nasa
{
    class ImageSet
    {
        private string _orig;
        private string _medium;
        private string _small;
        private string _thumb;
        private string _large;
        public string orig
        {
            get { return this._orig; }
            set { this._orig = value; }
        }
        public string medium
        {
            get { return this._medium; }
            set { this._medium = value; }
        }

        public string small
        {
            get { return this._small; }
            set { this._small = value; }
        }

        public string thumb
        {
            get { return this._thumb; }
            set { this._thumb = value; }
        }
        public string large
        {
            get { return this._large; }
            set { this._large = value; }
        }
    }
}
