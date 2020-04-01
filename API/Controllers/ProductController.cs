using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IProductRepository _products;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            _products = productRepository;
            _mapper = mapper;
        }
        private async Task<bool> ProductExists(int id)
        {
            return await _products.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Product>))]
        public IActionResult GetProduct()
        {
            var products = _products.GetAll();
            var listproductDto = new List<ListProductDto>();

            foreach (var product in products)
            {
                listproductDto.Add(_mapper.Map<ListProductDto>(product));
            }
            return Ok(listproductDto);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Product))]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [Produces(typeof(Product))]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductDto productdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productdto.Id)
            {
                return BadRequest();
            }

            var productObj = _mapper.Map<Product>(productdto);

            try
            {
                await _products.Update(productObj);
                return Ok(productObj);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost]
        [Produces(typeof(Product))]
        public async Task<IActionResult> PostProduct([FromForm] ProductDto productdto)
        {
            var folderName = Path.Combine("Resources", "uploadPics");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var uniqueFileName = productdto.Image.FileName;
            // Saving Image on Server
            if (productdto.Image.Length > 0)
            {
                var dbPath = Path.Combine(folderName, uniqueFileName);

                using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
                {
                    await productdto.Image.CopyToAsync(fileStream);
                }
                Product product = new Product();
                product.Name = productdto.Name;
                product.Price = productdto.Price;
                product.Description = productdto.Description;
                product.Available = productdto.Available;
                product.ImagePath = dbPath;
                await _products.Add(product);
                await _products.Save();

            }
            return Ok(new { status = true, message = "Product Created Successfully" });
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Product))]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await ProductExists(id))
            {
                return NotFound();
            }
            await _products.Remove(id);
            return Ok(new { status = true, message = "Product Deleted Successfully" });
        }
    }
}

