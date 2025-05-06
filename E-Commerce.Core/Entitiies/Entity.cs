using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitiies
{
    public abstract class Entity<Key> where Key : struct
    {
        public Key Id { get; set; }
        public Entity()
        {
            Id = default;
        }
    }
    }
