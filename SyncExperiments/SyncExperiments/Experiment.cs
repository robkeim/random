using System;

namespace SyncExperiments
{
    public class Experiment
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ExperimentDetails Prod { get; set; }

        public ExperimentDetails Dev { get; set; }
    }

    public class ExperimentDetails
    {
        public DateTimeOffset StartDate { get; set; }

        public string Id { get; set; }

        public bool AllClusters { get; set; }

        public int TrafficRate { get; set; }

        public bool FlatB { get; set; }

        public bool InEvaluation { get; set; }
    }
}
