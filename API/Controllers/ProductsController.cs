

using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        //private readonly IGenericRepository<TEntity> repository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductType> typeRepository;
        private readonly IGenericRepository<ProductBrand> brandRepository;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductType> typeRepository, IGenericRepository<ProductBrand> brandRepository,
        IMapper mapper)
        {
            this.mapper = mapper;
            this.brandRepository = brandRepository;
            this.productRepository = productRepository;
            this.typeRepository = typeRepository;
        }


        // Won't be used after GenericRepository is implemented
        // public readonly IProductRepository repository;

        // public ProductsController(IProductRepository repository)
        // {
        //     this.repository = repository;

        // }

        // public ProductsController(IGenericRepository<TEntity> repository)
        // {
        //     this.repository = repository;
        // }



        // [HttpGet("{id}")]
        // public async Task<ActionResult<TEntity>> Get(int id)
        // {
        //     // return await repository.GetProductByIdAsync(id);
        //     var spec = new ProductsWithTypesAndBrandsSpecification();

        //     return Ok(await productRepository.GetByIdAsync(id));
        // }


        // [HttpGet]
        // public async Task<ActionResult<List<Product>>> GetProducts()
        // {
        //     var controller = new ProductsController(repository as IGenericRepository<Product>);
        //     var productRepo = repository as IGenericRepository<Product>;

        //     var spec = new ProductsWithTypesAndBrandsSpecification();

        //     var products = await productRepo.GetListAllAsync(spec);
        //     return Ok(products);
        // }

        // [HttpGet("test")]
        // public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        // {
        //     var entities = await repository.GetListAllAsync();
        //     return Ok(entities);
        // }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // return await repository.GetProductByIdAsync(id);
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await productRepository.GetEntityWithSpec(spec);

            /*
            return Ok(new ProductToReturnDto{
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name

            });
            */
            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }

        
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams param)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(param);

            var countSpec = new ProductsWithFiltersForCountSpecification(param);

            var products = await productRepository.GetListAllAsync(spec);
            
            var totalItems = await productRepository.CountAsync(countSpec);

            var product = await  productRepository.GetListAllAsync(spec);

            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(param.PageIndex, param.PageSize, totalItems, data));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {

            // var brands = await repository.GetProductBrandsAsync();
            var brands = await brandRepository.GetListAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {

            // var types = await repository.GetProductTypesAsync();
            var types = await typeRepository.GetListAllAsync();
            return Ok(types);
        }
    }
}