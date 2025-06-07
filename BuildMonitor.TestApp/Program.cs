using BuildMonitor.Core;
using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Updater;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.TestApp
{
    static class Program
    {
        private static readonly Random s_Random = new Random();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var defnOne = new BuildDefinition
            {
                Id = 1,
                Name = "Test 1",
                IsVNext = true
            };

            var defnTwo = new BuildDefinition
            {
                Id = 2,
                Name = "Test 2",
                IsVNext = true
            };

            var storeMoq = new Mock<IBuildStore>();
            storeMoq
                .Setup(s => s.GetDefinitions(It.IsAny<string>()))
                .Returns(Task.Run(() => (IEnumerable<BuildDefinition>)[defnOne, defnTwo]));
            storeMoq
                .Setup(s => s.GetLatestBuild(It.IsAny<string>(), It.Is<BuildDefinition>(d => d.Id == 1)))
                .Returns(Task.Run(() => GetRandomStatus(1)));
            storeMoq
                .Setup(s => s.GetLatestBuild(It.IsAny<string>(), It.Is<BuildDefinition>(d => d.Id == 2)))
                .Returns(Task.Run(() => GetRandomStatus(2)));

            var optionsMoq = new Mock<IMonitorOptions>();
            optionsMoq
                .SetupGet(o => o.IntervalSeconds)
                .Returns(20);
            optionsMoq
                .SetupGet(o => o.ValidOptions)
                .Returns(true);



            var factoryMoq = new Mock<IBuildStoreFactory>();
            factoryMoq
                .Setup(f => f.GetBuildStore(It.IsAny<IMonitorOptions>()))
                .Returns(storeMoq.Object);

            var appUpdaterMoq = new Mock<IAppUpdater>();

            IBuildDefinitionMonitor monitor = new BuildDefinitionMonitor(factoryMoq.Object);

            Application.Run(
                new BuildDefinitionsListForm(monitor,
                                             optionsMoq.Object,
                                             factoryMoq.Object,
                                             appUpdaterMoq.Object)
                );
        }

        private static BuildStatus GetRandomStatus(int id)
        {
            return new()
            {
                Status = (Status)s_Random.Next(1, 5),
                Id = id,
                Start = DateTime.Now.AddHours(-s_Random.Next(1, 5)),
            };
        }
    }
}
