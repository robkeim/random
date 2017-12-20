using System;

namespace SyncExperiments
{
    public class Experiment
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? ProdStartDate { get; set; }

        public DateTimeOffset? DevStartDate { get; set; }
    }
}
