using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Player
    {
        public Player(int id, string name, string? postion = null)
        {
            this.Id = id;
            this.Name = name;
            this.Position = postion;
        }
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Position { get; private set; }
    }
}
