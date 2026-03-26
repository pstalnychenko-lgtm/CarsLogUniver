using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponents
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _customPartName;
        public string CustomPartName
        {
            get => _customPartName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _customPartName = value;
            }
        }

        public bool IsReplaced { get; private set; }

        public bool IsBeInNormalCondition { get; private set; }

        private string _notesComponents;
        public string NotesComponents
        {
            get => _notesComponents;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _notesComponents = value;
            }
        }

        public VehicleComponents(string customPartName, bool isReplaced,
                                  bool isBeInNormalCondition, string notesComponents)
        {
            CustomPartName = customPartName;
            IsReplaced = isReplaced;
            IsBeInNormalCondition = isBeInNormalCondition;
            NotesComponents = notesComponents;
        }
    }
}
