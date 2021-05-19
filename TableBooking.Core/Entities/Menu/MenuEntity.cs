using System;
using System.Collections.Generic;

namespace Core.Entities.Menu
{
    public class MenuEntity
    {
        public IList<MenuCategoryEntity> Categories { get; private set; }
        public int Id { get; set; }

        public MenuEntity()
        {
            Categories = new List<MenuCategoryEntity>();
        }

        public override bool Equals(object obj)
        {
            return obj is MenuEntity entity &&
                   EqualityComparer<IList<MenuCategoryEntity>>.Default.Equals(Categories, entity.Categories) &&
                   Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Categories, Id);
        }
    }
}