using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFVideoPlayer.Models
{
    public class VideoModel
    {
        #region " Constructor "

        public VideoModel(string _location)
        {
            Location = _location;
            Title = GetTitle(_location);
        }

        #endregion

        #region " Properties "

        public string Title { get; set; }

        public string Location { get; set; }

        #endregion

        #region " Methods "

        private string GetTitle(string _location)
        {
            return Path.GetFileNameWithoutExtension(_location);
        }

        #endregion
    }
}
