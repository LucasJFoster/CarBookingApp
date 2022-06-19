using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarBookingApp.Data;
using CarBookingApp.Repositories.Contracts;

namespace CarBookingApp.Pages.CarModels
{
    public class EditModel : PageModel
    {

        private readonly IGenericRepository<CarModel> _CarModelrepository;
        private readonly IGenericRepository<Make> _makesrepository;

        public EditModel (IGenericRepository<CarModel> CarModelrepository, IGenericRepository<Make> Makesrepository)
        {
            this._CarModelrepository = CarModelrepository;
            this._makesrepository = Makesrepository;
        }

        [BindProperty]
        public CarModel CarModel { get; set; }
        public SelectList Makes { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CarModel = await _CarModelrepository.Get(id.Value);

            if (CarModel == null)
            {
                return NotFound();
            }
            await LoadInitialData();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadInitialData();
                return Page();
            }

            try
            {
                await _CarModelrepository.Update(CarModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CarModelExistsAsync(CarModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task LoadInitialData()
        {
            Makes = new SelectList(await _makesrepository.GetAll(), "Id", "Name");
        }

        private async Task<bool> CarModelExistsAsync(int id)
        {
            return await _CarModelrepository.Exists(id);
        }
    }
}
