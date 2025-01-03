using System.ComponentModel.DataAnnotations;
using Bogus;

namespace MachineManagement.Models
{
    public class Machine
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
        public bool Status { get; set; } = false;
        public List<string> Data { get; set; } = [];

        public bool IsValid => Id == Guid.Empty || string.IsNullOrWhiteSpace(Name);

        public Machine(string name, bool status)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = status;
            AddSeedData(5);
        }

        private void AddSeedData(int amount)
        {
            var faker = new Faker();
            for (int i = 0; i < amount; i++)
            {
                Data.Add(faker.Lorem.Text());
            }
        }

        public void SwitchStatus()
        {
            Status = !Status;
        }
    }
}
