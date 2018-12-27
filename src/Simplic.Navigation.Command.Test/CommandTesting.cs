using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplic.Navigation.Command.Service;

namespace Simplic.Navigation.Command.Test
{
    [TestClass]
    public class CommandTesting
    {
        [TestMethod]
        public void SearchTest_SimpleCommand1()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);

            commandService.AddCommand(new NavigationCommand
            {
                Command = "cmd",
                Name = "Console",
                HasArguments = false
            });

            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Contact",
                HasArguments = false
            });

            var result = searchService.Search("crm");

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Command.Command, "crm");
        }

        [TestMethod]
        public void SearchTest_SimpleCommand2()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);

            commandService.AddCommand(new NavigationCommand
            {
                Command = "cmd",
                Name = "Console",
                HasArguments = false
            });

            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Add contact",
                HasArguments = false
            });

            var result = searchService.Search("add c");

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Command.Command, "crm");
            Assert.AreEqual(result[0].Arguments.Count, 0);
        }

        [TestMethod]
        public void SearchTest_SimpleCommand3()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);

            commandService.AddCommand(new NavigationCommand
            {
                Command = "cmd",
                Name = "Console",
                HasArguments = true
            });

            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Add contact",
                HasArguments = false
            });

            var result = searchService.Search("co");

            // Lets check the order. Since we have NO arugment, crm should be over cmd
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].Command.Command, "crm");
            Assert.AreEqual(result[1].Command.Command, "cmd");
        }

        [TestMethod]
        public void SearchTest_CommandOrder3()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);

            commandService.AddCommand(new NavigationCommand
            {
                Command = "cmd",
                Name = "Console",
                HasArguments = true
            });

            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Add contact",
                HasArguments = false
            });

            var result = searchService.Search("co param-1");

            // Lets check the order. Since we have an arugment, cmd should be over crm
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[1].Command.Command, "crm");
            Assert.AreEqual(result[0].Command.Command, "cmd");
        }

        [TestMethod]
        public void SearchTest_ArgumentCommand1()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);
            
            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Contact",
                HasArguments = true
            });

            var result = searchService.Search("Contact param-1");

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Command.Command, "crm");
            Assert.AreEqual(result[0].Arguments[0], "param-1");
        }

        [TestMethod]
        public void SearchTest_ArgumentCommand2()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);

            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Open contact",
                HasArguments = true
            });

            var result = searchService.Search("Open c param-1");

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Command.Command, "crm");
            Assert.AreEqual(result[0].Arguments[0], "param-1");
        }

        [TestMethod]
        public void SearchTest_ArgumentCommand3()
        {
            var commandService = new NavigationCommandService();
            var searchService = new NavigationCommandSearchService(commandService);

            commandService.AddCommand(new NavigationCommand
            {
                Command = "crm",
                Name = "Open contact",
                HasArguments = true
            });

            var result = searchService.Search("o contact param-1");

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Command.Command, "crm");
            Assert.AreEqual(result[0].Arguments[0], "param-1");
        }
    }
}
