namespace API.Dtos
{
    /// <summary>
    /// This class is used as a Data Transfer Object (DTO) to transfer the data about a product between 
    /// various parts of an application, such as between the API and the client.
    /// </summary>
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}