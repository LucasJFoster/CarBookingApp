using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarBookingApp.Data;
using Microsoft.EntityFrameworkCore;
using CarBookingApp.Repositories.Contracts;

namespace CarBookingApp.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ICarsRepository _carRepository;
        private readonly ICarsModelsRepository _CarModelRepository;
        private readonly IGenericRepository<Colour> _colourRepository;
        private readonly IGenericRepository<Make> _makesRepository;

        public CreateModel(ICarsRepository carRepository, 
           IGenericRepository<Make> makesRepository,
           ICarsModelsRepository CarModelRepository, 
           IGenericRepository<Colour> colourRepository)
        {
            this._carRepository = carRepository;
            this._makesRepository = makesRepository;
            this._CarModelRepository = CarModelRepository;
            this._colourRepository = colourRepository;
        }


        [BindProperty]
        public Car Car { get; set; }

        public SelectList Makes { get; set; }
        public SelectList Models { get; set; }
        public SelectList Colours { get; set; }

        public async Task< IActionResult> OnGet()
        {
            await LoadInitialData();
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        { 
            if(await _carRepository.IsLicencePlateExists(Car.LicensePlateNumber))
             {
                ModelState.AddModelError(nameof(Car.LicensePlateNumber), "License Plate Number Exists Already");
             }
        
            if (!ModelState.IsValid)
            {
                await LoadInitialData();
                return Page();
            }

            await _carRepository.Insert(Car);

            return RedirectToPage("./Index");

        }

        public async Task<JsonResult> OnGetCarModels(int makeId)
        {
            return new JsonResult(await _CarModelRepository.GetCarModelsByMake(makeId));
        }


            private async Task LoadInitialData()
            {
                Makes = new SelectList(await _makesRepository.GetAll(), "Id", "Name");
                Models = new SelectList(await _CarModelRepository.GetAll(), "Id", "Name");    
                Colours = new SelectList(await _colourRepository.GetAll(), "Id", "Name");
            }
        
    }
}
