using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.Models
{
    public abstract class BaseEntity
    {
        public string id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
