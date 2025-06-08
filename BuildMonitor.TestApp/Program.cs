using BuildMonitor.Core;
using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Updater;
using Moq;
using System;
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
                IsVNext = true,
                Url = "https://fake.dev/pipeline/1"
            };

            var defnTwo = new BuildDefinition
            {
                Id = 2,
                Name = "Test 2",
                IsVNext = true,
                Url = "https://fake.dev/pipeline/2"
            };

            var storeMoq = new Mock<IBuildStore>();
            storeMoq
                .Setup(s => s.GetDefinitions())
                .ReturnsAsync(() => [defnOne, defnTwo]);
            storeMoq
                .Setup(s => s.GetLatestBuild(It.IsAny<BuildDefinition>()))
                .ReturnsAsync((BuildDefinition defn) => GetRandomStatus(defn));

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

            var monitor = new BuildDefinitionMonitor(factoryMoq.Object);

            Application.Run(
                new BuildDefinitionsListForm(monitor,
                                             optionsMoq.Object,
                                             factoryMoq.Object,
                                             appUpdaterMoq.Object)
                );
        }

        private static BuildStatus GetRandomStatus(BuildDefinition forDefintion)
        {
            return new()
            {
                Id = forDefintion.Id,
                Name = forDefintion.Name,
                Url = $"{forDefintion.Url}/build/{s_Random.Next(1, 25)}",
                RequestedBy = $"User{s_Random.Next(1, 100)}",
                Status = (Status)s_Random.Next(1, 5),
                Start = DateTimeOffset.Now.AddHours(-s_Random.Next(1, 5)),
            };
        }
    }
}
