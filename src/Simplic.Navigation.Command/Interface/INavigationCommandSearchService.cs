using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Navigation.Command
{
    public interface INavigationCommandSearchService
    {
        /// <summary>
        /// Execute navigation selection/search
        /// </summary>
        /// <param name="input">Input or search string</param>
        /// <returns>List of selected commands with arguments</returns>
        IList<NavigationCommandSearchResult> Search(string input);
    }
}
