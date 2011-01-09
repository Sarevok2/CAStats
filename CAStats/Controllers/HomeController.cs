using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using CAStats.Models;
using CAStats.ViewModels;
using System.Text.RegularExpressions;


namespace CAStats.Controllers
{
    public class HomeController : Controller
    {
        
        private CAStatsEntities db = new CAStatsEntities();

        public ActionResult Index()
        {
            var jobs = from m in db.Jobs select m;

            CAIndexViewModel viewModel = new CAIndexViewModel();
            foreach (Job job in jobs.ToList())
            {
                string environment="", system="", type="";
                
                if (String.Compare(job.Jobset.Substring(5, 2),"PR") == 0){environment = "pr";}
                else if (String.Compare(job.Jobset.Substring(5, 2), "IG") == 0) { environment = "ig"; }

                if (String.Compare(job.Jobset.Substring(8, 2), "RB") == 0) { system = "rb"; }
                else if (String.Compare(job.Jobset.Substring(8, 2), "RF") == 0) { system = "rf"; }
                else system = "other";

                string pattern = @"R\d+$";
                if (Regex.IsMatch(job.Jobset, pattern)) { type = "report"; }
                else  { type = "batch"; }

                if (environment.Length > 0 && system.Length > 0 && type.Length > 0) 
                {
                    if (!viewModel.jobs[environment][system][type].ContainsKey(job.Jobset))
                    {
                        viewModel.jobs[environment][system][type].Add(job.Jobset, new List<string>());
                    }
                    viewModel.jobs[environment][system][type][job.Jobset].Add(job.Jobname);
                }
            }


            return View(viewModel);
        }

        public ActionResult ShowData(string environment, string system, string jobType, string submit, string jobsOrJobsets, string jobset, string jobname,
            string abortedOnly, string startDate, string endDate, string startTime, string endTime, string chartType, string chartSize, string yAxis)
        {
            if (submit.Equals(CAIndexViewModel.SUBMIT_RUNBOOK))
            {
                return null;
            }
            else
            {
                JobDataModel jobData = new JobDataModel();
                jobData.loadData(jobsOrJobsets, jobset, jobname, abortedOnly,
                    startDate, endDate, startTime, endTime, chartType, chartSize, yAxis);

                if (submit.Equals(CAIndexViewModel.SUBMIT_LIST))
                {
                    if (jobsOrJobsets.Equals("jobs")) { return View("ShowListJobs", jobData); }
                    else { return PartialView(); }
                    
                }
                else
                {
                    return PartialView();
                }
            }
            
        }

        public ActionResult showListJobs(JobDataModel jobData)
        {
            return PartialView(jobData);
        }

        public ActionResult showListJobsets(JobDataModel jobData)
        {
            return View(jobData);
        }

        public string showChart()
        {
            return "nothing";
        }

        public string showRunbook()
        {
            return "nothing";
        }
    }
}
