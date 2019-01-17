using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Entity
    {
        public Entity()
        {
            GUID = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
