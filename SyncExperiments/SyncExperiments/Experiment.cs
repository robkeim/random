namespace SyncExperiments
{
    public class Experiment
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ExperimentDetails Prod { get; set; }

        public ExperimentDetails Dev { get; set; }
    }
}
