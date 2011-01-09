using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAStats.Models
{
    public class JobsetOccurance
    {
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int qualifier;
        public List<JobOccurance> jobs { get; set; }

        public JobsetOccurance(string name, int qualifier, DateTime startTime, DateTime endTime)
        {
            this.name = name;
            this.qualifier = qualifier;
            this.startTime = startTime;
            this.endTime = endTime;
            this.jobs = new List<JobOccurance>();
        }

        public void addJobOccurance(JobOccurance job)
        {
            jobs.Add(job);
            if (DateTime.Compare(job.start_date, startTime) < 0) { startTime = job.start_date; }
            if (DateTime.Compare(job.end_date, endTime) > 0) { endTime = job.end_date; }
        }
    }
}