using System;
using System.Collections.Generic;
using System.Collections;

namespace CAStats.ViewModels
{
    public class CAIndexViewModel
    {
        public const string SUBMIT_LIST = "Show List", SUBMIT_CHART = "Show Chart", SUBMIT_RUNBOOK = "Runbook Info";
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>> jobs { get; set; }

        public CAIndexViewModel()
        {
            jobs = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>>();
            jobs.Add("pr", new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>());
            jobs.Add("ig", new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>());
            jobs["pr"].Add("rb", new Dictionary<string, Dictionary<string, List<string>>>());
            jobs["pr"].Add("rf", new Dictionary<string, Dictionary<string, List<string>>>());
            jobs["pr"].Add("other", new Dictionary<string, Dictionary<string, List<string>>>());
            jobs["ig"].Add("rb", new Dictionary<string, Dictionary<string, List<string>>>());
            jobs["ig"].Add("rf", new Dictionary<string, Dictionary<string, List<string>>>());
            jobs["ig"].Add("other", new Dictionary<string, Dictionary<string, List<string>>>());
            jobs["pr"]["rb"].Add("batch", new Dictionary<string, List<string>>());
            jobs["pr"]["rb"].Add("report", new Dictionary<string, List<string>>());
            jobs["pr"]["rf"].Add("batch", new Dictionary<string, List<string>>());
            jobs["pr"]["rf"].Add("report", new Dictionary<string, List<string>>());
            jobs["pr"]["other"].Add("batch", new Dictionary<string, List<string>>());
            jobs["pr"]["other"].Add("report", new Dictionary<string, List<string>>());
            jobs["ig"]["rb"].Add("batch", new Dictionary<string, List<string>>());
            jobs["ig"]["rb"].Add("report", new Dictionary<string, List<string>>());
            jobs["ig"]["rf"].Add("batch", new Dictionary<string, List<string>>());
            jobs["ig"]["rf"].Add("report", new Dictionary<string, List<string>>());
            jobs["ig"]["other"].Add("batch", new Dictionary<string, List<string>>());
            jobs["ig"]["other"].Add("report", new Dictionary<string, List<string>>());
        }

        public string toJavascript()
        {
            string jobHash = "{";
            foreach (string envKey in jobs.Keys)
            {
                string envString = envKey + ": {";
                foreach (string sysKey in jobs[envKey].Keys)
                {
                    string sysString = sysKey + ": {";
                    foreach (string typeKey in jobs[envKey][sysKey].Keys)
                    {
                        string typeString = typeKey + ": {";
                        foreach (string jobsetKey in jobs[envKey][sysKey][typeKey].Keys)
                        {
                            string jobsetString = jobsetKey + ": [";
                            string jobArray = "";
                            foreach (string jobname in jobs[envKey][sysKey][typeKey][jobsetKey])
                            {
                                string quotedJob = "\"" + jobname + "\"";
                                jobArray += jobArray.Equals(string.Empty) ? quotedJob : ", " + quotedJob;
                            }
                            jobsetString += jobArray + "]";
                            typeString += typeString.Equals(typeKey + ": {") ? jobsetString : ",\n " + jobsetString;
                        }
                        typeString += "}";
                        sysString += sysString.Equals(sysKey + ": {") ? typeString : ",\n " + typeString;
                    }
                    sysString += "}";
                    envString += envString.Equals(envKey + ": {") ? sysString : ",\n " + sysString;
                }
                envString += "}";
                jobHash += jobHash.Equals("{") ? envString : ",\n " + envString;
            }
            jobHash += "}";

            return jobHash;
        }
    }
}