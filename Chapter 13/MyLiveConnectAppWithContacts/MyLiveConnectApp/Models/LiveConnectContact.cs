using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiveConnectApp.Models
{
    public class LiveConnectContact : LiveConnectUser
    {

        public string Gender
        {
            get;
            set;
        }

        public bool IsFriend
        {
            get;
            set;
        }

        public bool IsFavourite
        {
            get;
            set;
        }

    }
}
