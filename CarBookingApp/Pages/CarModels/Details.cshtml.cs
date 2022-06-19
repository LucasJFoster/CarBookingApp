using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarBookingApp.Data;
using CarBookingApp.Repositories.Contracts;

namespace CarBookingApp.Pages.CarModels
{
    public class DetailsModel : PageModel
    {
        private readonly ICarsModelsRepository _repository;

        public DetailsModel(ICarsModelsRepository repository)
        {
            this._repository = repository;
        }

        public CarModel CarModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CarModel = await _repository.Get(id.Value);

            if (CarModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
