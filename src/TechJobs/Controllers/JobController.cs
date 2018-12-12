using Microsoft.AspNetCore.Mvc;
using System;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view

            //without using ViewBag. In other words, use the Job as ViewModel

            Job theJob = jobData.Find(id);

            return View(theJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if(ModelState.IsValid)
            {

                Job newJob = new Job
                {

                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)
                    
                };

                jobData.Jobs.Add(newJob);
               

                try
                {
                    //return Redirect("/Job");
                    return RedirectToPage("/Job?id={0}", newJob.ID);
                    //left off here, I'm in the process of redirecting to the
                    //single job display page for the new job
                }
                catch (System.InvalidOperationException e)
                {
                    Console.WriteLine("Trouble redirecting, here's the issue: " + e.Message);
                }
                
                


            }

            return View(newJobViewModel);
        }
    }
}
