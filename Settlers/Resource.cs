using System;
using System.Collections.Generic;
using System.Text;

namespace Settlers
{
    public class Resource
    {
        private ResourceType type;
        private int quantity;

        public Resource(ResourceType type, int quantity)
        {
            this.type = type;
            this.quantity = quantity;
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        public ResourceType GetType()
        {
            return type;
        }
    }
}
