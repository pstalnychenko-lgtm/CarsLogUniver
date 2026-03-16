using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponents
    {
        public Guid Id { get; init; } = Guid.NewGuid();

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

        private string _notes;
        public string Notes
        {
            get => _notes;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _notes = value;
            }
        }

        public VehicleComponents(ComponentType componentType, string customPartName,
                                  bool isReplaced, bool isBeInNormalCondition, string notes)
        {
            ComponentType = componentType;
            CustomPartName = customPartName;
            IsReplaced = isReplaced;
            IsBeInNormalCondition = isBeInNormalCondition;
            Notes = notes;
        }
    }

    public enum ComponentType
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
