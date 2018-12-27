using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Navigation.Command.Service
{
    /// <summary>
    /// Commands search implementation
    /// </summary>
    public class NavigationCommandSearchService : INavigationCommandSearchService
    {
        private INavigationCommandService commandService;

        /// <summary>
        /// Initialize search service
        /// </summary>
        /// <param name="commandService">Command service</param>
        public NavigationCommandSearchService(INavigationCommandService commandService)
        {
            this.commandService = commandService;
        }

        /// <summary>
        /// Execute navigation selection/search
        /// </summary>
        /// <param name="input">Input or search string</param>
        /// <returns>List of selected commands with arguments</returns>
        public IList<NavigationCommandSearchResult> Search(string input)
        {
            var searchResults = new List<NavigationCommandSearchResult>();
            var argument = new List<string>();

            var searchParts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (!searchParts.Any())
                return searchResults;

            // Shortcut match
            searchResults.AddRange
                (
                    commandService.Commands.Where(x => !string.IsNullOrWhiteSpace(x.Command))
                             .Where(x => x.Command.ToLower().StartsWith(searchParts[0]?.ToLower()))
                             .OrderBy(x => x.Command)
                             .ThenBy(x => x.Name)
                             .Select(x => new NavigationCommandSearchResult { Command = x })
                );

            // Add argument. I starts with 1, because shortcuts does not allow spaces
            foreach (var app in searchResults)
                for (int i = 1; i < searchParts.Count; i++)
                    app.Arguments.Add(searchParts[i]);

            // 0 = starts with, 1 = contains
            for (var dummyCounter = 0; dummyCounter < 2; dummyCounter++)
            {
                var tempCommands = commandService.Commands.ToList();
                var lastCommands = new List<NavigationCommand>();

                // Go through all commands, until no app exist
                foreach (var searchPart in searchParts)
                {
                    // Filter by shortcut
                    if (dummyCounter == 0)
                        tempCommands = tempCommands.Where(x => x.Name.ToLower().StartsWith(searchPart?.ToLower()))
                                         .OrderBy(x => x.Name).ToList();
                    else
                        tempCommands = tempCommands.Where(x => x.Name.ToLower().Contains(searchPart?.ToLower()))
                                             .OrderBy(x => x.Name).ToList();

                    if (!tempCommands.Any())
                        break;

                    // If shortscuts exists, set them
                    lastCommands = tempCommands.ToList();
                }

                foreach (var command in lastCommands)
                {
                    // If app is not existing in result list
                    if (!searchResults.Any(x => x.Command == command))
                    {
                        var wordCount = command.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count();
                        var searchCommandResult = new NavigationCommandSearchResult
                        {
                            Command = command
                        };

                        // Add argument. A argument is only an entry from the search parts that has a higher index than
                        // the count of words withing the display name
                        for (int i = wordCount; i < searchParts.Count; i++)
                            searchCommandResult.Arguments.Add(searchParts[i]);

                        // Skip if a argument exists but is not required
                        if (!searchCommandResult.Command.HasArguments && searchCommandResult.Arguments.Any())
                            continue;

                        searchResults.Add(searchCommandResult);
                    }
                }
            }

            // Get all shortcuts that requires a shortcut, but has no shortcut
            var noParameterShortCut = searchResults.Where(x => x.Command.HasArguments && !x.Arguments.Any()).
                OrderBy(x => x.Command.Name).ToList();

            // Remove "wrong" shortcuts (requires paramter but has no argument)
            foreach (var appWithoutParameter in noParameterShortCut)
                searchResults.Remove(appWithoutParameter);

            // Readd at the bottom
            searchResults.AddRange(noParameterShortCut);

            return searchResults;
        }
    }
}
