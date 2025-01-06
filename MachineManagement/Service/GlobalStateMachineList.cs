using MachineManagement.Models;

namespace MachineManagement.Service
{
    public class GlobalStateMachineList
    {
        public List<Machine> Machines { get; set; } = [
            new Machine("Big logger", true),
            new Machine("Small logger", false),
            new Machine("Cutter", true),
            new Machine("Dishwasher", false),
        ];
        public Action? OnChange { get; set; }

        public Machine ChosenMachine { get; private set; } = new Machine("", false);

        public int TotalDataCount 
        { 
            get 
                { 
                    int total = 0;
                    foreach  (var machine in Machines)
                    {
                        total += machine.Data.Count;
                    }
                    return total;
                } 
        }

        public bool ChoseMachine(Machine machine)
        {
            if (machine == null) return false;
            if (!Machines.Contains(machine)) return false;
            ChosenMachine = machine;
            return true;
        }

        public bool AddMachine(Machine machine)
        {
            if (machine.IsValid)
            {
                Machines.Add(machine);
                NotifyStateChanged();
                return true;
            }
            else return false;
        }
        public bool RemoveMachine(Machine machine)
        {
            var result = Machines.Remove(machine);
            NotifyStateChanged();
            return result;
        }

        public void SwitchStatus(Machine machine)
        {
            machine.SwitchStatus();
            NotifyStateChanged();
        }

        public bool AddDataToMachine(string data, Machine machine)
        {
            bool success = machine.AddData(data);
            NotifyStateChanged();
            return success;
        }
        public bool UpdateData(string newData, Machine machine, int index)
        {
            bool success = machine.EditData(newData, index);
            NotifyStateChanged();
            return success;
        }


        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
