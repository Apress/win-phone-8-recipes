using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlipTiles.Models;

namespace FlipTiles.ViewModels
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
            this.Key = key;
        }

        public ObservableLongListGroup(List<T> list, string key)
            : base(list)
        {
            this.Key = key;
        }
        
        public string Key
        {
            get;
            set;
        }

    }
}
