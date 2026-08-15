using jobpostingboard_Dela_Cruz.DTOs;
using jobpostingboard_Dela_Cruz.Models;
using jobpostingboard_Dela_Cruz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobpostingboard_Dela_Cruz.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly IJobRepository _repository;

        public JobsController(IJobRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string? search)
        {
            var jobs = _repository.GetAll(search);

            return View(jobs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobCreateDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var job = new Job
            {
                Title = model.Title,
                Company = model.Company,
                Location = model.Location,
                Description = model.Description,
                JobType = model.JobType
            };

            _repository.Add(job);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var job = _repository.GetById(id);

            if (job == null)
                return NotFound();

            var model = new JobUpdateDto
            {
                Id = job.Id,
                Title = job.Title,
                Company = job.Company,
                Location = job.Location,
                Description = job.Description,
                JobType = job.JobType
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobUpdateDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var job = _repository.GetById(model.Id);

            if (job == null)
                return NotFound();

            job.Title = model.Title;
            job.Company = model.Company;
            job.Location = model.Location;
            job.Description = model.Description;
            job.JobType = model.JobType;

            _repository.Update(job);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(int id)
        {
            var job = _repository.GetById(id);

            if (job == null)
                return NotFound();

            job.IsClosed = true;

            _repository.Update(job);

            return RedirectToAction(nameof(Index));
        }
    }
}