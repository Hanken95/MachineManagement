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


        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
