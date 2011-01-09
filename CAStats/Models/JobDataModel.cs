using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAStats.Models
{
    public class JobDataModel
    {
        public List<JobsetOccurance> jobsets { get; set; }

        public JobDataModel()
        {
            jobsets = new List<JobsetOccurance>();
        }

        public void loadData(string jobsOrJobsets, string jobsetName, string jobname, string abortedOnly, string startDate, string endDate,
            string startTime, string endTime, string chartType, string chartSize, string yAxis)
        {
            CAStatsEntities db = new CAStatsEntities();

            var joblist = from job in db.JobOccurances
                          where true             
                          select job;


            foreach (JobOccurance job in joblist)
            {
                bool jobsetFound = false;
                foreach (JobsetOccurance jobset in jobsets)
                {
                    if ( (jobset.name.Equals(job.jobset)) && (jobset.qualifier == job.qualifier) && (Math.Abs(jobset.startTime.Subtract(job.start_date).Days) < 12) ) 
                    {
                        jobsetFound = true;
                        jobset.addJobOccurance(job);
                        break;
                    }
                }
                if (!jobsetFound)
                {
                    JobsetOccurance jobset = new JobsetOccurance(job.jobset, job.qualifier, job.start_date, job.end_date);
                    jobset.addJobOccurance(job);
                    jobsets.Add(jobset);
                }
            }
        }

        public bool includeJob(JobOccurance job, string jobsOrJobsets, string jobsetName, string jobname, string abortedOnly, string startDate, string endDate,
            string startTime, string endTime, string chartType, string chartSize, string yAxis)
        {

            if (!jobsetName.Equals("all"))
            {
                if (!job.jobset.Equals(jobsetName)) return false;
                if (!jobname.Equals("all"))
                {
                    if (!job.jobname.Equals(jobname)) return false;
                }
            }
            return true;
        }
    }
}