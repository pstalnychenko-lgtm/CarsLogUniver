using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponents // клас базових компонентів та деталей машини
    {
        public Guid Id { get; init; } = Guid.NewGuid(); // унікальний ідентифікатор компонента транспортного засобу

        private string _customPartName;
        public string CustomPartName // Назва компонента, якщо вибрано "Other"
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

        public bool IsReplaced { get; private set; } // Чи був компонент замінений

        public bool IsBeInNormalCondition { get; private set; } // Чи знаходиться компонент в нормальному стані

        private string _notesComponents;
        public string NotesComponents // Додаткові нотатки або коментарі щодо компонента (наприклад, причина заміни)
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

        public VehicleComponents(string customPartName,
                                  bool isReplaced, bool isBeInNormalCondition, string notesComponents) // конструктор для створення запису про компонент транспортного засобу
        {
            CustomPartName = customPartName;
            IsReplaced = isReplaced;
            IsBeInNormalCondition = isBeInNormalCondition;
            NotesComponents = notesComponents;
        }
    }
}
