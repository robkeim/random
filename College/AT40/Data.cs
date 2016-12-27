namespace AT40
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime;
    using System.Xml;

    [DataContract(Name = "AT40")]
    public struct History
    {
        [DataMember(Order = 0)]
        public Week[] Weeks;
    }

    public struct Week : IComparable<Week>
    {
        [DataMember(Order = 0)]
        public string Date;

        [DataMember(Order = 1)]
        public Song[] Songs;

        public int CompareTo(Week week)
        {
            return Date.CompareTo(week.Date);
        }
    }

    public struct Song
    {
        [DataMember(Order = 0)]
        public string Rating;

        [DataMember(Order = 1)]
        public string Title;

        [DataMember(Order = 2)]
        public string Artist;
    }
}
