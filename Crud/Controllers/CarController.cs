namespace Crud.Controllers
{
    using Crud.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarRepository _carRepository;
        private readonly BrandRepository _brandRepository;

        public CarController(CarRepository carRepository, BrandRepository brandRepository)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
        }

        [HttpPost]
        public IActionResult CreateCar([FromBody] Car car)
        {
            try
            {
                _carRepository.InsertCar(car);

                return Ok("Carro criado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao inserir carro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetCarById(int id)
        {
            try
            {
                var car = _carRepository.GetCarById(id);

                return Ok(car);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar carro: {ex.Message}");
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                _carRepository.DeleteCar(id);

                return Ok("Carro deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao inserir carro: {ex.Message}");
            }
        }


        [HttpPut]
        public IActionResult UpdateCar([FromBody] Car car)
        {
            try
            {
                _carRepository.UpdateCar(car);

                return Ok("Carro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao inserir carro: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            var cars = _carRepository.GetAllCars();
            return Ok(cars);
        }
    }
}
