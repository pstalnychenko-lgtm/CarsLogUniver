using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponents
    {
        public Guid Id { get; init; } = Guid.NewGuid(); // унікальний ідентифікатор компонента транспортного засобу

        public ComponentType ComponentType { get; private set; } // Тип компонента (наприклад, масло, фільтр, шини тощо)

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

        public VehicleComponents(ComponentType componentType, string customPartName,
                                  bool isReplaced, bool isBeInNormalCondition, string notesComponents) /* конструктор для створення запису про компонент транспортного засобу*/
        {
            ComponentType = componentType;
            CustomPartName = customPartName;
            IsReplaced = isReplaced;
            IsBeInNormalCondition = isBeInNormalCondition;
            NotesComponents = notesComponents;
        }
    }
    
    public enum ComponentType // Перелік типів компонентів, які можуть бути відстежені
    {
        OilFilter,      // Масляний фільтр
        AirFilter,      // Повітряний фільтр
        FuelFilter,     // Паливний фільтр
        Tires,          // Шини
        Brakes,         // Гальма
        TimingBelt,     // Ремінь ГРМ
        Battery,        // Акумулятор
        SparkPlugs,     // Свічки запалювання
        CoolantFluid,   // Охолоджуюча рідина
        BrakeFluid,     // Гальмівна рідина
        Other           // Інший компонент
    }
}
