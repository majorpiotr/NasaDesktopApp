using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Nasa
{
    public class ItemData
    {
        private string _Title;
        private string _date_created;
        private string _nasa_id;
        private string _Description;
        private string _location;
        private string _media_type;
        private List<string> _keywords;
        private string _center;
        private string _href;
        private int _Size;
        private string _ImageDatarget;
        private BitmapImage _ImageData;

        //private string _ImageData;
        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }
        public string date_created
        {
            get { return this._date_created; }
            set { this._date_created = value; }
        }

        public string nasa_id
        {
            get { return this._nasa_id; }
            set { this._nasa_id = value; }
        }
        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        public string location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        public string media_type
        {
            get { return this._media_type; }
            set { this._media_type = value; }
        }


        public List<string> keywords
        {
            get { return this._keywords; }
            set { this._keywords = value; }
        }

        public string center
        {
            get { return this._center; }
            set { this._center = value; }
        }

        public string href
        {
            get { return this._href; }
            set { this._href = value; }
        }

        public int Size
        {
            get { return this._Size; }
            set { this._Size = value; }
        }

        public string ImageDatarget
        {
            get { return this._ImageDatarget; }
            set { this._ImageDatarget = value;}
        }

        public BitmapImage ImageData
        {
            get { return this._ImageData; }
            set { this._ImageData = value; }
        }
        private BitmapImage LoadImage(string filename)
        {

            return new BitmapImage(new Uri(filename));

        }
    }
}
