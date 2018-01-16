using System;

namespace SyncExperiments
{
    public class ExperimentDetails
    {
        public DateTimeOffset StartDate { get; set; }

        public string Id { get; set; }

        public bool AllClusters { get; set; }

        public int TrafficRate { get; set; }

        public bool FlatB { get; set; }

        public bool InEvaluation { get; set; }

        public bool OneHundredPercentAll
        {
            get
            {
                return AllClusters && !FlatB && TrafficRate == 100;
            }
        }

        public bool FlatBAll
        {
            get
            {
                return AllClusters && FlatB && TrafficRate == 100;
            }
        }

        public bool OneHundredPercentFlatBPrelive
        {
            get
            {
                return !AllClusters && FlatB && TrafficRate == 100;
            }
        }

        public bool ZeroPercentPrelive
        {
            get
            {
                return !AllClusters && !FlatB && TrafficRate == 0;
            }
        }
    }
}
