using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Dtos.FlowerDTOS;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace FlowerStoreAPI.Controllers
{   
    [Route("api/flowers")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly IFlowerRepo _repository;
        private readonly IMapper _mapper;
        
        public FlowersController(IFlowerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET api/flowers/{shopId}
        /// <summary>
        /// Gets you a list of all the flowers.
        /// </summary>
        /// <param name="ShopId">The unique identifier of the shop</param>
        /// <returns>A list of flowers</returns>
        [HttpGet("{ShopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult <IEnumerable<FlowerReadDto>>> GetAllFlowers(int ShopId)
        {
            var flowerItems = await _repository.GetAllFlowers(ShopId);

            return Ok( _mapper.Map<IEnumerable<FlowerReadDto>>(flowerItems).Select(x => x.Convert()).ToList());
        }

        //GET api/flowers/{shopId}/{id}
        /// <summary>
        /// Gets you a specific flower.
        /// </summary>
        /// <param name="id">The unique identifier of the flower</param>
        /// <param name="ShopId">The unique identifier of the shop</param>
        /// <returns></returns>
        [HttpGet("{shopId}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult <FlowerReadDto>> GetFlowerById(int ShopId, int id)
        {
            var flowerItem = await _repository.GetFlowerById(ShopId, id);
            if(flowerItem != null)
            {
                return Ok( _mapper.Map<FlowerReadDto>(flowerItem));
            }
            return NotFound();
        }

        //POST api/flowers/{shopId}
        /// <summary>
        /// Creates a new flower.
        /// </summary>
        /// <param name="ShopId">The unique identifier of the shop</param>
        /// <param name="flowerCreateDto">The unique identifier of the shop</param>
        /// <returns></returns>
        [HttpPost("{ShopId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<FlowerReadDto> CreateFlower(int ShopId, FlowerCreateDto flowerCreateDto)
        {

            var flowerModel = _mapper.Map<Flower>(flowerCreateDto);
            _repository.CreateFlowerAsync(ShopId, flowerModel);
            _repository.SaveChanges();

            var flowerReadDto = _mapper.Map<FlowerReadDto>(flowerModel);

            return CreatedAtRoute(nameof(GetFlowerById), new { Id = flowerReadDto.Id }, flowerReadDto);
        }


        // PUT api/flowers/{shopId}/{id}
        /// <summary>
        /// Changes an existing flower.
        /// </summary>
        /// <param name="id">The unique identifier of the flower</param>
        /// <param name="ShopId">The unique identifier of the shop</param>
        /// <param name="flowerUpdateDto">The unique identifier of the shop</param>
        /// <returns></returns>
        [HttpPut("{shopId}/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateFlower(int ShopId, int id, FlowerUpdateDto flowerUpdateDto)
        {
            var flowerModelFromRepo = await _repository.GetFlowerById(ShopId, id);
            if(flowerModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(flowerUpdateDto, flowerModelFromRepo);

            _repository.UpdateFlower(ShopId, flowerModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }


        //DELETE api/flowers/{shopId}/{id}
        /// <summary>
        /// Deletes an existing flower.
        /// </summary>
        /// <param name="id">The unique identifier of the flower</param>
        /// <param name="ShopId">The unique identifier of the shop</param>
        /// <returns></returns>
        [HttpDelete("{shopId}/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteFlowerAsync(int ShopId, int id)
        {
            var flowerModelFromRepo = await _repository.GetFlowerById(ShopId, id);
            if(flowerModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteFlower(ShopId, flowerModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}

    

