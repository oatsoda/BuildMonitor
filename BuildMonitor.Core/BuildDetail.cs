namespace BuildMonitor.Core
{
   public class BuildDetail
    {
       public IBuildDefinition Definition { get; private set; }
       public IBuildStatus Status { get; private set; }

       public BuildDetail(IBuildDefinition definition, IBuildStatus status)
       {
           Status = status;
           Definition = definition;
       }
    }
}
