using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheLongListSelector.Models;

namespace TheLongListSelector.ViewModels
{
    public class MainViewModel
    {
        // Fields...

        private ObservableCollection<Country> countries = new ObservableCollection<Country>();
        public ObservableCollection<Country> Countries
        {
            get
            {
                return countries;
            }
            set
            {
            	countries = value;
            }
        }
        public ObservableCollection<ObservableLongListGroup<Country>> CountriesByFirstLetter
        {
            get;
            set;
        }


        public ObservableCollection<ObservableLongListGroup<Country>> CountriesByWorldRegion
        {
            get;
            set;
        }

        public void LoadData()
        {
            PopulateGroupsByFirstLetter();
            PopulateGroupsByWorldRegion();

        }

        private void PopulateGroupsByWorldRegion()
        {
            var byWorldRegion = from country in Countries
                                group country by country.WorldRegion into groupedCountries
                                orderby groupedCountries.Key ascending
                                select new ObservableLongListGroup<Country>(groupedCountries, groupedCountries.Key);

            CountriesByWorldRegion = new ObservableCollection<ObservableLongListGroup<Country>>(byWorldRegion);
        }

        private void PopulateGroupsByFirstLetter()
        {
            var byFirstLetter = from country in Countries
                                group country by country.EnglishName.Substring(0, 1).ToLower() into groupedCountries
                                orderby groupedCountries.Key ascending
                                select new ObservableLongListGroup<Country>(groupedCountries, groupedCountries.Key);

            CountriesByFirstLetter = new ObservableCollection<ObservableLongListGroup<Country>>(byFirstLetter);
        }
    }
}
