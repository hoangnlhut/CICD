using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Domain.Abstractions.Entities
{
    public abstract class DomainEntity<TKey>
    {
        public virtual TKey Id { get; set; }

        /// <summary>
        /// True if the domain entity has an identity
        /// <returns></returns>
        public bool IsTransient()
        {
            return Id.Equals(default(TKey));
        }
    }
}
