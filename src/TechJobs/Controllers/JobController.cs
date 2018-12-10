﻿using Microsoft.AspNetCore.Mvc;
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

                    //Employer = newJobViewModel.Employers[newJobViewModel.EmployerID;
                    Employer = newJobViewModel.EmployerID;
                    Location = newJobViewModel.Location;
                    CoreCompetency = newJobViewModel.CoreCompetency;
                    PositionType = newJobViewModel.PositionType;
                    
                };

                jobData.Jobs.Add(newJob);
                //return Redirect(Index(newJob.ID));
                //return Redirect("/Job");


            }

            return View(newJobViewModel);
        }
    }
}
