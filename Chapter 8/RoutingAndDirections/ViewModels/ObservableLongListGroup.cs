using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAndDirections.ViewModels
{
    public class ObservableLongListGroup<T> : ObservableCollection<T>
    {
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
