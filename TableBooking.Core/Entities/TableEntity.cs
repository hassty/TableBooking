using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class TableEntity
    {
        public int Capacity { get; set; }
        public int Id { get; set; }
        public RestaurantEntity Restaurant { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TableEntity entity &&
                   Capacity == entity.Capacity &&
                   Id == entity.Id &&
                   EqualityComparer<RestaurantEntity>.Default.Equals(Restaurant, entity.Restaurant);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Capacity, Id, Restaurant);
        }
    }
}