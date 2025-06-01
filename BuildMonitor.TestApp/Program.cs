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

            var defnOneMoq = new Mock<IBuildDefinition>();
            defnOneMoq
                .SetupGet(d => d.Id).Returns(1);
            defnOneMoq
                .SetupGet(d => d.Name).Returns("Test 1");
            defnOneMoq
                .SetupGet(d => d.IsVNext).Returns(true);

            var defnTwoMoq = new Mock<IBuildDefinition>();
            defnTwoMoq
                .SetupGet(d => d.Id).Returns(2);
            defnTwoMoq
                .SetupGet(d => d.Name).Returns("Test 2");
            defnTwoMoq
                .SetupGet(d => d.IsVNext).Returns(true);

            var storeMoq = new Mock<IBuildStore>();
            storeMoq
                .Setup(s => s.GetDefinitions(It.IsAny<string>()))
                .Returns(Task.Run(() => (IEnumerable<IBuildDefinition>)new[] { defnOneMoq.Object, defnTwoMoq.Object }));
            storeMoq
                .Setup(s => s.GetLatestBuild(It.IsAny<string>(), It.Is<IBuildDefinition>(d => d.Id == 1)))
                .Returns(Task.Run(() => GetRandomStatus(1)));
            storeMoq
                .Setup(s => s.GetLatestBuild(It.IsAny<string>(), It.Is<IBuildDefinition>(d => d.Id == 2)))
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

        private static IBuildStatus GetRandomStatus(int id)
        {
            var statusMoq = new Mock<IBuildStatus>();
            statusMoq.Setup(s => s.Status).Returns((Status)s_Random.Next(1, 5));
            statusMoq.Setup(s => s.Id).Returns(id);
            statusMoq.Setup(s => s.Start).Returns(DateTime.Now.AddHours(-3));
            return statusMoq.Object;
        }
    }
}
