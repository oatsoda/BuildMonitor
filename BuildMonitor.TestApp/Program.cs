using System;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Controls;
using Moq;

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
            
            var defnMoq = new Mock<IBuildDefinition>();
            defnMoq
                .SetupGet(d => d.Id)
                .Returns(1);
            defnMoq
                .SetupGet(d => d.Name)
                .Returns("Test");

            var storeMoq = new Mock<IBuildStore>();
            storeMoq
                .Setup(s => s.GetDefinitions(It.IsAny<string>()))
                .Returns(new[] { defnMoq.Object });
            storeMoq
                .Setup(s => s.GetLatestBuild(It.Is<IBuildDefinition>(d => d.Id == 1)))
                .Returns(GetRandomStatus);

            var optionsMoq = new Mock<IMonitorOptions>();
            optionsMoq
                .SetupGet(o => o.IntervalSeconds)
                .Returns(60);

            var factoryMoq = new Mock<IBuildStoreFactory>();
            factoryMoq
                .Setup(f => f.GetBuildStore(It.IsAny<IMonitorOptions>()))
                .Returns(storeMoq.Object);

            IBuildDefinitionMonitor monitor = new BuildDefinitionMonitor(factoryMoq.Object);

            Application.Run(new BuildDefinitionsListForm(monitor, optionsMoq.Object));
        }

        private static IBuildStatus GetRandomStatus()
        {
            var statusMoq = new Mock<IBuildStatus>();
            statusMoq.Setup(s => s.Status).Returns((Status) s_Random.Next(1, 3));
            return statusMoq.Object;
        }
    }
}
