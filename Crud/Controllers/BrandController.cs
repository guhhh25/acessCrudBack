using Crud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandRepository _brandRepository;
        private readonly CarRepository _carRepository;

        public BrandController(BrandRepository brandRepository, CarRepository carRepository)
        {
            _brandRepository = brandRepository;
            _carRepository = carRepository;
        }

        [HttpGet]
        public IActionResult GetBrands()
        {
            var brands = _brandRepository.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult GetBrandById(int id)
        {
            var brand = _brandRepository.GetBrandById(id);

            if (brand != null)
            {
                return Ok(brand);
            }

            return NotFound("Brand not found!");
        }

        [HttpPost]
        public IActionResult AddBrand([FromBody] Brand brand)
        {
            if (brand == null || string.IsNullOrEmpty(brand.Name))
            {
                return BadRequest("Brand name is required.");
            }

            var BrandNameExists = _brandRepository.GetBrands();

            if (BrandNameExists.Any(b => b.Name == brand.Name))
            {
                return BadRequest(new { message = "This name already exists." });
            }

            _brandRepository.AddBrand(brand);
            return Ok("Brand added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBrand([FromBody] Brand updatedBrand)
        {
            if (updatedBrand == null || string.IsNullOrEmpty(updatedBrand.Name))
            {
                return BadRequest("Brand name is required.");
            }

            var existingBrand = _brandRepository.GetBrandById(updatedBrand.Id);

            if (existingBrand == null)
            {
                return NotFound("Brand not found.");
            }

            var BrandNameExists = _brandRepository.GetBrands();

            if (BrandNameExists.Any(b => b.Name == updatedBrand.Name))
            {
                return BadRequest(new { message = "This name already exists." });
            }

            existingBrand.Name = updatedBrand.Name;
            _brandRepository.UpdateBrand(existingBrand);

            return Ok("Brand updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            var existingBrand = _brandRepository.GetBrandById(id);
            if (existingBrand == null)
            {
                return NotFound("Brand not found.");
            }

            var brandInUse = _carRepository.GetAllCars();

            if (brandInUse.Any(b => b.BrandId == id))
            {
                return BadRequest(new { message = "This brand is in use!" });
            }

            _brandRepository.DeleteBrand(id);

            return Ok("Brand deleted successfully.");
        }

    }
}
