using BuildMonitor.Core;
using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Options;
using BuildMonitor.UI.Updater;
using Moq;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BuildMonitor.TestApp
{
    static class Program
    {
        private static readonly Random s_Random = new();
        private static int RandomBetween(int min, int max) => s_Random.Next(min, max + 1);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BuildDefinition[] definitions =
                [
                    ..Enumerable
                        .Range(1, 8)
                        .Select(i => new BuildDefinition
                        {
                            Id = i,
                            Name = $"Test {i}",
                            IsVNext = true,
                            Url = $"https://fake.dev/pipeline/{i}"
                        })
                ];

            var storeMoq = new Mock<IBuildStore>();
            storeMoq
                .Setup(s => s.GetDefinitions())
                .ReturnsAsync(() => definitions.Take(RandomBetween(1, 8)));
            storeMoq
                .Setup(s => s.GetLatestBuild(It.IsAny<BuildDefinition>()))
                .ReturnsAsync((BuildDefinition defn) => GetRandomStatus(defn));

            var options = new MonitorOptions();
            options.Reset();
            options.IntervalSeconds = 20;
            options.RefreshDefintions = true;
            options.RefreshDefinitionIntervalSeconds = 45;
            options.ValidOptions = true;

            var factoryMoq = new Mock<IBuildStoreFactory>();
            factoryMoq
                .Setup(f => f.GetBuildStore(It.IsAny<IMonitorOptions>(), It.IsAny<bool>()))
                .Returns(storeMoq.Object);

            var appUpdaterMoq = new Mock<IAppUpdater>();

            var monitor = new BuildDefinitionMonitor(factoryMoq.Object);

            Application.Run(
                new BuildDefinitionsListForm(monitor,
                                             options,
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
                Url = $"{forDefintion.Url}/build/{RandomBetween(1, 24)}",
                RequestedBy = $"User{s_Random.Next(1, 100)}",
                Status = (Status)s_Random.Next(1, 5),
                Start = DateTimeOffset.Now.AddHours(-RandomBetween(1, 4)),
                WarningCount = RandomBetween(0, 20),
                ErrorCount = RandomBetween(0, 1)
            };
        }
    }
}
