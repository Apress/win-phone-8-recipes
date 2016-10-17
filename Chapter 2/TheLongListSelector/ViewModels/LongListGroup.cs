using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLongListSelector.Models;

namespace TheLongListSelector.ViewModels
{

    public class ObservableLongListGroup<T> : ObservableCollection<T>
    {
        //public ObservableLongListGroup()
        //{

        //}
        //public ObservableLongListGroup(IEnumerable<T> collection)
        //    : base(collection)
        //{

        //}
        //public ObservableLongListGroup(List<T> list)
        //    : base(list)
        //{

        //}

        public ObservableLongListGroup(IEnumerable<T> collection, string key)
            : base(collection)
        {
            this.Broken = key;
        }

        public ObservableLongListGroup(List<T> list, string key)
            : base(list)
        {
            this.Broken = key;
        }
        
        public string Broken
        {
            get;
            set;
        }

        private string alsoBroken = "Hello";
        public string AlsoBroken
        {
            get
            {
                return alsoBroken;
            }
            set
            {
                alsoBroken = value;
            }
        }

    }
}
