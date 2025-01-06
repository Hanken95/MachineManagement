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

        public string? LastData => Data.LastOrDefault();

        public bool IsValid
        {
            get
            {
                return Id != Guid.Empty && !string.IsNullOrWhiteSpace(Name);
            }
        }

        public Machine(string name, bool status, bool addSeedData = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = status;
            if (addSeedData) AddSeedData();
        }

        public bool AddData(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;   

            Data.Add(data);
            return true;
        }

        public bool EditData(string newData, int indexOfData)
        {
            if (string.IsNullOrWhiteSpace(newData)) return false;

            Data[indexOfData] = newData;
            return true;
        }

        public void AddSeedData()
        {
            var faker = new Faker();
            for (int i = 0; i < new Random().Next(2, 6); i++) Data.Add(faker.Lorem.Sentence(3, 2));
        }

        public void AddSeedData(int amount)
        {
            var faker = new Faker();
            for (int i = 0; i < amount; i++) Data.Add(faker.Lorem.Sentence(3, 2));
        }

        public void SwitchStatus()
        {
            Status = !Status;
        }
    }
}
