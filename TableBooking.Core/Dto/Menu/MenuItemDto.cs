namespace Core.Dto.Menu
{
    public class MenuItemDto
    {
        public MenuCategoryDto Category { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}